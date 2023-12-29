using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRCompanySystemSupportController : BaseController
    {
        // GET: CompanyManagement/HRCompanySystemSupport

        private readonly HRCompanySystemSupportServices _hRCompanySystemSupportServices;

        public HRCompanySystemSupportController()
        {
            _hRCompanySystemSupportServices = new HRCompanySystemSupportServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HRCompanySystemSupport, bool>> Condition(string searchKey = "")
        {
            Expression<Func<HRCompanySystemSupport, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IsActive && x.ContactPerson.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    x.HRCompany.HRCompanyCode.HRCompanyCodeTitle.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    x.HRCompany.CompanyNameNP.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                     x.MobileNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                      x.PhoneNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                       x.Email.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    searchKey == "");
                case (int)RoleType.Admin:
                    return returnData = (x => x.IsActive && x.ContactPerson.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    x.HRCompany.HRCompanyCode.HRCompanyCodeTitle.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                      x.HRCompany.CompanyNameNP.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                     x.MobileNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                      x.PhoneNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                       x.Email.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    searchKey == "");
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IsActive && x.ContactPerson.ToUpper().Contains(searchKey.ToUpper().ToString())||
                    x.HRCompany.HRCompanyCode.HRCompanyCodeTitle.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                     x.HRCompany.CompanyNameNP.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                     x.MobileNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                      x.PhoneNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                       x.Email.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    searchKey == "");
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IsActive && x.ContactPerson.ToUpper().Contains(searchKey.ToUpper().ToString())|| 
                    x.HRCompany.HRCompanyCode.HRCompanyCodeTitle.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                     x.HRCompany.CompanyNameNP.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                     x.MobileNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                      x.PhoneNumber.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                       x.Email.ToUpper().Contains(searchKey.ToUpper().ToString()) ||
                    searchKey == "");
                default:
                    return returnData;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRCompanySystemSupportViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanySystemSupportServices.GetPagedList(Condition(), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    BreadCrumbTitle = "सम्पर्क कर्मचारी ",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanySystemSupportAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanySystemSupportViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanySystemSupportServices.GetPagedList(Condition(searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    BreadCrumbActionName = "_ListHRCompanySystemSupportAsync",
                    BreadCrumbTitle = "Support Employee",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanySystemSupportAsync()
        {
            try
            {
                return PartialView(new HRCompanySystemSupportViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    BreadCrumbActionName = "_CreateHRCompanySystemSupportAsync",
                    FormModelName = "HRCompanySystemSupport",
                    ModalTitle = "नयाँ सम्पर्क कर्मचारी थप्नुहोस्",
                    BreadCrumbTitle = "Support Employee",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, SessionDetail.IdHRCompany),
                    DataModel = new HRCompanySystemSupportModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanySystemSupportAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanySystemSupportServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanySystemSupportViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCompanySystemSupportAsync",
                    FormModelName = "HRCompanySystemSupport",
                    ModalTitle = "सम्पर्क कर्मचारी विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Support Employee",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, model.IdHRCompany),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanySystemSupportAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanySystemSupportServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanySystemSupportViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanySystemSupportAsync",
                    FormModelName = "HRCompanySystemSupport",
                    ModalTitle = "सम्पर्क कर्मचारी सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "System Structure",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, model.IdHRCompany),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanySystemSupportAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanySystemSupportServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanySystemSupportViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRCompanySystemSupport",
                    ModalTitle = "सम्पर्क कर्मचारी मेटाउनुहोस्",
                    BreadCrumbTitle = "Support Employee",
                    BreadCrumbActionName = "_DeleteHRCompanySystemSupportAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, model.IdHRCompany),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCompanySystemSupportAsync()
        {
            try
            {
                var model = await _hRCompanySystemSupportServices.FindAllAsync(Condition());
                var data = model.Select(x=>new
                {
                    OfficeCode=x.HRCompany.HRCompanyCode.HRCompanyCodeTitle,
                    CompanyName=x.HRCompany.CompanyNameNP,
                    ContactPerson=x.ContactPerson,
                    MobileNumber=x.MobileNumber,
                    PhoneNumber=x.PhoneNumber,
                    Email=x.Email,
                    
                });
                return await ExportToExcel("HRCompanySystemSupport", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanySystemSupportAsync()
        {
            try
            {
                return PartialView(new HRCompanySystemSupportViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanySystemSupport",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport",
                    BreadCrumbActionName = "_PrintHRCompanySystemSupportAsync",
                    FormModelName = "HRCompanySystemSupport",
                    ModalTitle = "सम्पर्क कर्मचारी सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Support Employee",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRCompanySystemSupportServices.FindAllAsync(Condition())
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
        public async Task<ActionResult> _CreateHRCompanySystemSupportAsync(HRCompanySystemSupportViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanySystemSupport(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanySystemSupportAsync(HRCompanySystemSupportViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanySystemSupport(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCompanySystemSupportAsync(HRCompanySystemSupportViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanySystemSupport(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanySystemSupport(HRCompanySystemSupportViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanySystemSupport";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanySystemSupport";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanySystemSupportAsync";
                        viewModel.ModalTitle = "नयाँ सम्पर्क कर्मचारी थप्नुहोस्";
                        viewModel.FormModelName = "HRCompanySystemSupport";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanySystemSupportAsync";
                        viewModel.ModalTitle = "सम्पर्क कर्मचारी सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRCompanySystemSupport";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRCompanySystemSupportAsync";
                        viewModel.ModalTitle = "सम्पर्क कर्मचारी मेटाउनुहोस्";
                        viewModel.FormModelName = "HRCompanySystemSupport";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRCompanySystemSupportAsync";
                        viewModel.ModalTitle = "सम्पर्क कर्मचारी सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HRCompanySystemSupport";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHRCompanySystemSupport(HRCompanySystemSupportViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCompanySystemSupport(viewModel, cRUDType);
                }
                await _hRCompanySystemSupportServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanySystemSupport(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanySystemSupportAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}