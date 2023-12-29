using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
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
    public class HREmployeeKaajRecommendController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeKaajRecommend
        private readonly HREmployeeKaajHistoryServices _HREmployeeKaajHistoryServices;
        private readonly HRCompanyLeaveTypeServices _hRCompanyLeaveTypeServices;

        public HREmployeeKaajRecommendController()
        {
            this._HREmployeeKaajHistoryServices = new HREmployeeKaajHistoryServices(this._unitOfWork);
            this._hRCompanyLeaveTypeServices = new HRCompanyLeaveTypeServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeKaajHistory, bool>> Condition(string searchKey = "")
        {
            Expression<Func<HREmployeeKaajHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdRecommendedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdRecommendedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdRecommendedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdRecommendedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeKaajHistoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajRecommend",
                    BreadCrumbTitle = "काज/तालिम सिफारिस",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey=string.Empty
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeKaajRecommendAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModelList
                {
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajRecommend",
                    BreadCrumbActionName = "_ListHREmployeeKaajRecommendAsync",
                    SessionDetails = SessionDetail,
                    BreadCrumbTitle = "Leave Recommendation",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey=searchKey
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeKaajRecommendAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajRecommend",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeKaajRecommendAsync",
                    SessionDetails = SessionDetail,
                    FormModelName = "HREmployeeKaajRecommend",
                    ModalTitle = "View Leave Recommend Detail",
                    BreadCrumbTitle = "Leave Recommendation",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveRecommendationStatus),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeKaajRecommendAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajRecommend",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajRecommend",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    SessionDetails = SessionDetail,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeKaajRecommendAsync",
                    FormModelName = "HREmployeeKaajRecommend",
                    ModalTitle = "Recommend Leave Request",
                    BreadCrumbTitle = "Leave Recommendation",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
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
        public async Task<ActionResult> _ExportHREmployeeKaajRecommendAsync()
        {
            try
            {
                var model = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition());
                return await ExportToExcel("HREmployeeKaajRecommend", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeKaajRecommendAsync()
        {
            try
            {
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    BreadCrumbActionName = "_PrintHREmployeeKaajHistoryAsync",
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "Print HREmployeeKaajHistory List",
                    BreadCrumbTitle = "HREmployeeKaajHistory",
                    CRUDAction = CRUDType.PRINT,
                    SessionDetails = SessionDetail,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition())
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
        public async Task<ActionResult> _UpdateHREmployeeKaajRecommendAsync(HREmployeeKaajHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeKaajRecommend(viewModel, CRUDType.UPDATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeKaajRecommend(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeKaajRecommend";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajRecommend";
                viewModel.DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveRecommendationStatus);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeKaajRecommendAsync";
                        viewModel.ModalTitle = "Recommend Leave Request";
                        viewModel.FormModelName = "HREmployeeKaajRecommend";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeKaajRecommendAsync";
                        viewModel.ModalTitle = "Print Recommend Leave Request List";
                        viewModel.FormModelName = "HREmployeeKaajRecommend";
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
        protected async Task<ActionResult> CRUDHREmployeeKaajRecommend(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeKaajRecommend(viewModel, cRUDType);
                }
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        var model = await _HREmployeeKaajHistoryServices.GetByIdAsync(1);
                        //model.RemarkRecommended = viewModel.DBModel.RemarkRecommended;
                        model.RecommendedOn = DateTime.Now;
                        //model.IdKaajStatus = viewModel.DBModel.IdKaajStatus;
                        await _HREmployeeKaajHistoryServices.UpdateAsync(model);
                        await _unitOfWork.SaveAsync();
                        break;
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeKaajRecommend(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeKaajRecommendAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}