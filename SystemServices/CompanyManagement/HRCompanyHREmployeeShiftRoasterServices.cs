using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.CompanyManagement;
using SystemModels.CompanyManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.CompanyManagement
{
    public class HRCompanyHREmployeeShiftRoasterServices : BaseRepository<HRCompanyHREmployeeShiftRoaster, HRCompanyHREmployeeShiftRoasterModel>, IHRCompanyHREmployeeShiftRoasterServices<HRCompanyHREmployeeShiftRoaster, HRCompanyHREmployeeShiftRoasterModel>
    {
        public HRCompanyHREmployeeShiftRoasterServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<proc_GetAssignHREmployeeOfShiftRoaster_Result>> Gets(long? idHRCompany, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
               new SqlParameter() {ParameterName = "@empName", SqlDbType = SqlDbType.VarChar, Value = searchKey}
                };
                return (await this.UnitOfWork.Db.Database.SqlQuery<proc_GetAssignHREmployeeOfShiftRoaster_Result>("EXEC proc_GetAssignHREmployeeOfShiftRoaster @paramIdHRCompany,@empName", obj).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection).ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<long>> GetsAssignEmployee(long? idHRCompany, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value = fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = toDate}
            };
                return (await this.UnitOfWork.Db.Database.SqlQuery<proc_GetAssignHREmployeeOfShiftRoasterByDate_Result>("EXEC proc_GetAssignHREmployeeOfShiftRoasterByDate @paramIdHRCompany,@paramFromDate,@paramToDate", obj).ToListAsync()).Select(x => x.IdHREmployee).ToList();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        //public virtual async Task<List<HRCompanyHREmployeeShiftRoasterModel>> GetsByShiftDate(long? idShiftDate)
        //{
        //    try
        //    {
        //        List<HRCompanyHREmployeeShiftRoasterModel> _list = new List<HRCompanyHREmployeeShiftRoasterModel>();
        //        var model = _dbSet.Where(x => x.IdHRCompanyHREmployeeShiftDate == idShiftDate).OrderBy(x => x.IdDayOfWeekNP);
        //        foreach (var item in model)
        //        {
        //            _list.Add(new HRCompanyHREmployeeShiftRoasterModel
        //            {
        //                Id = item.Id,
        //                IdDayOfWeekNP = item.IdDayOfWeekNP,
        //                IdHRCompanyHREmployeeShiftDate = item.IdHRCompanyHREmployeeShiftDate,
        //                IsWeekend = item.IsWeekend,
        //                DOWNP = item.DayOfWeekNP.DOWNP,
        //                IsActive = item.IsActive,
        //                //DDLShiftRoasterSelected =_list.FirstOrDefault(x=>x.IdDayOfWeekNP==item.IdDayOfWeekNP) new List<long> { (long)item.IdHRCompanyShiftRoaster }
        //            });
        //        }
        //        return _list;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception(exp.Message);
        //    }
        //}

        public virtual async Task<List<proc_GetEmployeeShiftRoasterDetail_Result>> Gets(long? idHREmployee)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
            };
                return (await this.ExecuteProcedure<proc_GetEmployeeShiftRoasterDetail_Result>("EXEC proc_GetEmployeeShiftRoasterDetail @paramIdHREmployee", obj)).ToList();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<List<HRCompanyHREmployeeShiftRoasterModel>> GetsModel(long? idHREmployee)
        {
            try
            {
                List<HRCompanyHREmployeeShiftRoasterModel> list = new List<HRCompanyHREmployeeShiftRoasterModel>();
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
                 };
                var model = await this.ExecuteProcedure<proc_GetEmployeeShiftRoasterDetail_Result>("EXEC proc_GetEmployeeShiftRoasterDetail @paramIdHREmployee", obj);
                foreach (var item in model)
                {
                    if ((bool)item.IsWeekend)
                    {
                        list.Add(new HRCompanyHREmployeeShiftRoasterModel
                        {
                            Id = 0,
                            IdDayOfWeekNP = item.IdDayOfWeekNP,
                            //IdHREmployee = item.IdHREmployee,
                            IdHRCompanyShiftRoaster = null,
                            DOWNP = item.DOWNP,
                            IsActive = true,
                            IsWeekend = true
                        });
                    }
                    else
                    {
                        if (item.IdHRCompanyShiftRoaster == null)
                        {
                            list.Add(new HRCompanyHREmployeeShiftRoasterModel
                            {
                                Id = 0,
                                IdDayOfWeekNP = item.IdDayOfWeekNP,
                                //IdHREmployee = item.IdHREmployee,
                                IdHRCompanyShiftRoaster = null,
                                DOWNP = item.DOWNP,
                                IsActive = true,
                                IsWeekend = false
                            });
                        }
                        else if (list.Where(x => x.IdDayOfWeekNP == item.IdDayOfWeekNP).Count() == 1)
                        {
                            list.Where(x => x.IdDayOfWeekNP == item.IdDayOfWeekNP).FirstOrDefault().DDLShiftRoasterSelected.Add((long)item.IdHRCompanyShiftRoaster);
                        }
                        else
                            list.Add(new HRCompanyHREmployeeShiftRoasterModel
                            {
                                Id = 0,
                                IdDayOfWeekNP = item.IdDayOfWeekNP,
                                IdHRCompanyShiftRoaster = null,
                                //IdHREmployee = item.IdHREmployee,
                                IsActive = true,
                                IsWeekend = false,
                                DOWNP = item.DOWNP,
                                DDLShiftRoasterSelected = new List<long> { (long)item.IdHRCompanyShiftRoaster }
                            });
                    }
                }
                return list;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<List<HRCompanyHREmployeeShiftRoasterModel>> GetsWeekdayModel(long? idHREmployee)
        {
            try
            {
                List<HRCompanyHREmployeeShiftRoasterModel> list = new List<HRCompanyHREmployeeShiftRoasterModel>();
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
                 };
                var model = await this.ExecuteProcedure<proc_GetEmployeeShiftRoasterDetail_Result>("EXEC proc_GetEmployeeShiftRoasterDetail @paramIdHREmployee", obj);
                return model.Select(x => new HRCompanyHREmployeeShiftRoasterModel { Id = 0, IdDayOfWeekNP = x.IdDayOfWeekNP, DOWNP = x.DOWNP, IdHRCompanyShiftRoaster = x.IdHRCompanyShiftRoaster, IsActive = true, IsWeekend = (bool)x.IsWeekend }).ToList();
            }
            catch (Exception exp)
            {

                throw new Exception(exp.Message);
            }
        }

        public async Task<List<EmployeeShiftRoasterSelectedModel>> GetsByCompany(long? idHRCompany, long? idHREmployee, int? idRoleType)
        {
            object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
            var model = (await ExecuteProcedure<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", obj)).ToList();
            return model.Select(x => new EmployeeShiftRoasterSelectedModel { IdHREmployee = x.Id, EmployeeName = x.HREmployeeNameNP, IsChecked = true, PISNumber = x.PISNumber, DesignationTitle = x.HRDesignationTitle, DivisionName = x.HRCompanyDivisionName }).ToList();
        }

        public async Task DeleteHREmployeeShiftRoaster(long? id)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdShiftRoaster", SqlDbType = SqlDbType.BigInt, Value= id}
            };
                await UnitOfWork.Db.Database.ExecuteSqlCommandAsync("EXEC proc_DeleteHRCompanyHREmployeeShiftRoaster @paramIdShiftRoaster", obj);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<bool> DeleteHRCompanyHREmployeeShiftRoasterAsync(ICollection<HRCompanyHREmployeeShiftRoaster> EntityList)
        {
            try
            {
                _dbSet.RemoveRange(EntityList);
                await this.UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
