using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HrEmployeeLeaveApprovedController : BaseController
    {
        // GET: EmployeeManagement/HrEmployeeLeaveApproved

        private readonly HREmployeeLeaveHistoryServices _HREmployeeLeaveHistoryServices;
        private readonly LeaveAppliedSummaryServices _leaveAppliedSummaryServices;

        public HrEmployeeLeaveApprovedController()
        {
            this._HREmployeeLeaveHistoryServices = new HREmployeeLeaveHistoryServices(this._unitOfWork);
            this._leaveAppliedSummaryServices = new LeaveAppliedSummaryServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeLeaveHistory, bool>> Condition(long? id = null, string searchKey = "")
        {
            Expression<Func<HREmployeeLeaveHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdLeaveStatus >= 2 && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee || this.SessionDetail.IdHREmployee == 0) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdLeaveStatus >= 2 && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdLeaveStatus >= 2 && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdLeaveStatus >= 2 && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeLeaveHistoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeLeaveHistoryServices.GetPagedList(Condition(id), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveApproved",
                    BreadCrumbTitle = "बिदा स्वीकृत",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = string.Empty
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHrEmployeeLeaveApprovedAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveHistoryViewModelList
                {
                    DBModelList = await _HREmployeeLeaveHistoryServices.GetPagedList(Condition(null, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveApproved",
                    BreadCrumbActionName = "_ListHREmployeeLeaveApprovedAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHrEmployeeLeaveApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_ReadHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveApproved",
                    ModalTitle = "बिदा स्वीकृत विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveStatusType),
                    LeaveAppliedModel = await this._leaveAppliedSummaryServices.GetAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHrEmployeeLeaveApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_UpdateHREmployeeLeaveApprovedAsync",
                    FormModelName = "HREmployeeLeaveApproved",
                    ModalTitle = "बिदा स्वीकृत विवरण सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus),
                    LeaveAppliedModel = await this._leaveAppliedSummaryServices.GetAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHrEmployeeLeaveApprovedAsync()
        {
            try
            {
                var model = await _HREmployeeLeaveHistoryServices.FindAllAsync(Condition(null));
                return await ExportToExcel("HREmployeeLeaveApproved", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHrEmployeeLeaveApprovedAsync(long? id)
        {
            try
            {
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveApproved",
                    BreadCrumbActionName = "_PrintHREmployeeLeaveApprovedAsync",
                    FormModelName = "HREmployeeLeaveApproved",
                    ModalTitle = "बिदा स्वीकृत सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Leave Approval",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    SessionDetails = SessionDetail,
                    CancelButtonText = CancelButtonText.Cancel,
                    LeaveAppliedModel = await this._leaveAppliedSummaryServices.GetAsync(id)
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
        public async Task<ActionResult> _UpdateHrEmployeeLeaveApprovedAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHrEmployeeLeaveApproved(viewModel, CRUDType.UPDATE, formCollection);
        }


        [NonAction]
        protected async Task<ActionResult> RETSourceHrEmployeeLeaveApproved(HREmployeeLeaveHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLeaveApproved";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveApproved";
                viewModel.DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeLeaveApprovedAsync";
                        viewModel.ModalTitle = "बिदा स्वीकृत विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveApproved";
                        viewModel.CRUDAction = CRUDType.UPDATE;
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
        protected async Task<ActionResult> CRUDHrEmployeeLeaveApproved(HREmployeeLeaveHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHrEmployeeLeaveApproved(viewModel, cRUDType);
                }
                if (viewModel.LeaveAppliedModel.IdLeaveStatus == 2)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "स्वीकृत अथवा अस्वीकृत चयन गर्नुहोस");
                    return await RETSourceHrEmployeeLeaveApproved(viewModel, cRUDType);
                }

                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        var entity = await _HREmployeeLeaveHistoryServices.GetByIdAsync(viewModel.LeaveAppliedModel.IdLeavehistory);
                        entity.ApprovalRemark = viewModel.LeaveAppliedModel.RecommandationRemark;
                        entity.IdLeaveStatus = viewModel.LeaveAppliedModel.IdLeaveStatus;
                        entity.ApprovedOn = DateTime.Now;
                        await _HREmployeeLeaveHistoryServices.UpdateAsync(entity);
                        await _unitOfWork.SaveAsync();
                        break;
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHrEmployeeLeaveApproved(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLeaveApprovedAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> DownloadDocumentAsync(long? id, string FileName)
        {
            try
            {
                string dirPath = Server.MapPath("~") + "Upload\\LeaveForm\\" + id + "\\" + FileName;
                string result = Path.GetFileName(dirPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Download", exp.Message, AlertNotificationType.error);
            }
        }
    }
}