using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.SystemSecurity;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemServices.EmployeeManagement
{
    public class HREmployeeKaajHistoryServices : BaseRepository<HREmployeeKaajHistory, HREmployeeKaajHistoryModel>, IHREmployeeKaajHistoryServices<HREmployeeKaajHistory, HREmployeeKaajHistoryModel>
    {
        public HREmployeeKaajHistoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<HREmployeeKaajHistory>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey="")
        {
            try
            {
                var model = await FindAllAsync(x => x.KaajRegistrationNumber.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<proc_GetEmployeeDetail_Result> GetsEmployeeDetail(long? idHREmployee, long? idHRCompany)
        {
            try
            {
                object[] obj =
               {
                     new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                     new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany}
            };
                return await ExecuteScalarFunction<proc_GetEmployeeDetail_Result>("EXEC proc_GetEmployeeDetail @paramIdHREmployee,@paramIdHRCompany", obj);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        

        public virtual async Task<bool> CheckKaajTrainingExistance(long? idHREmployee, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] obj =
             {
                     new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                     new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value = fromDate},
                     new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = toDate}
              };
                return await ExecuteScalarFunction<bool>("SELECT * FROM ifc_CheckLeaveExists(@paramIdHREmployee,@paramFromDate,@paramToDate)", obj);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task CommitAsync(HREmployeeKaajHistoryModel entityModel, CRUDType mode)
        {
            try
            {
                var entity = MapModelToEntity(entityModel);
                switch (mode)
                {
                    case CRUDType.CREATE:
                        var kaajModel = (from c in _dbSet where c.IdHrCompany == entity.IdHrCompany && c.FiscalYear == entity.FiscalYear select c.Id);
                        long kaajMaxId = kaajModel.Count() > 0 ? kaajModel.Max() : 0;
                        


                        var registrationNumber =await (from c in _dbSet where c.Id == kaajMaxId select c.KaajRegistrationNumber).FirstOrDefaultAsync();
                        int KaajCount = registrationNumber != null ? int.Parse(registrationNumber.Split('-')[1]) + 1 : 1;
                        



                        entity.KaajRegistrationNumber = $"{entity.FiscalYear}-{KaajCount}"; //generate registration number

                        //checking kaajregistrationNOalreadyExistsed

                        bool KaajRegExisted = await Exits(x => x.IdHrCompany==entityModel.IdHrCompany && x.KaajRegistrationNumber==entity.KaajRegistrationNumber);
                        if (KaajRegExisted)
                        {
                            Random rnd = new Random();
                            int NewKaajCount=rnd.Next(2000,10000);
                            entity.KaajRegistrationNumber = $"{entity.FiscalYear}-{NewKaajCount}";
                        }


                        entity.KaajTakenNumber = await CountAsync(x => x.IdHrCompany == entity.IdHrCompany && x.FiscalYear == entity.FiscalYear && x.IdHREmployee == entity.IdHREmployee) + 1; //count current year leave

                        

                        bool Createresult = await Exits(x => x.IsActive && x.IdKaajStatus != 4 && x.IdHREmployee == entity.IdHREmployee && ((entity.KaajFromDate >= x.KaajFromDate && entity.KaajFromDate <= x.KaajFromDate) || (entity.KaajToDate >= x.KaajFromDate && entity.KaajToDate <= x.KaajToDate)));



                        if (!Createresult)
                        {
                            _dbSet.Add(entity);
                            await this.UnitOfWork.SaveAsync();
                        }
                        break;
                    case CRUDType.UPDATE:
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Modified;
                        await this.UnitOfWork.SaveAsync();
                        break;
                    case CRUDType.DELETE:
                        entity = await GetByIdAsync(entity.Id);
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Deleted;
                        await this.UnitOfWork.SaveAsync();
                        break;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
