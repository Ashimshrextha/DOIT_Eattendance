using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemServices.EmployeeManagement;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeLateAttendanceApprovedController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeLateAttendanceApproved
        private readonly HREmployeeLateAttendanceApprovedServices _HREmployeeLateAttendanceApprovedServices;

        public HREmployeeLateAttendanceApprovedController()
        {
            this._HREmployeeLateAttendanceApprovedServices = new HREmployeeLateAttendanceApprovedServices(this._unitOfWork);
        }

        public Expression<Func<HREmployeeAttendanceHistory, bool>> Condition(long? idHREmployee)
        {
            Expression<Func<HREmployeeAttendanceHistory, bool>> returndata = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returndata = (x => (x.IdHREmployee == idHREmployee || idHREmployee == 0) && x.LateBy != null && x.LateReason != null);

                case (int)RoleType.Admin:
                    return returndata = (x => x.IdHRCompany == this.SessionDetail.IdHRCompany &&( x.IdApprovedBy==this.SessionDetail.IdHREmployee || x.IdHREmployee==this.SessionDetail.IdHREmployee) && x.LateBy != null && x.LateReason != null);

                case (int)RoleType.RootUser:
                    return returndata = (x => x.LateReason != null && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && x.LateBy != null);

                case (int)RoleType.SectionAdmin:
                    return returndata = (x => x.LateReason != null && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && x.LateBy != null);

                case (int)RoleType.NormalUser:
                    return returndata = (x => x.LateReason != null && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && x.LateBy != null);

                default:
                    return returndata;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "AttendanceDate", "DESC");
                return View(new HREmployeeAttendanceHistoryViewModelList
                {
                    DBModelList = await _HREmployeeLateAttendanceApprovedServices.GetPagedList(Condition(this.SessionDetail.IdHREmployee), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLateAttendanceApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLateAttendanceApproved",
                    BreadCrumbTitle = "ढिलो आएको स्वीकृत",
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
        public async Task<ActionResult> _ListHREmployeeLateAttendanceApprovedAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "AttendanceDate", "DESC");
                return PartialView(new HREmployeeAttendanceHistoryViewModelList
                {
                    DBModelList = await _HREmployeeLateAttendanceApprovedServices.GetPagedList(Condition(this.SessionDetail.IdHREmployee), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLateAttendanceApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLateAttendanceApproved",
                    BreadCrumbActionName = "_ListHREmployeeLateAttendanceApprovedAsync",
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

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeLateAttendanceApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLateAttendanceApprovedServices.GetModelByIdAsync(id);
                var empModel = await _HREmployeeLateAttendanceApprovedServices.GetEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLateAttendanceApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLateAttendanceApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeLateAttendanceApprovedAsync",
                    FormModelName = "HREmployeeLateAttendanceApproved",
                    ModalTitle = "ढिलो आएको कारण",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany),
                    DataModel = model,
                    DBModelEmployee = empModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeLateAttendanceApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLateAttendanceApprovedServices.GetModelByIdAsync(id);
                var empModel = await _HREmployeeLateAttendanceApprovedServices.GetEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLateAttendanceApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLateAttendanceApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeLateAttendanceApprovedAsync",
                    FormModelName = "HREmployeeLateAttendanceApproved",
                    ModalTitle = "ढिलो आएको कारण स्वीकृत गर्ने",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany),
                    DataModel = model,
                    DBModelEmployee = empModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeLateAttendanceApprovedAsync()
        {
            try
            {
                var model = await _HREmployeeLateAttendanceApprovedServices.FindAllAsync(x => x.LateReason != null && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && x.LateBy != null);
                return await ExportToExcel("HREmployeeLateAttendanceApproved", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeLateAttendanceApprovedAsync()
        {
            try
            {
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLateAttendanceApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLateAttendanceApproved",
                    BreadCrumbActionName = "_PrintHREmployeeLateAttendanceApprovedAsync",
                    FormModelName = "HREmployeeLateAttendanceApproved",
                    ModalTitle = "ढिलो आएको कारण विवरण छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeLateAttendanceApprovedServices.FindAllAsync(x => x.LateReason != null && (x.IdApprovedBy == this.SessionDetail.IdHREmployee || x.IdHREmployee == this.SessionDetail.IdHREmployee) && x.LateBy != null)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeLateAttendanceApprovedAsync(HREmployeeAttendanceHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLateAttendanceApproved(viewModel, CRUDType.UPDATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLateAttendanceApproved(HREmployeeAttendanceHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLateAttendanceApproved";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLateAttendanceApproved";
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, viewModel.DataModel.IdHREmployee);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeLateAttendanceApprovedAsync";
                        viewModel.ModalTitle = "ढिलो आएको कारण स्वीकृत गर्ने";
                        viewModel.FormModelName = "HREmployeeLateAttendanceApproved";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeLateAttendanceApprovedAsync";
                        viewModel.ModalTitle = "ढिलो आएको कारण विवरण छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLateAttendanceApproved";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeLateAttendanceApproved(HREmployeeAttendanceHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLateAttendanceApproved(viewModel, cRUDType);
                }
                var model = await _HREmployeeLateAttendanceApprovedServices.GetByIdAsync(viewModel.DataModel.Id);
                model.Remark = viewModel.DataModel.Remark;
                model.IsLateReasonApproved = viewModel.DataModel.IsLateReasonApproved;
                await _HREmployeeLateAttendanceApprovedServices.UpdateAsync(model);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLateAttendanceApproved(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLateAttendanceApprovedAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}
