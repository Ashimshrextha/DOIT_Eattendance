using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.EmployeeManagement;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeLeaveHistoryController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeLeaveHistory
        private readonly HREmployeeLeaveHistoryServices _HREmployeeLeaveHistoryServices;
        private readonly HRCompanyLeaveTypeServices _hRCompanyLeaveTypeServices;


        public HREmployeeLeaveHistoryController()
        {
            this._HREmployeeLeaveHistoryServices = new HREmployeeLeaveHistoryServices(this._unitOfWork);
            this._hRCompanyLeaveTypeServices = new HRCompanyLeaveTypeServices(this._unitOfWork);
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
                    return returnData = (x => (x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => (x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => (x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));

                case (int)RoleType.NormalUser:
                    return returnData = (x => (x.IdHREmployee == this.SessionDetail.IdHREmployee) && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
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
                var pagination = Get_PaginationValue(pageNumber, pageSize, "LeaveValidFromNP", orderingDirection);
                return View(new HREmployeeLeaveHistoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeLeaveHistoryServices.GetPagedList(Condition(id), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    BreadCrumbTitle = "बिदा अनुरोध",
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
        public async Task<ActionResult> _ListHREmployeeLeaveHistoryAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "LeaveValidFromNP", orderingDirection);
                return PartialView(new HREmployeeLeaveHistoryViewModelList
                {
                    DBModelList = await _HREmployeeLeaveHistoryServices.GetPagedList(Condition(null, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    BreadCrumbActionName = "_ListHREmployeeLeaveHistoryAsync",
                    BreadCrumbTitle = "बिदा अनुरोध",
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
        public async Task<ActionResult> _CreateHREmployeeLeaveHistoryAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                idHREmployee = idHREmployee == 0 ? this.SessionDetail.IdHREmployee : idHREmployee;
                int? yearNP = Today.Result.NepYear;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLeaveHistoryServices.GetsEmployeeDetail(idHREmployee, this.SessionDetail.IdHRCompany);
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    BreadCrumbActionName = "_CreateHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    ModalTitle = "बिदा माग गर्नुहोस्",
                    BreadCrumbTitle = "बिदा अनुरोध",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SessionDetails = this.SessionDetail,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = model.Id,
                    SearchKey = string.Empty,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, idHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, idHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, idHREmployee),
                    DataModel = new HREmployeeLeaveHistoryModel
                    {
                        LeaveValidFrom = this.Today.Result.EngDate,
                        LeaveValidTo = this.Today.Result.EngDate,
                        LeaveValidFromNP = this.Today.Result.Nepdate,
                        IdHREmployee = model.Id,
                        IdJob = (long)model.IdJob,
                        EmployeeName = model.HREmployeeNameNP,
                        IdHRCompany = (long)model.IdHRCompany,
                        Designation = model.HRDesignationTitle,
                        DesignationRank = model.HRDesignationRank,
                        Division = model.HRCompanyDivisionName,
                        LeaveYearNP = yearNP,
                        IsActive = true,
                        PISNumber = model.PISNumber,
                        Gender = model.HREmployeeSexTitle,
                        Mobile = model.MobileNumber,
                        JobStatus = model.JobStatus,
                        HRCompanyName = model.CompanyNameNP,
                        ImageName = model.ImageName,
                        AppoinmentDateNP=model.AppointmentDateNP,
                    },
                    DBModelLeaveSummary = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(idHREmployee, yearNP)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeLeaveHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                var empModel = await _HREmployeeLeaveHistoryServices.GetsEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                model.EmployeeName = empModel.HREmployeeNameNP;
                model.Division = empModel.HRCompanyDivisionName;
                model.Designation = empModel.HRDesignationTitle;
                model.Mobile = empModel.MobileNumber;
                model.PISNumber = empModel.PISNumber;
                model.Gender = empModel.HREmployeeSexTitle;
                model.HRCompanyName = empModel.CompanyNameNP;
                model.ImageName = empModel.ImageName;
                model.CurrentYearBalanceLeaveDay = await this.GetAvailableLeaveDays(model.IdHREmployee, model.IdLeaveType, (int)model.LeaveYearNP);
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    SessionDetails = SessionDetail,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    ModalTitle = "बिदा विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DBModelLeaveSummary = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(model.IdHREmployee, model.LeaveYearNP)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeLeaveHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                var empModel = await _HREmployeeLeaveHistoryServices.GetsEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                model.EmployeeName = empModel.HREmployeeNameNP;
                model.Division = empModel.HRCompanyDivisionName;
                model.Designation = empModel.HRDesignationTitle;
                model.Mobile = empModel.MobileNumber;
                model.PISNumber = empModel.PISNumber;
                model.Gender = empModel.HREmployeeSexTitle;
                model.HRCompanyName = empModel.CompanyNameNP;
                model.ImageName = empModel.ImageName;
                model.CurrentYearBalanceLeaveDay = await this.GetAvailableLeaveDays(model.IdHREmployee, model.IdLeaveType, (int)model.LeaveYearNP);
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
                    BreadCrumbActionName = "_UpdateHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    SessionDetails = SessionDetail,
                    ModalTitle = "बिदा सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "HREmployeeLeaveHistory",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DBModelLeaveSummary = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(model.IdHREmployee, model.LeaveYearNP)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeLeaveHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _HREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                var empModel = await _HREmployeeLeaveHistoryServices.GetsEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                model.EmployeeName = empModel.HREmployeeNameNP;
                model.Division = empModel.HRCompanyDivisionName;
                model.Designation = empModel.HRDesignationTitle;
                model.Mobile = empModel.MobileNumber;
                model.PISNumber = empModel.PISNumber;
                model.Gender = empModel.HREmployeeSexTitle;
                model.CurrentYearBalanceLeaveDay = await this.GetAvailableLeaveDays(model.IdHREmployee, model.IdLeaveType, (int)model.LeaveYearNP);
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
                    FormModelName = "HREmployeeLeaveHistory",
                    SessionDetails = this.SessionDetail,
                    ModalTitle = "बिदा विवरण मेटाउनुहोस्",
                    BreadCrumbActionName = "_DeleteHREmployeeLeaveHistoryAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, model.IdHRCompany, model.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, model.IdHRCompany, model.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, model.IdHRCompany, model.IdHREmployee)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeLeaveHistoryAsync()
        {
            try
            {
                var model = await _HREmployeeLeaveHistoryServices.FindAllAsync(Condition());
                return await ExportToExcel("HREmployeeLeaveHistory", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeLeaveHistoryAsync()
        {
            try
            {
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_PrintHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    ModalTitle = "बिदा सूची छाप्नुहोस्",
                    BreadCrumbTitle = "HREmployeeLeaveHistory",
                    CRUDAction = CRUDType.PRINT,
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

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CancelHREmployeeLeaveHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    BreadCrumbActionName = "_CancelHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    ModalTitle = "Cancel Leave",
                    BreadCrumbTitle = "Cancel Leave",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    CRUDAction = CRUDType.CANCEL,
                    SubmitButtonType = SubmitButtonType.submit,
                    SessionDetails = this.SessionDetail,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model
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
        public async Task<ActionResult> _CreateHREmployeeLeaveHistoryAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveHistory(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeLeaveHistoryAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveHistory(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeLeaveHistoryAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            try
            {
                await _HREmployeeLeaveHistoryServices.DeleteByIdAsync(viewModel.DataModel.Id);
                await _HREmployeeLeaveHistoryServices.UnitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
            await this.AlertNotification("Delete", viewModel.BreadCrumbTitle, AlertNotificationType.error);
            return RedirectToAction("_ListHREmployeeLeaveHistoryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CancelHREmployeeLeaveHistoryAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveHistory(viewModel, CRUDType.CANCEL, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLeaveHistory(HREmployeeLeaveHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = this.SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLeaveHistory";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory";
                viewModel.DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHREmployee);
                viewModel.DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHREmployee);
                viewModel.DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHREmployee);
                viewModel.DBModelLeaveSummary = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(viewModel.DataModel.IdHREmployee, viewModel.DataModel.LeaveYearNP);
                viewModel.IdHREmployee = viewModel.DataModel.IdHREmployee;
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeLeaveHistoryAsync";
                        viewModel.ModalTitle = "बिदा माग गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveHistory";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeLeaveHistoryAsync";
                        viewModel.ModalTitle = "बिदा सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveHistory";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeLeaveHistoryAsync";
                        viewModel.ModalTitle = "बिदा विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveHistory";
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
        protected async Task<ActionResult> CRUDHREmployeeLeaveHistory(HREmployeeLeaveHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ValidateSameYear(viewModel.DataModel.LeaveValidFromNP, viewModel.DataModel.LeaveValidToNP))
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", GetErrorMessage(UserErrorMessage.SAMEYEAR));
                    return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                }

                viewModel.DataModel.LeaveYearNP = GetYearFromNepaliDate(viewModel.DataModel.LeaveValidFromNP);
                viewModel.DataModel.LeaveValidFrom = (DateTime)this.GetEnglishDate(viewModel.DataModel.LeaveValidFromNP);
                viewModel.DataModel.LeaveValidTo = (DateTime)this.GetEnglishDate(viewModel.DataModel.LeaveValidToNP);
                viewModel.DataModel.IdMasterLeaveTitle = GetMasterLeaveTitleId(viewModel.DataModel.IdLeaveType);

                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                }

                if (viewModel.DataModel.LeaveValidFrom > viewModel.DataModel.LeaveValidTo)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("DataModel.LeaveValidFromNP", GetErrorMessage(UserErrorMessage.FromToGreaterThanToDate));
                    return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                }
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        bool isExit = await _HREmployeeLeaveHistoryServices.CheckLeaveExistance(viewModel.DataModel.IdHREmployee, viewModel.DataModel.LeaveValidFrom, viewModel.DataModel.LeaveValidTo);
                        if (isExit)
                        {
                            Response.StatusCode = 350;
                            ModelState.AddModelError("DataModel.IdLeaveType", GetErrorMessage(UserErrorMessage.EXISTLeave));
                            return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                        }
                        break;
                    case CRUDType.UPDATE:
                        break;
                    case CRUDType.DELETE:
                        break;
                    case CRUDType.CANCEL:
                        var model = await _HREmployeeLeaveHistoryServices.GetByIdAsync(viewModel.DataModel.Id);
                        model.LeaveCancelFrom = viewModel.DataModelLeaveCancel.LeaveCancelFrom;
                        model.LeaveCancelFromNP = viewModel.DataModelLeaveCancel.LeaveCancelFromNP;
                        cRUDType = CRUDType.UPDATE;
                        break;
                }
                //get Eligibile days for leave
                decimal leavecount = 0;
                var leaveAvailableDayModel = await this.GetEndDateAndAvailableDaysLeave(viewModel.DataModel.IdHREmployee, viewModel.DataModel.IdLeaveType, viewModel.DataModel.LeaveValidFrom, viewModel.DataModel.LeaveValidFrom.AddDays(double.Parse(viewModel.DataModel.CurrentYearBalanceLeaveDay.ToString())));
                decimal requestedDays = decimal.Parse(viewModel.DataModel.LeaveValidTo.Subtract(viewModel.DataModel.LeaveValidFrom).Days.ToString()) + 1;
                var availableLeaveDays = await this.GetAvailableLeaveDays(viewModel.DataModel.IdHREmployee, viewModel.DataModel.IdLeaveType, (int)viewModel.DataModel.LeaveYearNP);
                requestedDays = viewModel.DataModel.IsHalfDayCount ? decimal.Divide(requestedDays, 2) : requestedDays;
                if (leaveAvailableDayModel != null)
                {
                    leavecount = decimal.Parse(leaveAvailableDayModel.TotalAvailableLeaveDays.ToString());
                    
                }
                if (availableLeaveDays > leavecount)
                {
                    leavecount = availableLeaveDays;
                }
                decimal TotalAvailableDays = viewModel.DataModel.IsHalfDayCount && availableLeaveDays > 0 ? availableLeaveDays : leavecount;

                if (availableLeaveDays <= 0)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", $"पाऊनु पर्ने बिदा भन्दा बडी माग गर्नु भएको {leaveAvailableDayModel.NepDate}");
                    return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                }
                else if (requestedDays > TotalAvailableDays)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", $"पाऊनु पर्ने बिदा भन्दा बडी माग गर्नु भएको {leaveAvailableDayModel.NepDate}");
                    return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                }

                viewModel.DataModel.IdLeaveStatus = (this.SessionDetail.IdRoleType == (int)RoleType.Admin || this.SessionDetail.IdRoleType == (int)RoleType.SectionAdmin) ? (this.SessionDetail.IdHREmployee == viewModel.DataModel.IdHREmployee ? 1 : 3) : 1;
                viewModel.DataModel.IdLeaveStatus = viewModel.DataModel.IdRecommandationBy == null && viewModel.DataModel.IdLeaveStatus != 3 ? 2 : viewModel.DataModel.IdLeaveStatus;

                //Document upload
                if (viewModel.FormDocument != null && ValidateImageFileType(viewModel.FormDocument.ContentType))
                {
                    if (viewModel.FormDocument.ContentLength <= 1048576)
                    {
                        if (viewModel.FormDocument != null)
                        {
                            var SubFolderName = viewModel.DataModel.IdHREmployee.ToString();
                            if (viewModel.FormDocument != null)
                            {
                                var FolderName = "LeaveForm\\" + SubFolderName + "\\";
                                var DocumentName = FileUploads(viewModel.FormDocument, FolderName);
                                viewModel.DataModel.Document = DocumentName;
                            }
                        }
                    }
                    else
                    {
                        Response.StatusCode = 350;
                        ModelState.AddModelError("DataModel.Document", GetErrorMessage(UserErrorMessage.IMAGESIZE));
                        return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
                    }
                }

                //Committing Data
                try
                {
                    await _HREmployeeLeaveHistoryServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                }

                catch (DbUpdateException e)
                {
                    SqlException s = e.InnerException.InnerException as SqlException;
                    if (s != null && s.Number == 2627)
                    {
                        ModelState.AddModelError(string.Empty,
                            string.Format("Part number '{0}' already exists.", 0));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                            "An error occured - please contact your system administrator.");
                    }
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLeaveHistory(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLeaveHistoryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
       

        public string FileUploads(HttpPostedFileBase File, string FolderName)
        {
            string serverPath = Server.MapPath("~");
            string DocName = File.FileName;
            string DocPath = serverPath + "Upload\\" + FolderName;
            var fileExt = Path.GetExtension(File.FileName);
            if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
            var newDocName = GetFileName(DocPath, Path.GetFileNameWithoutExtension(DocName), fileExt);
            string targetPath = Path.Combine(DocPath, newDocName + fileExt);
            File.SaveAs(targetPath);
            return newDocName + fileExt;
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

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _GetHREmployeeLeaveSummaryAsync(long? idHREmployee, string dateNP)
        {
            try
            {
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    DBModelLeaveSummary = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(idHREmployee, GetYearFromNepaliDate(dateNP))
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintIndividualHREmployeeLeaveHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string RecomendationBy = string.Empty;
                string RecomendationDesignation = string.Empty;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                var empModel = await _HREmployeeLeaveHistoryServices.GetsEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                model.EmployeeName = empModel.HREmployeeNameNP;
                model.Division = empModel.HRCompanyDivisionName;
                model.Designation = empModel.HRDesignationTitle;
                model.Mobile = empModel.MobileNumber;
                model.PISNumber = empModel.PISNumber;
                model.Gender = empModel.HREmployeeSexTitle;
                model.HRCompanyName = empModel.CompanyNameNP;
                model.ImageName = empModel.ImageName;
                model.Division = empModel.HRCompanyDivisionName;
                model.CurrentYearBalanceLeaveDay = await this.GetAvailableLeaveDays(model.IdHREmployee, model.IdLeaveType, (int)model.LeaveYearNP);
                var yearNp = GetYearFromNepaliDate(model.LeaveYearNP.ToString());
                var Datalist = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(model.IdHREmployee, yearNp);
                var LevaebalanceList = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeavebalanceSummary(model.IdHREmployee, yearNp,model.LeaveValidFrom);
                if (model.IdRecommandationBy != null)
                {
                    RecomendationBy = GetEmployeeNameById(model.IdRecommandationBy);
                    RecomendationDesignation = GetEmployeeCurrentPostById(model.IdRecommandationBy);
                }
                var ApprovedBy = GetEmployeeNameById(model.IdApprovedBy);
                var ApprovedDesignation = GetEmployeeCurrentPostById(model.IdApprovedBy);


                var information = await GetCompanyHeaderDetails(model.IdHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;


                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    SessionDetails = SessionDetail,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_PrintIndividualHREmployeeLeaveHistoryAsync",
                    FormModelName = "HREmployeeLeaveHistory",
                    ModalTitle = "बिदा विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.SEARCH,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DBModelLeaveSummary =  Datalist,
                    DBModelLeaveBalanceSummary = LevaebalanceList,
                    RecommendationEmployeeName =RecomendationBy,
                    RecommendationEmployeeDesignation = RecomendationDesignation,
                    ApprovalEmployeeName=ApprovedBy,
                    ApprovalEmployeeDesignation = ApprovedDesignation,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = ""
                    },

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
}