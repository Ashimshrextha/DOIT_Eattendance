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
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HrEmployeeKaajApprovedController : BaseController
    {
        // GET: EmployeeManagement/HrEmployeeKaajApproved
        private readonly HREmployeeKaajHistoryServices _HREmployeeKaajHistoryServices;
        private readonly KaajAppliedSummaryServices _kaajAppliedSummaryServices;

        public HrEmployeeKaajApprovedController()
        {
            this._HREmployeeKaajHistoryServices = new HREmployeeKaajHistoryServices(this._unitOfWork);
            this._kaajAppliedSummaryServices = new KaajAppliedSummaryServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeKaajHistory, bool>> Condition(long? id = null, string searchKey = "")
        {
            Expression<Func<HREmployeeKaajHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));

                case (int)RoleType.NormalUser:
                    return returnData = (x => (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
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
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", "DESC");
                return View(new HREmployeeKaajHistoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(id), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HrEmployeeKaajApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HrEmployeeKaajApproved",
                    BreadCrumbTitle = "काज स्वीकृत",
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
        public async Task<ActionResult> _ListHrEmployeeKaajApprovedAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", "DESC");
                return PartialView(new HREmployeeKaajHistoryViewModelList
                {
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(null, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HrEmployeeKaajApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HrEmployeeKaajApproved",
                    BreadCrumbActionName = "_ListHrEmployeeKaajApprovedAsync",
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
        public async Task<ActionResult> _ReadHrEmployeeKaajApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
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
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_ReadHREmployeeKaajHistoryAsync",
                    FormModelName = "HrEmployeeKaajApproved",
                    ModalTitle = "काज स्वीकृत विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    KaajModel = await this._kaajAppliedSummaryServices.GetAsync(id),
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHrEmployeeKaajApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await this._kaajAppliedSummaryServices.GetAsync(id);
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HrEmployeeKaajApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HrEmployeeKaajApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_UpdateHrEmployeeKaajApprovedAsync",
                    FormModelName = "HrEmployeeKaajApproved",
                    ModalTitle = "काज स्वीकृत सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    KaajModel = model,
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHrEmployeeKaajApprovedAsync()
        {
            try
            {
                var model = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition());
                return await ExportToExcel("HrEmployeeKaajApproved", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHrEmployeeKaajApprovedAsync()
        {
            try
            {
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HrEmployeeKaajApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HrEmployeeKaajApproved",
                    BreadCrumbActionName = "_PrintHrEmployeeKaajApprovedAsync",
                    FormModelName = "HrEmployeeKaajApproved",
                    ModalTitle = "काज स्वीकृत सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Leave Approval",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    SessionDetails = SessionDetail,
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
        public async Task<ActionResult> _UpdateHrEmployeeKaajApprovedAsync(HREmployeeKaajHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHrEmployeeKaajApproved(viewModel, CRUDType.UPDATE, formCollection);
        }


        [NonAction]
        protected async Task<ActionResult> RETSourceHrEmployeeKaajApproved(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HrEmployeeKaajApproved";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HrEmployeeKaajApproved";
                viewModel.DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHrEmployeeKaajApprovedAsync";
                        viewModel.ModalTitle = "काज स्वीकृत सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HrEmployeeKaajApproved";
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
        protected async Task<ActionResult> CRUDHrEmployeeKaajApproved(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHrEmployeeKaajApproved(viewModel, cRUDType);
                }
                if (viewModel.KaajModel.IdKaajStatus == 2)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "स्वीकृत अथवा अस्वीकृत चयन गर्नुहोस");
                    return await RETSourceHrEmployeeKaajApproved(viewModel, cRUDType);
                }
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        var model = await _HREmployeeKaajHistoryServices.GetByIdAsync(viewModel.KaajModel.IdKaaj);
                        model.RemarkApprovedBy = viewModel.KaajModel.KaajApprovalRemark;
                        model.ApprovedOn = DateTime.Now;
                        model.IdKaajStatus = viewModel.KaajModel.IdKaajStatus;
                        await _HREmployeeKaajHistoryServices.UpdateAsync(model);
                        await _unitOfWork.SaveAsync();
                        break;
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHrEmployeeKaajApproved(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHrEmployeeKaajApprovedAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> DownloadDocumentAsync(long? id, string FileName)
        {
            try
            {
                string dirPath = Server.MapPath("~") + "Upload\\EmployeeKaajProfile\\" + id + "\\" + FileName;
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