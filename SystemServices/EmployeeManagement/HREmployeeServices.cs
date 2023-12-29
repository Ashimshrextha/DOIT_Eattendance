using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemModels.EmployeeManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.EmployeeManagement
{
    public class HREmployeeServices : BaseRepository<HREmployee, HREmployeeModel>, IHREmployeeServices<HREmployee>
    {
        public HREmployeeServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public async Task<List<proc_GetEmployeeByDesignation_Result>> GetsByCompany(long? idHRCompany, long? idHREmployee, int? idRoleType)
        {
            object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 1},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
            return (await ExecuteProcedure<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", obj)).ToList();
        }

        public async Task<ICollection<proc_GetEmployeeByDesignation_Result>> Gets(long? idHRCompany, long? idHREmployee, int? idRoleType,int? status)
        {
            object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                 new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = status},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
            return (await ExecuteProcedure<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", obj)).ToList();
        }

        public async Task<IPagedList<proc_GetEmployeeByDesignation_Result>> GetPagedList(long? idHRCompany, long? idHREmployee, int? idRoleType, int? active,int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = active},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return (await ExecuteProcedure<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", obj))
                     .OrderBy(orderingBy + " " + orderingDirection)
                     .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
   
        public async Task<proc_GetEmployeeDetail_Result> GetEmployee(long? idHRCompany, long? idHREmployee)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
            };
                return (await ExecuteProcedure<proc_GetEmployeeDetail_Result>("EXEC proc_GetEmployeeDetail @paramIdHREmployee,@paramIdHRCompany", obj)).FirstOrDefault();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<HREmployee>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await FindAllAsync(x => x.HREmployeeName.Contains(searchKey.ToUpper()) || searchKey == "");
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<IPagedList<HREmployee>> GetsByEmployee(long? idEmployee, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await FindAllAsync(x => x.IdParent_HREmployee == idEmployee && (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<HREmployee>> GetsByCompany(long? idHRCompany, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await FindAllAsync(x => x.IdHRCompany == idHRCompany && (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<bool> CheckUserNameAsync(string UserName)
        {
            try
            {
                var model = await ExecuteProcedure<Login>("select * from Logins where UserName=@p1", new SqlParameter() { ParameterName = "@p1", SqlDbType = SqlDbType.NVarChar, Value = UserName });
                return model.Any();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IEnumerable<proc_GetEmployeeCountByGender_Result>> GetsEmployeeCountByGender(long? idHRCompany, int? idRoleType)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType}
                };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetEmployeeCountByGender_Result>("EXEC proc_GetEmployeeCountByGender @paramIdHRCompany,@paramIdRoleType", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IEnumerable<proc_GetEmployeeTodayStatusForDashboard_Result>> GetEmployeeTodayStatusForDashboard(long? idHRCompany, int? idRoleType)
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType}
                };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetEmployeeTodayStatusForDashboard_Result>("EXEC proc_GetEmployeeTodayStatusForDashboard @paramIdHRCompany,@paramIdRoleType", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task DeleteEmployee(long? idHREmployee)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
                };
                await UnitOfWork.Db.Database.ExecuteSqlCommandAsync("EXEC proc_DeleteHREmployee @paramIdHREmployee", obj);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<int> GetEmployeeCount(long? idHRCompany, int? idRoleType)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType}
                };
                return await UnitOfWork.Db.Database.SqlQuery<int>("SELECT * FROM ifc_GetEmployeeCount(@paramIdHRCompany,@paramIdRoleType)", obj).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<int> GetEmployeeCountNew(long? idHRCompany, int? idRoleType)
        {
            try
            {
                return await UnitOfWork.Db.Database.SqlQuery<int>("SELECT count(*) EmployeeCount FROM HREmployeeJobHistory WITH(NOLOCK) where iscurrentStatus=1 and IdHREmployeeJobStatus not in (5,7,17,19) and ExpiryDate is null").FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<List<proc_getdailyindex_Result>> GetTodayStatusForDashboard(long? idHRCompany, int? idRoleType)
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType}
                };
                return await UnitOfWork.Db.Database.SqlQuery<proc_getdailyindex_Result>("EXEC proc_getdailyindex @paramIdHRCompany,@paramIdRoleType", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        public bool CheckIdhremployee(long? RequestedId, long? Idcompany,long? IdCompanyDivision, long? IdRoleType)
        {
            try
            {
                var HremployeeID = this.UnitOfWork.Db.Set<HREmployeeJobHistory>().Where(x => 
                x.IdHRCompany == Idcompany
                && x.IdHREmployee == RequestedId
                && x.IsCurrentStatus==true
                ).ToList();
                var active = HremployeeID.Where(x => x.IsCurrentStatus == true).ToList();
                
                //here we check if that emmployee only register
                var OnlyREgisterEmployee = this.UnitOfWork.Db.Set<HREmployee>().Where(x => x.Id == RequestedId).ToList();

                var NoJobHisory= new List<HREmployeeJobHistory>();
                if (IdRoleType == 4)
                {
                    NoJobHisory = this.UnitOfWork.Db.Set<HREmployeeJobHistory>().Where(x => x.IdHREmployee == RequestedId && x.IdHRCompanyDivision == IdCompanyDivision).ToList();
                }
                else
                {
                    NoJobHisory = this.UnitOfWork.Db.Set<HREmployeeJobHistory>().Where(x => x.IdHREmployee == RequestedId).ToList();
                }
                //End of check if that emmployee only register

                //check if register but not inserted on jobhistrory

                var okRegEmp = OnlyREgisterEmployee.Count();
                var Nojob = NoJobHisory.Count();

                //End of check if register but not inserted on jobhistrory

                var num = active.Count();
                if (num>0)
                {
                    return true;

                }
                if (okRegEmp > 0 && Nojob == 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch
            {
                return false;
            }
        }

        public int Idroletype(int? idrole)
        {
            var IdRoleType = this.UnitOfWork.Db.Set<HREmployeeRole>().Where(x => x.Id == idrole).Select(x => x.IdRoleType).FirstOrDefault();
            return IdRoleType;


        }

        #region Section_Admin

        public virtual async Task<int> GetSectionEmployeeCount(long? idHRCompany, int? idRoleType, long? idCompanyDivision)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdCompanyDivision", SqlDbType = SqlDbType.Int, Value = idCompanyDivision},
                };
                return await UnitOfWork.Db.Database.SqlQuery<int>("SELECT * FROM ifc_GetSectionEmployeeCount(@paramIdHRCompany,@paramIdRoleType,@paramIdCompanyDivision)", obj).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual async Task<IEnumerable<proc_GetEmployeeCountByGender_Result>> GetsSectionEmployeeCountByGender(long? idHRCompany, int? idRoleType, long? idCompanyDivision)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdCompanyDivision", SqlDbType = SqlDbType.Int, Value = idCompanyDivision},
                };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetEmployeeCountByGender_Result>("SELECT * FROM ifc_GetSectionEmployeeCountByGender(@paramIdHRCompany,@paramIdCompanyDivision)", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        #endregion
    }
}
