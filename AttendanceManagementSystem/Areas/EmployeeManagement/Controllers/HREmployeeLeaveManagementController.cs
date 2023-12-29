using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeLeaveManagementController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeLeaveManagement

        private readonly HREmployeeLeaveManagementServices _hREmployeeLeaveManagementServices;
        private readonly HRCompanyLeaveTypeServices _hRCompanyLeaveTypeServices;

        public HREmployeeLeaveManagementController()
        {
            this._hREmployeeLeaveManagementServices = new HREmployeeLeaveManagementServices(this._unitOfWork);
            this._hRCompanyLeaveTypeServices = new HRCompanyLeaveTypeServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeLeaveManagement, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<HREmployeeLeaveManagement, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeLeaveManagementAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveManagementViewModelList
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement",
                    BreadCrumbActionName = "_ListWrapperHREmployeeLeaveManagementAsync",
                    CRUDAction = CRUDType.READ,
                    SessionDetails = SessionDetail,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    DBModelList = await _hREmployeeLeaveManagementServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeLeaveManagementAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveManagementViewModelList
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement",
                    BreadCrumbActionName = "_ListWrapperHREmployeeLeaveManagementAsync",
                    CRUDAction = CRUDType.READ,
                    SessionDetails = SessionDetail,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    DBModelList = await _hREmployeeLeaveManagementServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection)

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeLeaveManagementAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveManagementViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement",
                    BreadCrumbActionName = "_CreateHREmployeeLeaveManagementAsync",
                    FormModelName = "HREmployeeLeaveManagement",
                    ModalTitle = "नयाँ खर्च भएको बिदा थप्नुहोस्  ",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    SessionDetails = SessionDetail,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, idHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, idHREmployee),
                    DataModel = new HREmployeeLeaveManagementModel { IdHREmployee = (long)idHREmployee, LeaveYearNP = Today.Result.NepYear }
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
        public async Task<ActionResult> _CreateHREmployeeLeaveManagementAsync(HREmployeeLeaveManagementViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHREmployeeLeaveManagementAsync(viewModel, formCollection, CRUDType.CREATE);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeLeaveManagementAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var model = await this._hREmployeeLeaveManagementServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveManagementViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    BreadCrumbActionName = "_ReadHREmployeeLeaveManagementAsync",
                    FormModelName = "HREmployeeLeaveManagement",
                    ModalTitle = "कर्मचारी सम्पर्क सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.CurrentYear),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, model.IdHREmployee),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeLeaveManagementAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var model = await this._hREmployeeLeaveManagementServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveManagementViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    BreadCrumbActionName = "_UpdateHREmployeeLeaveManagementAsync",
                    FormModelName = "HREmployeeLeaveManagement",
                    ModalTitle = "कर्मचारीको खर्च भएको बिदा सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.CurrentYear),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, model.IdHREmployee),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _UpdateHREmployeeLeaveManagementAsync(HREmployeeLeaveManagementViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHREmployeeLeaveManagementAsync(viewModel, formCollection, CRUDType.UPDATE);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeLeaveManagementAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var model = await this._hREmployeeLeaveManagementServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveManagementViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    BreadCrumbActionName = "_DeleteHREmployeeLeaveManagementAsync",
                    FormModelName = "HREmployeeLeaveManagement",
                    ModalTitle = "कर्मचारीको खर्च भएको बिदा हटाउनुहोस",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.CurrentYear),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, model.IdHREmployee),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _DeleteHREmployeeLeaveManagementAsync(HREmployeeLeaveManagementViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHREmployeeLeaveManagementAsync(viewModel, formCollection, CRUDType.DELETE);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLeaveManagementAsync(HREmployeeLeaveManagementViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLeaveManagement";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveManagement";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeLeaveBalanceAsync";
                        viewModel.ModalTitle = "नयाँ बचत बिदा थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveManagement";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeLeaveManagementAsync(HREmployeeLeaveManagementViewModel viewModel, FormCollection formCollection, CRUDType cRUDType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLeaveManagementAsync(viewModel, cRUDType);
                }
                var model = await this._hRCompanyLeaveTypeServices.GetByIdAsync(viewModel.DataModel.IdLeaveType);
                viewModel.DataModel.IdMasterLeaveTitle = model.IdMasterLeaveTitle;
                await _hREmployeeLeaveManagementServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLeaveManagementAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLeaveManagementAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}