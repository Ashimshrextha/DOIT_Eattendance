using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemServices.EmployeeManagement;
using SystemServices.Reports;
using SystemServices.SystemSecurity;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class LateAttendanceReportController : BaseController
    {
        // GET: Reports/LateAttendanceReport

        private readonly HRCalendarServices _HRCalendarServices;
        private readonly LateAttendanceReportServices _LateAttendanceReportServices;
        private readonly HREmployeeLateAttendanceApprovedServices _HREmployeeLateAttendanceApprovedServices;

        public LateAttendanceReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            _LateAttendanceReportServices = new LateAttendanceReportServices(this._unitOfWork);
            this._HREmployeeLateAttendanceApprovedServices = new HREmployeeLateAttendanceApprovedServices(this._unitOfWork);
        }

        [NonAction]
        public Func<proc_LateAttendance_Result, bool> Condition(long? idHRCompany, long? idHREmployee,long? idDivision, DateTime date, string searchKey = "")
        {
            Func<proc_LateAttendance_Result, bool> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && (x.Name.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHRCompany == idHRCompany && (x.Name.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && x.IdHREmployee == idHREmployee && (x.Name.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHRCompany == idHRCompany && (x.Name.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && x.IdHREmployee == idHREmployee && (x.Name.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new LateAttendanceReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LateAttendanceReport",
                    BreadCrumbBaseURL = "Reports/LateAttendanceReport",
                    BreadCrumbTitle = "ढिलो आएको हाजिरी विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelPageList = await _LateAttendanceReportServices.GetPagedList(Condition(SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee,SessionDetail.IdHRCompanyDivision, today.EngDate), SessionDetail.IdHRCompany, SessionDetail.IdHRCompanyDivision, null, SessionDetail.IdHREmployee, date, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    Date = today.EngDate,
                    DateNp = today.NepDate,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    //DDLCompany = await GetLongStringPairModelReport(PairModelTitle.HRCompany),
                    //DDLDivision = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany),
                    //DDLEmployee = await GetLongStringPairModelReport(PairModelTitle.Employee, SessionDetail.IdHRCompany),
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListLateAttendanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                idJobStatus = idJobStatus == 0 ? null : idJobStatus;
                return PartialView(new LateAttendanceReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LateAttendanceReport",
                    BreadCrumbBaseURL = "Reports/LateAttendanceReport",
                    BreadCrumbActionName = "_ListLateAttendanceReportAsync",
                    BreadCrumbTitle = "ढिलो आएको हाजिरी विवरण",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelPageList = await _LateAttendanceReportServices.GetPagedList(Condition(idHRCompany, idHREmployee,idHRCompanyDivision, date), idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintLateAttendanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date)
        {
            try
            {
                var Section = "सबै शाखा";
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var NpMoth = await GetNpMonth(today.NepMonth);
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);
                Section = await GetDivisionNameNep(idHRCompanyDivision);
                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                LateAttendanceReportViewModelList modeldata = new LateAttendanceReportViewModelList();

                modeldata = new LateAttendanceReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LateAttendanceReport",
                    BreadCrumbBaseURL = "Reports/LateAttendanceReport",
                    BreadCrumbActionName = "_PrintLateAttendanceReportAsync",
                    FormModelName = "LateAttendanceReport",
                    ModalTitle = "ढिलो आएको हाजिरी विवरण छाप्नुहोस्",
                    BreadCrumbTitle = "ढिलो आएको हाजिरी विवरण",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _LateAttendanceReportServices.GetResult(Condition(idHRCompany, idHREmployee, idHRCompanyDivision, date), idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{today.NepYear}({NpMoth}) को {Section} (शाखा) को {jobStatus} सेवाका कर्मचारीहरुको ढिलो आएको हाजिरी विवरण"
                    },

                };


                return PartialView(modeldata);

            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateLateAttendanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = await _HREmployeeLateAttendanceApprovedServices.GetModelFindAsync(x => x.IdHREmployee == idHREmployee && x.AttendanceDate == date);
                var empModel = await _LateAttendanceReportServices.GetEmployeeDetail(SessionDetail.IdHREmployee, SessionDetail.IdHRCompany);
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new LateAttendanceReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LateAttendanceReport",
                    BreadCrumbBaseURL = "Reports/LateAttendanceReport",
                    BreadCrumbActionName = "_UpdateLateAttendanceReportAsync",
                    FormModelName = "LateAttendanceReport",
                    ModalTitle = "ढिलो आएको कारण लेख्नुहोस्",
                    BreadCrumbTitle = "ढिलो आएको हाजिरी विवरण",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee),
                    DBModelEmployee = empModel,
                    DataModel = model
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
        public async Task<ActionResult> _UpdateLateAttendanceReportAsync(LateAttendanceReportViewModelList viewModel, FormCollection formCollection)
        {
            return await CRUDLateAttendanceReport(viewModel, CRUDType.UPDATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceLateAttendanceReport(LateAttendanceReportViewModelList viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "Reports";
                viewModel.BreadCrumbController = "LateAttendanceReport";
                viewModel.BreadCrumbBaseURL = "Reports/LateAttendanceReport";
                viewModel.DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee);
                viewModel.DBModelEmployee = await _LateAttendanceReportServices.GetEmployeeDetail(SessionDetail.IdHREmployee, SessionDetail.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateLateAttendanceReportAsync";
                        viewModel.ModalTitle = "ढिलो आएको कारण लेख्नुहोस्";
                        viewModel.FormModelName = "LateAttendanceReport";
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
        protected async Task<ActionResult> CRUDLateAttendanceReport(LateAttendanceReportViewModelList viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceLateAttendanceReport(viewModel, cRUDType);
                }
                var model = await _HREmployeeLateAttendanceApprovedServices.GetByIdAsync(viewModel.DataModel.Id);
                model.LateReason = viewModel.DataModel.LateReason;
                model.IdApprovedBy = viewModel.DataModel.IdApprovedBy;
                await _HREmployeeLateAttendanceApprovedServices.UpdateAsync(model);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceLateAttendanceReport(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListLateAttendanceReportAsync", new { idHRCompany = this.SessionDetail.IdHRCompany, idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision, idHREmployee = this.SessionDetail.IdHREmployee, date = viewModel.DataModel.AttendanceDate, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> CompanyList()
        {
            try
            {
                var list = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                var DDLDivision = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var DDLEmployee = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return Json(new { list = list, dDLDivision = DDLDivision, dDLEmployee = DDLEmployee }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}