using AttendanceManagementSystem.App_Auth;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SystemServices.CompanyManagement;
using SystemServices.EmployeeManagement;
using SystemServices.SystemAuthentication;
using SystemServices.SystemSecurity;
using SystemStores.GlobalModels;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using SystemViewModels.SystemAuthentication;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SystemAuthenticationServices _systemAuthenticationServices;
        private readonly HRCompanyServices _hRCompanyServices;
        private readonly HREmployeeServices _hREmployeeServices;
        private readonly HREmployeeAttendanceHistoryServices _hREmployeeAttendanceHistoryServices;
        private readonly HRDeviceServices _hRDeviceServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BaseController _baseController;
        private readonly LoginsServices _loginsServices;



        public AccountController() : this(null, null)
        {
            _unitOfWork = new UnitOfWork();
            _systemAuthenticationServices = new SystemAuthenticationServices(_unitOfWork);
            this._hRCompanyServices = new HRCompanyServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
            this._hREmployeeAttendanceHistoryServices = new HREmployeeAttendanceHistoryServices(this._unitOfWork);
            this._hRDeviceServices = new HRDeviceServices(this._unitOfWork);
            this._loginsServices = new LoginsServices(this._unitOfWork);
            this._baseController = new BaseController();
        }


        public AccountController(IFormsAuthentication formsAuth, MembershipProvider provider)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationWrapper();
            Provider = provider ?? Membership.Provider;
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Login()
        {
            try
            {
                var todayDate = DateTime.Now.Date;
                return View(new LoginViewModel
                {
                    //DbGender = await _hREmployeeServices.GetsEmployeeCountByGender(0, 0),
                    //DbEmployeeTodayStatus = await _systemAuthenticationServices.GetEmployeeTodayStatusForDashboard(0, 0),
                    //ActiveDeviceCount = await _hRDeviceServices.CountAsync(x => x.IsActive && x.IsRegister),
                    //ActiveOfficeCount = await _hRCompanyServices.GetActiveOfficeCount(),
                    //DeviceCount = await _hRDeviceServices.CountAsync(x => x.IsActive),
                    //EmployeeCount = await _hREmployeeServices.GetEmployeeCount(0, 0),
                    //EmployeePresentCount = await _hREmployeeAttendanceHistoryServices.CountAsync(x => x.AttendanceDate == todayDate),
                    //OfficeCount = await _hRCompanyServices.CountAsync(x => x.IsActive)
                });
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.ModelError);
                }
                if (string.IsNullOrEmpty(loginViewModel.DataModel.UserName))
                {
                    Response.StatusCode = 350;
                    return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.UserNameEmpty);
                }
                if (string.IsNullOrEmpty(loginViewModel.DataModel.Password))
                {
                    Response.StatusCode = 350;
                    return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.PasswordEmpty);
                }
                var result = await _systemAuthenticationServices.PasswordSignInAsync(loginViewModel.DataModel.UserName, loginViewModel.DataModel.Password);
                


                switch (result)
                {
                    case SignInStatus.Success:
                        FormsAuth.SetAuthCookie(loginViewModel.DataModel.UserName, true);
                        bool FirstLoginExists =  _baseController.checkingData(loginViewModel.DataModel.UserName);
                        if (FirstLoginExists)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("ChangePassword", "Account");
                        }
                    case SignInStatus.LockedOut:
                        return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.LockedOut);
                    case SignInStatus.Failure:
                        return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.Failure);
                    default:
                        return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.Exception);
                }
            }
            catch
            {
                return await this.RETSourceLoginAsync(loginViewModel, SignInStatus.Exception);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                ClearCache();
                return RedirectToAction("Login");
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [AuthenticationFilter]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> ChangePassword()
        {
            try
            {
                await Task.WhenAll();
                return PartialView(new ChangePasswordViewModel
                {
                    UserModel = (UserSessionModel)HttpContext.Session["UserSession"]
                });
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    model.UserModel = (UserSessionModel)Session["UserSession"];
                    return PartialView(model);
                }
                var result = await _systemAuthenticationServices.PasswordSignInAsync(model.UserModel.UserName, model.CurrentPassword);
                switch (result)
                {
                    case SignInStatus.Success:
                        string passwordMD5 = Encryption(model.Password);
                        string passwordSHA = Get_HASH_SHA512(model.Password, model.UserModel.UserName, 256);
                        await _systemAuthenticationServices.ChangePasswordAsync(model.UserModel.UserName, passwordMD5, passwordSHA);
                        return RedirectToAction("Index", new { controller = "Home" });
                    case SignInStatus.Failure:
                    default:
                        Response.StatusCode = 350;
                        ModelState.AddModelError("CurrentPassword", "तपाईको पुरानो पासवर्ड मेल खाएन");
                        model.UserModel = (UserSessionModel)Session["UserSession"];
                        return PartialView(model);
                };
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            return PartialView(model);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model, string Email)
        {
            var IdHremployee = _baseController.Getidhremployee(model.Username);
            var loginID = _baseController.GetLoginID(model.Username);
            var EmailAddress = _baseController.GetEmailByIdhremployee(IdHremployee);
            string RandomPassword = RandomNumber(1000, 9000).ToString();

            model.loginModel = await _loginsServices.GetByIdAsync(loginID);
            //Update new Password
            model.loginModel.Password_MD5 = Encryption(RandomPassword);
            model.loginModel.Password_SHA = Get_HASH_SHA512(RandomPassword, model.Username, 256);
            await _loginsServices.UpdateAsync(model.loginModel);
            await _unitOfWork.SaveAsync();
            //Email sending With Message of new Password
            var Emailsubject = "Password Reset";
            var EmailMessage = $"Regarding Your Forgot Password : Dear {model.Username} ji, Your New Password is { RandomPassword}";
            _baseController.SendEmail(EmailAddress, Emailsubject, EmailMessage);

            return RedirectToAction("login");
        }


        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Register(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.HeaderTitle = "Register For Attendance Management System";
                    return View("Login", model);
                }
                var result = await _systemAuthenticationServices.RegisterUserAsync(model.DataModelRegister.UserName, model.DataModelRegister.Password, model.DataModelRegister.Email, model.DataModelRegister.Mobile);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Login");
                    case SignInStatus.Failure:
                        return RedirectToAction("Login");
                }
                return View();
            }
            catch (Exception exp)
            {
                return View(exp.Message);
            }
        }

        public ActionResult AccessDenied()
        {
            return PartialView();
        }


        void ClearCache()
        {
            FormsAuth.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

  
        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public MembershipProvider Provider
        {
            get;
            private set;
        }

        public interface IFormsAuthentication
        {
            void SetAuthCookie(string userName, bool createPersistentCookie);

            void SignOut();
        }

        public class FormsAuthenticationWrapper : IFormsAuthentication
        {
            public void SetAuthCookie(string userName, bool createPersistentCookie)
            {
                FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            }

            public void SignOut()
            {
                FormsAuthentication.SignOut();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> ValidateUserNameAsync(string userName)
        {
            try
            {
                return Json(await this._systemAuthenticationServices.ValidateUserNameAsync(userName) ? "success" : "unsuccess", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("unsuccess", JsonRequestBehavior.AllowGet);
            }
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceLoginAsync(LoginViewModel viewModel, SignInStatus signInStatus)
        {
            try
            {
                //var todayDate = DateTime.Now.Date;
                //viewModel.DbGender = await _hREmployeeServices.GetsEmployeeCountByGender(0, 0);
                //viewModel.DbEmployeeTodayStatus = await _systemAuthenticationServices.GetEmployeeTodayStatusForDashboard(0, 0);
                ////viewModel.ActiveDeviceCount = await _hRDeviceServices.CountAsync(x => x.IsActive && x.IsRegister);
                //viewModel.ActiveOfficeCount = await _hRCompanyServices.GetActiveOfficeCount();
                ////viewModel.DeviceCount = await _hRDeviceServices.CountAsync(x => x.IsActive);
                //viewModel.EmployeeCount = await _hREmployeeServices.GetEmployeeCount(0, 0);
                //viewModel.EmployeePresentCount = await _hREmployeeAttendanceHistoryServices.CountAsync(x => x.AttendanceDate == todayDate);
                //viewModel.OfficeCount = await _hRCompanyServices.CountAsync(x => x.IsActive);


                var todayDate = DateTime.Now.Date;
                viewModel.DbGender = await _hREmployeeServices.GetsEmployeeCountByGender(0, 0);
                viewModel.DbEmployeeTodayStatus = await _systemAuthenticationServices.GetEmployeeTodayStatusForDashboard(0, 0);
                viewModel.ActiveDeviceCount = await _hRDeviceServices.CountAsync(x => x.IsActive && x.IsRegister);
                viewModel.ActiveOfficeCount = await _hRCompanyServices.GetActiveOfficeCount();
                viewModel.DeviceCount = await _hRDeviceServices.CountAsync(x => x.IsActive);
                viewModel.EmployeeCount = await _hREmployeeServices.GetEmployeeCount(0, 0);
                viewModel.EmployeePresentCount = await _hREmployeeAttendanceHistoryServices.CountAsync(x => x.AttendanceDate == todayDate);
                viewModel.OfficeCount = await _hRCompanyServices.CountAsync(x => x.IsActive);

                switch (signInStatus)
                {
                    case SignInStatus.ModelError:
                        return View(viewModel);
                    case SignInStatus.UserNameEmpty:
                        return View(viewModel);
                    case SignInStatus.PasswordEmpty:
                        return View(viewModel);
                    case SignInStatus.Exception:
                        ModelState.AddModelError("", "कृपया सूचना प्रविधि विभागमा तुरून्त सम्पर्क गर्नुहोला");
                        return View(viewModel);
                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("", "गलत प्रयोगकर्तानाम र पासवर्डले गर्दा तीन मिनेटको लागि बन्द गरिएको छ");
                        return View(viewModel);
                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "कृपया प्रयोगकर्तानाम र पासवर्डले सही राख्नुहोस!!!");
                        return View(viewModel);
                    default:
                        ModelState.AddModelError("", "अवैध लग इन प्रयास");
                        return View(viewModel);
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}