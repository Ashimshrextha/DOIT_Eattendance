using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
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
    public class HRCompanyNotificationController : BaseController
    {
        // GET: CompanyManagement/HRCompanyNotification

        private readonly HRCompanyNotificationServices _HRCompanyNotificationServices;

        public HRCompanyNotificationController()
        {
            _HRCompanyNotificationServices = new HRCompanyNotificationServices(this._unitOfWork);
        }
		[NonAction]
		public Expression<Func<HRCompanyNotification, bool>> Condition(string searchKey = "")
		{
			Expression<Func<HRCompanyNotification, bool>> retutndata = (x => false);

			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => (x.NotificationTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2) retutndata = (x => x.IdHRCompany == SessionDetail.IdHRCompany && (x.NotificationTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else retutndata = (x => false);

			return retutndata;
		}
		[AuthorizeUser]
		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRCompanyNotificationViewModelList
                {
                    DBModelList = await _HRCompanyNotificationServices.GetPagedList(Condition(),pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    BreadCrumbTitle = "Company Notification",
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

		[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ListHRCompanyNotificationAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyNotificationViewModelList
                {
					SessionDetails = SessionDetail,
					DBModelList = await _HRCompanyNotificationServices.GetPagedList(Condition(searchKey),pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    BreadCrumbActionName = "_ListHRCompanyNotificationAsync",
                    BreadCrumbTitle = "Company Notification",
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
		public async Task<ActionResult> _CreateHRCompanyNotificationAsync()
        {
            try
            {
                var Session = SessionDetail;
                return PartialView(new HRCompanyNotificationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    BreadCrumbActionName = "_CreateHRCompanyNotificationAsync",
                    FormModelName = "HRCompanyNotification",
                    ModalTitle = "Create New Company Notification",
                    BreadCrumbTitle = "Company Notification",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, Session.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, Session.IdHRCompany),
                    DataModel = new HRCompanyNotificationModel { Id = 0,IdNoticeBy =Session.IdHREmployee ??0}
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ReadHRCompanyNotificationAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var Session = SessionDetail;
                var DataModel = await _HRCompanyNotificationServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyNotificationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCompanyNotificationAsync",
                    FormModelName = "HRCompanyNotification",
                    ModalTitle = "View Company Notification Detail",
                    BreadCrumbTitle = "Company Notification",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, DataModel.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, DataModel.IdHRCompany),
                    DataModel = DataModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _UpdateHRCompanyNotificationAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var Session = SessionDetail;
                var DataModel = await _HRCompanyNotificationServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyNotificationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanyNotificationAsync",
                    FormModelName = "HRCompanyNotification",
                    ModalTitle = "Update Existing Company Notification",
                    BreadCrumbTitle = "System Structure",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, DataModel.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, DataModel.IdHRCompany),
                    DataModel = DataModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _DeleteHRCompanyNotificationAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var Session = SessionDetail;
                var DataModel = await _HRCompanyNotificationServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyNotificationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRCompanyNotification",
                    ModalTitle = "Delete Company Notification",
                    BreadCrumbTitle = "Company Notification",
                    BreadCrumbActionName = "_DeleteHRCompanyNotificationAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, DataModel.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, DataModel.IdHRCompany),
                    DataModel = DataModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ExportHRCompanyNotificationAsync()
        {
            try
            {
                var model = await _HRCompanyNotificationServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("HRCompanyNotification", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _PrintHRCompanyNotificationAsync()
        {
            try
            {
                return PartialView(new HRCompanyNotificationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyNotification",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification",
                    BreadCrumbActionName = "_PrintHRCompanyNotificationAsync",
                    FormModelName = "HRCompanyNotification",
                    ModalTitle = "Print Company Notification List",
                    BreadCrumbTitle = "Company Notification",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRCompanyNotificationServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHRCompanyNotificationAsync(HRCompanyNotificationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyNotification(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
		[ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanyNotificationAsync(HRCompanyNotificationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyNotification(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
		[ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCompanyNotificationAsync(HRCompanyNotificationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyNotification(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyNotification(HRCompanyNotificationViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyNotification";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyNotification";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, viewModel.DataModel.IdHRCompany);
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyNotificationAsync";
                        viewModel.ModalTitle = "Create New Company Notification";
                        viewModel.FormModelName = "HRCompanyNotification";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyNotificationAsync";
                        viewModel.ModalTitle = "Update Existing Company Notification";
                        viewModel.FormModelName = "HRCompanyNotification";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRCompanyNotificationAsync";
                        viewModel.ModalTitle = "Delete Existing Company Notification";
                        viewModel.FormModelName = "HRCompanyNotification";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRCompanyNotificationAsync";
                        viewModel.ModalTitle = "Print Company Notification  List";
                        viewModel.FormModelName = "HRCompanyNotification";
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
        protected async Task<ActionResult> CRUDHRCompanyNotification(HRCompanyNotificationViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCompanyNotification(viewModel, cRUDType);
                }
                await _HRCompanyNotificationServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyNotification(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyNotificationAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}