using System;
using System.Collections.Generic;
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
    public class HREmployeeLeaveHistoryServices : BaseRepository<HREmployeeLeaveHistory, HREmployeeLeaveHistoryModel>, IHREmployeeLeaveHistoryServices<HREmployeeLeaveHistory, HREmployeeLeaveHistoryModel>
    {
        public HREmployeeLeaveHistoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }


        public virtual async Task<IEnumerable<proc_GetEmployeeLeaveSummary_Result>> GetsEmployeeLeaveSummary(long? idHREmployee, int? yearNP)
        {
            try
            {
                object[] obj =
               {
                     new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                     new SqlParameter() {ParameterName = "@paramYearNP", SqlDbType = SqlDbType.Int, Value= yearNP}
            };
                return await ExecuteProcedure<proc_GetEmployeeLeaveSummary_Result>("EXEC proc_GetEmployeeLeaveSummary @paramIdHREmployee,@paramYearNP", obj);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }
        public virtual async Task<IEnumerable<proc_GetHREmployeeLeaveHistoryPerYearDateReport_Result>> GetsEmployeeLeavebalanceSummary(long? idHREmployee, int? yearNP, DateTime RequestedDate)
        {
            try
            {
                object[] obj =
               {
                     new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                     new SqlParameter() {ParameterName = "@paramYearNP", SqlDbType = SqlDbType.Int, Value= yearNP},
                     new SqlParameter() {ParameterName = "@paramYearDate", SqlDbType = SqlDbType.DateTime, Value= RequestedDate}
            };
                return await ExecuteProcedure<proc_GetHREmployeeLeaveHistoryPerYearDateReport_Result>("EXEC proc_GetHREmployeeLeaveHistoryPerYearDateReport @paramIdHREmployee,@paramYearNP,@paramYearDate", obj);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
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

        public virtual async Task<IEnumerable<proc_GetRecommendedPerson_Result>> GetsRecommendedEmployee(long? idHREmployee, long? idHRCompany)
        {
            try
            {
                object[] obj =
               {
                    new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                     new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
            };
                return await ExecuteProcedure<proc_GetRecommendedPerson_Result>("EXEC proc_GetRecommendedPerson @paramIdHRCompany,@paramIdHREmployee", obj);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<bool> CheckLeaveExistance(long? idHREmployee, DateTime fromDate, DateTime toDate)
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

        public override async Task<dynamic> CommitSaveChangesAsync(HREmployeeLeaveHistoryModel entityModel, CRUDType mode, object id = null, bool retIdentity = true)
        {
            try
            {
                var entity = MapModelToEntity(entityModel);
                switch (mode)
                {
                    case CRUDType.CREATE:
                        entity.LeaveYear = entity.LeaveValidFrom.Year;
                        entity.CreatedOn = DateTime.Now;
                        dynamic insertObject = _dbSet.Add(entity);
                        await this.UnitOfWork.SaveAsync();
                        return retIdentity ? insertObject.Id : -1;

                    case CRUDType.UPDATE:
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Modified;
                        await this.UnitOfWork.SaveAsync();
                        return retIdentity ? ((dynamic)entity).Id : -1;

                    case CRUDType.DELETE:
                        entity = await GetByIdAsync(entity.Id);
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Deleted;
                        await this.UnitOfWork.SaveAsync();
                        return retIdentity ? ((dynamic)entity).Id : -1;
                    default:
                        return -1;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
