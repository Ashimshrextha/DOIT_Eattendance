using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
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
    public class HREmployeeLeaveRecommendController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeLeaveHistory

        private readonly HREmployeeLeaveHistoryServices _HREmployeeLeaveHistoryServices;
        private readonly LeaveAppliedSummaryServices _leaveAppliedSummaryServices;

        public HREmployeeLeaveRecommendController()
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
                    return returnData = (x => (x.IdRecommandationBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => (x.IdRecommandationBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => (x.IdRecommandationBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => (x.IdRecommandationBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
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
                    BreadCrumbController = "HREmployeeLeaveRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveRecommend",
                    BreadCrumbTitle = "बिदा सिफारिस",
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
        public async Task<ActionResult> _ListHREmployeeLeaveRecommendAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveHistoryViewModelList
                {
                    DBModelList = await _HREmployeeLeaveHistoryServices.GetPagedList(Condition(null, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveRecommend",
                    BreadCrumbActionName = "_ListHREmployeeLeaveRecommendAsync",
                    SessionDetails = SessionDetail,
                    BreadCrumbTitle = "बिदा सिफारिस",
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
        public async Task<ActionResult> _ReadHREmployeeLeaveRecommendAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
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
                    BreadCrumbController = "HREmployeeLeaveRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveRecommend",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeLeaveRecommendAsync",
                    SessionDetails = SessionDetail,
                    FormModelName = "HREmployeeLeaveRecommend",
                    ModalTitle = "बिदा सिफारिस विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "बिदा सिफारिस",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    LeaveAppliedModel = await this._leaveAppliedSummaryServices.GetAsync(id),
                    DDLLeaveStatusType = await this.GetIntStringPairModel(PairModelTitle.LeaveStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeLeaveRecommendAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
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
                    BreadCrumbController = "HREmployeeLeaveRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveRecommend",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    SessionDetails = SessionDetail,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeLeaveRecommendAsync",
                    FormModelName = "HREmployeeLeaveRecommend",
                    ModalTitle = "बिदा सिफारिस गर्नुहोस्",
                    BreadCrumbTitle = "बिदा सिफारिस",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    LeaveAppliedModel = await this._leaveAppliedSummaryServices.GetAsync(id),
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveRecommendationStatus)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeLeaveRecommendAsync()
        {
            try
            {
                var model = await _HREmployeeLeaveHistoryServices._dbSet.Where(x => x.IdHREmployee == this.SessionDetail.IdHREmployee).ToListAsync();
                var final = model.Select(x => new
                {
                    Id = x.Id,
                    Leave_Year = x.LeaveYearNP,
                    Leave_Title = x.MasterLeaveTitle.LeaveTitleNP,
                    Leave_Reason = x.LeaveReason,
                    LeaveFrom = x.LeaveValidFromNP,
                    LeaveTo = x.LeaveValidToNP,
                    ApprovedBy = x?.HREmployee?.HREmployeeNameNP,
                    RecommendationBy = x?.HREmployee2?.HREmployeeNameNP,
                    RecommendationOn = x.RecommandationDate,
                    ApprovedOn = x.ApprovedOn,
                    ApprovalRemark = x.ApprovedOn,
                    RecommendationRemark = x.RecommandationRemark,
                    LeaveStatus = x?.HRCompanyLeaveStatusType?.LeaveStatusTypeNP
                });
                return await ExportToExcel("HREmployeeLeaveRecommend", final);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeLeaveRecommendAsync()
        {
            try
            {
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    BreadCrumbActionName = "_PrintHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    ModalTitle = "Print HREmployeeLeaveHistory List",
                    BreadCrumbTitle = "HREmployeeLeaveHistory",
                    CRUDAction = CRUDType.PRINT,
                    SessionDetails = SessionDetail,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeLeaveHistoryServices.FindAllAsync(Condition())
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
        public async Task<ActionResult> _UpdateHREmployeeLeaveRecommendAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveRecommend(viewModel, CRUDType.UPDATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLeaveRecommend(HREmployeeLeaveHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLeaveRecommend";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveRecommend";
                viewModel.DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveRecommendationStatus);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeLeaveRecommendAsync";
                        viewModel.ModalTitle = "बिदा सिफारिस विवरण हेर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveRecommend";
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
        protected async Task<ActionResult> CRUDHREmployeeLeaveRecommend(HREmployeeLeaveHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLeaveRecommend(viewModel, cRUDType);
                }

                if (viewModel.LeaveAppliedModel.IdLeaveStatus == 1)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "सिफारिस अथवा अस्वीकृत चयन गर्नुहोस");
                    return await RETSourceHREmployeeLeaveRecommend(viewModel, cRUDType);
                }

                var entity = await _HREmployeeLeaveHistoryServices.GetByIdAsync(viewModel.LeaveAppliedModel.IdLeavehistory);
                entity.RecommandationRemark = viewModel.LeaveAppliedModel.RecommandationRemark;
                entity.IdLeaveStatus = viewModel.LeaveAppliedModel.IdLeaveStatus;
                entity.RecommandationDate = DateTime.Now;
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        await _HREmployeeLeaveHistoryServices.UpdateAsync(entity);
                        await _unitOfWork.SaveAsync();
                        break;
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLeaveRecommend(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLeaveRecommendAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
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