using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.SystemAuthentication;
using SystemServices.EmployeeManagement;
using SystemServices.SystemAuthentication;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeLoginController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeLogin

        private readonly LoginsServices _loginsServices;
        private readonly HREmployeeServices _hREmployeeservices;
        private readonly MembershipProviderServices _MembershipProviderServices;


        public HREmployeeLoginController()
        {
            this._loginsServices = new LoginsServices(this._unitOfWork);
            this._hREmployeeservices = new HREmployeeServices(this._unitOfWork);
            this._MembershipProviderServices = new MembershipProviderServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<Login, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<Login, bool>> retutndata = (x => false);
            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.MembershipProviders.FirstOrDefault().IdHREmployee == idHREmployee && (x.UserName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 4 ||this.SessionDetail.IdRoleType == 2) retutndata = (x => x.MembershipProviders.FirstOrDefault().IdHREmployee == idHREmployee && x.MembershipProviders.FirstOrDefault().HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.UserName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else retutndata = (x => x.MembershipProviders.FirstOrDefault().IdHREmployee == this.SessionDetail.IdHREmployee && (x.UserName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            return retutndata;
        }


        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeLoginAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLoginViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _loginsServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    BreadCrumbActionName = "_ListWrapperHREmployeeLoginAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeLoginAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLoginViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _loginsServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    BreadCrumbActionName = "_ListHREmployeeLoginAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeLoginAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _MembershipProviderServices.FindAsync(x => x.IdHREmployee == idHREmployee);
                return PartialView(new HREmployeeLoginViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    BreadCrumbActionName = "_CreateHREmployeeLoginAsync",
                    FormModelName = "HREmployeeLogin",
                    ModalTitle = " नयाँ लगइन थप्नुहोस",
                    BreadCrumbTitle = "Employee Login",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, model.HREmployeeRole.IdHRCompany),
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage),
                    IdHREmployee = idHREmployee,
                    IdHRCompany = model.HREmployeeRole.IdHRCompany,
                    DataModel = new LoginsModel { Id = model.Login.Id, UserName = model.Login.UserName, Language = model.Login.Language }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeLoginAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _loginsServices.GetModelByIdAsync(id);
                var modelMembership = await _MembershipProviderServices.GetModelFindAsync(x => x.IdLogin == id);
                var EmployeeHistory = await _hREmployeeservices.GetByIdAsync(modelMembership.IdHREmployee);
                return PartialView(new HREmployeeLoginViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeLoginAsync",
                    FormModelName = "HREmployeeLogin",
                    ModalTitle = "कर्मचारी लगइन विवरण हेर्नुहोस",
                    BreadCrumbTitle = "Employee Login",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DataModelMembership = modelMembership,
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, EmployeeHistory.IdHRCompany),
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeLoginAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _loginsServices.GetModelByIdAsync(id);
                var modelMembership = await _MembershipProviderServices.GetModelFindAsync(x => x.IdLogin == id);
                var EmployeeHistory = await _hREmployeeservices.GetByIdAsync(modelMembership.IdHREmployee);
                return PartialView(new HREmployeeLoginViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeLoginAsync",
                    FormModelName = "HREmployeeLogin",
                    ModalTitle = "कर्मचारी लगइन सम्पादन गर्नुहोस",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DataModelMembership = modelMembership,
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, EmployeeHistory.IdHRCompany),
                    //DDLRole = await GetIntStringPairModel(PairModelTitle.Role),
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ResetPasswordAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _loginsServices.GetModelByIdAsync(id);
                var modelMembership = await _MembershipProviderServices.GetModelFindAsync(x => x.IdLogin == id);
                var EmployeeHistory = await _hREmployeeservices.GetByIdAsync(modelMembership.IdHREmployee);
                return PartialView(new HREmployeeLoginViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ResetPasswordAsync",
                    FormModelName = "HREmployeeLogin",
                    ModalTitle = "पासवर्ड रिसेट गर्न निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Employee Login",
                    CRUDAction = CRUDType.RESET,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DataModelMembership = modelMembership,
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, EmployeeHistory.IdHRCompany),
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeLoginAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _loginsServices.GetModelByIdAsync(id);
                var modelMembership = await _MembershipProviderServices.GetModelFindAsync(x => x.IdLogin == id);
                var EmployeeHistory = await _hREmployeeservices.GetByIdAsync(modelMembership.IdHREmployee);
                return PartialView(new HREmployeeLoginViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeLogin",
                    ModalTitle = "कर्मचारी लगइन हटाउनुहोस",
                    BreadCrumbActionName = "_DeleteHREmployeeLoginAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DataModelMembership = modelMembership,
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, EmployeeHistory.IdHRCompany),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeLoginAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _loginsServices.FindAllAsync(Condition(idHREmployee));
                var data = model.Select(x=>new {
                    Language=x.Language,
                    UserName=x.UserName,
                    Role=x.MembershipProviders.FirstOrDefault().HREmployeeRole.HRRoleTitle,
                    LockOutSuccess=x.LockOutEnabled.ToString(),
                    LoginUnsucess=x.AccessFailedCount,
                });
                return await ExportToExcel("EmployeeLogin", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeLoginAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _loginsServices.FindAllAsync(Condition(idHREmployee));
                return PartialView(new HREmployeeLoginViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLogin",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLogin",
                    BreadCrumbActionName = "_PrintHREmployeeLoginAsync",
                    SessionDetails = SessionDetail,
                    FormModelName = "HREmployeeLogin",
                    ModalTitle = "कर्मचारी लगइन विवरण छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DBModelList = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHREmployeeLoginAsync(HREmployeeLoginViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLoginAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeLoginAsync(HREmployeeLoginViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLoginAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _ResetPasswordAsync(HREmployeeLoginViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLoginAsync(viewModel, CRUDType.RESET, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeLoginAsync(HREmployeeLoginViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLoginAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLoginAsync(HREmployeeLoginViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLoginHistory";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLoginHistory";
                viewModel.DDLRole = await GetIntStringPairModel(PairModelTitle.Role);
                viewModel.DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeLoginAsync";
                        viewModel.ModalTitle = "कर्मचारी लगइन विवरण हेर्नुहोस";
                        viewModel.FormModelName = "HREmployeeLogin";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeLoginAsync";
                        viewModel.ModalTitle = "कर्मचारी लगइन सम्पादन गर्नुहोस";
                        viewModel.FormModelName = "HREmployeeLogin";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.RESET:
                        viewModel.BreadCrumbActionName = "_ResetPasswordAsync";
                        viewModel.ModalTitle = "पासवर्ड रिसेट गर्न निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeLogin";
                        viewModel.CRUDAction = CRUDType.RESET;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeLoginAsync";
                        viewModel.ModalTitle = "कर्मचारी लगइन हटाउनुहोस";
                        viewModel.FormModelName = "HREmployeeLogin";
                        viewModel.CRUDAction = CRUDType.DELETE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeLoginAsync(HREmployeeLoginViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLoginAsync(viewModel, cRUDType);
                }
                var loginModel = await _loginsServices.GetByIdAsync(viewModel.DataModel.Id);
                switch (cRUDType)
                {

                    case CRUDType.RESET:
                        loginModel.Password_MD5 = Encryption("1234");
                        loginModel.Password_SHA = Get_HASH_SHA512("1234", viewModel.DataModel.UserName, 256);
                        loginModel.Language = viewModel.DataModel.Language;
                        loginModel.Mobile = viewModel.DataModel.Mobile;
                        loginModel.Email = viewModel.DataModel.Email;
                        break;
                    default:
                        loginModel.Language = viewModel.DataModel.Language;
                        loginModel.Mobile = viewModel.DataModel.Mobile;
                        loginModel.Email = viewModel.DataModel.Email;
                        break;
                }
                await _loginsServices.UpdateAsync(loginModel);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLoginAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLoginAsync", new { idHREmployee = viewModel.DataModelMembership.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}