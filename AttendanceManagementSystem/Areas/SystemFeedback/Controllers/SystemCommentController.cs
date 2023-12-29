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
using SystemModels.Feedback;
using SystemServices.SystemFeedback;
using SystemViewModels.SystemFeedback;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemFeedback.Controllers
{
    public class SystemCommentController : BaseController
    {
        // GET: SystemFeedback/SystemComment

        private readonly SystemCommentServices _hrSystemCommentServices;

        public SystemCommentController()
        {
            this._hrSystemCommentServices = new SystemCommentServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<SystemComment, bool>> Condition(string searchKey = "")
        {
            Expression<Func<SystemComment, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.Comment.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == "");

            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2) retutndata = (x => x.IdHRCompany == SessionDetail.IdHRCompany && (x.Comment.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

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
                return View(new SystemCommentViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hrSystemCommentServices.GetPagedList(Condition(), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "SystemFeedback",
                    BreadCrumbController = "SystemComment",
                    BreadCrumbBaseURL = "SystemFeedback/SystemComment",
                    BreadCrumbTitle = "System Comment",
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
        public async Task<ActionResult> _ListSystemCommentAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemCommentViewModelList
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemFeedback",
                    BreadCrumbController = "SystemComment",
                    BreadCrumbBaseURL = "SystemFeedback/SystemComment",
                    BreadCrumbActionName = "_ListSystemCommentAsync",
                    BreadCrumbTitle = "System Comment",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemCommentResponseAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var model = await _hrSystemCommentServices.GetByIdAsync(idHRCompany);
                return PartialView(new SystemCommentResponseViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "SystemFeedback",
                    BreadCrumbController = "SystemComment",
                    BreadCrumbBaseURL = "SystemFeedback/SystemComment",
                    BreadCrumbActionName = "_ReadSystemCommentResponseAsync",
                    FormModelName = "SystemCommentResponse",
                    ModalTitle = "नयाँ प्रणाली प्रतिक्रिया थप्नुहोस्",
                    BreadCrumbTitle = "System Comment",
                    CRUDAction = CRUDType.READ,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateSystemCommentAsync()
        {
            try
            {
                return PartialView(new SystemCommentViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "SystemFeedback",
                    BreadCrumbController = "SystemComment",
                    BreadCrumbBaseURL = "SystemFeedback/SystemComment",
                    BreadCrumbActionName = "_CreateSystemCommentAsync",
                    FormModelName = "SystemComment",
                    ModalTitle = "नयाँ प्रणाली प्रतिक्रिया थप्नुहोस्",
                    BreadCrumbTitle = "System Comment",
                    CRUDAction = CRUDType.CREATE,
                    PriorityEnum = Priority.High,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, SessionDetail.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, SessionDetail.IdHREmployee),
                    DataModel = new SystemCommentModel { Id = 0 }

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateSystemCommentAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrSystemCommentServices.GetModelByIdAsync(id);
                return PartialView(new SystemCommentViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemFeedback",
                    BreadCrumbController = "SystemComment",
                    BreadCrumbBaseURL = "SystemFeedback/SystemComment",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanySystemSupportAsync",
                    FormModelName = "SystemComment",
                    ModalTitle = "प्रणाली प्रतिक्रिया सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "System Comment",
                    CRUDAction = CRUDType.UPDATE,
                    PriorityEnum = Priority.High,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, model.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, SessionDetail.IdHREmployee),
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
        public async Task<ActionResult> _DeleteSystemCommentAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrSystemCommentServices.GetModelByIdAsync(id);
                return PartialView(new SystemCommentViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "SystemFeedback",
                    BreadCrumbController = "SystemComment",
                    BreadCrumbBaseURL = "SystemFeedback/SystemComment",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "SystemComment",
                    ModalTitle = "प्रणाली प्रतिक्रिया मेटाउनुहोस्",
                    BreadCrumbTitle = "System Comment",
                    BreadCrumbActionName = "_DeleteSystemCommentAsync",
                    CRUDAction = CRUDType.DELETE,
                    PriorityEnum = Priority.High,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, model.IdHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, SessionDetail.IdHREmployee),
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
                var model = await _hrSystemCommentServices.FindAllAsync(Condition());
                return await ExportToExcel("SystemComment", model);
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
        public async Task<ActionResult> _CreateSystemCommentAsync(SystemCommentViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemComment(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateSystemCommentAsync(SystemCommentViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemComment(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteSystemCommentAsync(SystemCommentViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemComment(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceSystemComment(SystemCommentViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "SystemFeedback";
                viewModel.BreadCrumbController = "SystemComment";
                viewModel.BreadCrumbBaseURL = "CompanyManagSystemFeedbackement/SystemComment";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateSystemCommentAsync";
                        viewModel.ModalTitle = "नयाँ प्रणाली प्रतिक्रिया थप्नुहोस्";
                        viewModel.FormModelName = "SystemComment";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateSystemCommentAsync";
                        viewModel.ModalTitle = "प्रणाली प्रतिक्रिया सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "SystemComment";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteSystemCommentAsync";
                        viewModel.ModalTitle = "प्रणाली प्रतिक्रिया मेटाउनुहोस्";
                        viewModel.FormModelName = "SystemComment";
                        viewModel.CRUDAction = CRUDType.DELETE;
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
        protected async Task<ActionResult> CRUDSystemComment(SystemCommentViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemComment(viewModel, cRUDType);
                }
                await _hrSystemCommentServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemComment(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListSystemCommentAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}

