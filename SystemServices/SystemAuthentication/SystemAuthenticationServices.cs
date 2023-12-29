using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SystemDatabase;
using SystemInterfaces.SystemAuthentication;
using SystemModels.SystemAuthentication;
using SystemStores.GlobalModels;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace SystemServices.SystemAuthentication
{
    public class SystemAuthenticationServices : BaseRepository<Login, LoginsModel>, ISystemAuthenticationServices
    {
        public SystemAuthenticationServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password)
        {
            try
            {
                var User = GetLoginFormat(userName, password);
                bool status = await Exits(x => x.UserName == userName && x.IsActive);
                if (status)
                {
                    status = await Exits(x => x.UserName == userName && x.Password_MD5 == User.Value && x.IsActive);
                    switch (status)
                    {
                        case true:
                            var result = await FindAsync(x => x.UserName == userName);
                            if (result.LockOutEnabled && result.LockOutEndDate < DateTime.Now)
                            {
                                result.LockOutEnabled = false;
                                result.AccessFailedCount = 0;
                                result.LockOutEndDate = null;
                                await UpdateAsync(result);
                                await this.UnitOfWork.SaveAsync();
                            }
                            if (result.LockOutEnabled && result.LockOutEndDate >= DateTime.Now)
                            {
                                return SignInStatus.LockedOut;
                            }
                            await SetSession(userName, User.Value, "");
                            return SignInStatus.Success;
                        case false:
                            var model = await FindAsync(x => x.UserName == userName);
                            model.AccessFailedCount += 1;
                            if (model.AccessFailedCount > 3)
                            {
                                model.LockOutEnabled = true;
                                model.LockOutEndDate = DateTime.Now.AddMinutes(3);
                            }
                            await UpdateAsync(model);
                            await this.UnitOfWork.SaveAsync();
                            if (model.LockOutEnabled && model.LockOutEndDate > DateTime.Now)
                                return SignInStatus.LockedOut;
                            else
                                return SignInStatus.Failure;
                        default:
                            return SignInStatus.Failure;
                    }
                }
                else return SignInStatus.Failure;
            }
            catch
            {
                return SignInStatus.Failure;
            }
        }

        private async Task SetSession(string userName, string passwordMD5, string passwordShA)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.NVarChar, Value= userName},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.NVarChar, Value = passwordMD5},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.NVarChar, Value = passwordShA},
            };
            var User = await ExecuteProcedure<proc_Authorization_Result>("Exec proc_Authorization @p1,@p2,@p3", myObjArray);
            var UserDetail = User.FirstOrDefault();
            UserSessionModel _modelSession = new UserSessionModel
            {
                IdLogin = UserDetail.IdLogin,
                UserName = UserDetail.UserName,
                Name = UserDetail.Name,
                ImageName = UserDetail.ImageName,
                Email = UserDetail.Email,
                Mobile = UserDetail.Mobile,
                Locked = UserDetail.Locked,
                LockOutEndDate = UserDetail.LockOutEndDate,
                IdEnroll = UserDetail.IdEnroll,
                Language = UserDetail.Language,
                IdGender = UserDetail.IdGender,
                IdMarital = UserDetail.IdMarital,
                Address = UserDetail.Address,
                DOB = UserDetail.DOB,
                IdHREmployee = UserDetail.IdHREmployee,
                IdHRCompany = UserDetail.IdHRCompany,
                HRCompanyName = UserDetail.HRCompanyName,
                ParentHRCompanyName = UserDetail.ParentCompanyNameNP,
                IdRoleType = UserDetail.IdRoleType,
                IdHREmployeeRole = UserDetail.IdHREmployeeRole,
                IdHREmployeeServiceType = UserDetail.IdHREmployeeServiceType,
                IdHRCompanyDivision = UserDetail.IdHRCompanyDivision,
                IdHREmployeeJobStatus = UserDetail.IdHREmployeeJobStatus,
                IsHead = UserDetail.IsHead,
                HRCompanyLogo = UserDetail.HRCompanyLogo
            };
            _modelSession.IdSessoin = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session["UserSession"] = _modelSession;
        }

        public async Task<SignInStatus> ChangePasswordAsync(string userName, string passwordMD5, string passwordSHA)
        {
            try
            {
                var model = await FindAsync(x => x.UserName == userName);
                model.Password_SHA = passwordSHA;
                model.Password_MD5 = passwordMD5;
                await UpdateAsync(model);
                await this.UnitOfWork.SaveAsync();
                return SignInStatus.Success;
            }
            catch
            {
                return SignInStatus.Failure;
            }
        }

        public async Task<SignInStatus> RegisterUserAsync(string userName, string password, string email, string mobile)
        {
            try
            {
                var model = GetLoginFormat(userName, password);
                await CommitSaveChangesAsync(new LoginsModel
                {
                    Id = model.Key,
                    UserName = userName,
                    Password_MD5 = model.Value,
                    Password_SHA = model.Identity,
                    Email = email,
                    Mobile = mobile,
                    IsActive = false,
                    LockOutEnabled = false,
                    TwoFactorEnabled = false,
                    IsEmailConfirmed = true,
                    AccessFailedCount = 0
                }, CRUDType.CREATE);
                return SignInStatus.Success;
            }
            catch (Exception)
            {
                return SignInStatus.Failure;
            }
        }

        public async Task<bool> CheckPermission(string Area, string Controller, string Action)
        {
            UserSessionModel model = ((UserSessionModel)HttpContext.Current.Session["UserSession"]);
            var params1 = new SqlParameter() { ParameterName = "@p1", SqlDbType = SqlDbType.Int, Value = model.IdHREmployeeRole };
            var params2 = new SqlParameter() { ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value = model.IdHREmployee };
            var params3 = new SqlParameter() { ParameterName = "@p3", SqlDbType = SqlDbType.NVarChar, Value = Area };
            if (model.IdHREmployeeRole == null) params1.Value = DBNull.Value;
            if (model.IdHREmployee == null) params2.Value = DBNull.Value;
            if (Area == null) params3.Value = DBNull.Value;
            object[] myObjArray = { params1, params2, params3, new SqlParameter() { ParameterName = "@p4", SqlDbType = SqlDbType.NVarChar, Value = Controller }, new SqlParameter() { ParameterName = "@p5", SqlDbType = SqlDbType.NVarChar, Value = Action } };
            return await ExecuteScalarFunction<bool>("select dbo.f_CheckPermission (@p1,@p2,@p3,@p4,@p5)", myObjArray);
        }

        private void Flush()
        {
            HttpContext.Current.Session.Remove("UserSession");
        }

        public async Task<bool> ValidateUserNameAsync(string userName)
        {
            try
            {
                return await Exits(x => x.UserName == userName && x.IsActive);
            }
            catch
            {
                return false;
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
    }
}
