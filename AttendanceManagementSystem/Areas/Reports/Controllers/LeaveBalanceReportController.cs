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
    public class LeaveBalanceReportController : BaseController
    {
        // GET: Reports/LeaveBalanceReport
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly LeaveBalanceReportServices _leaveBalanceReportServices;
        private readonly HREmployeeServices _hREmployeeServices;
        private readonly HREmployeeLeaveManagementServices _hREmployeeLeaveManagementServices;

        public LeaveBalanceReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._leaveBalanceReportServices = new LeaveBalanceReportServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
            this._hREmployeeLeaveManagementServices = new HREmployeeLeaveManagementServices(this._unitOfWork);
        }

        [NonAction]
        public Func<proc_GetBalanceLeaveReport_Result, bool> Condition(string searchKey = "")
        {
            Func<proc_GetBalanceLeaveReport_Result, bool> returnData = (x => false);
            returnData = (x =>
               (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.IdEnroll.ToString().Contains(searchKey) || searchKey == ""));
            return returnData;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                //var date = DateTime.Now.Date;
                //HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return View(new LeaveBalanceReportViewModelList
                {
                    DBModelPageList = await _leaveBalanceReportServices.GetPagedList(Condition(""), this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, null, this.SessionDetail.IdHREmployee, Today.Result.NepYear, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveBalanceReport",
                    BreadCrumbBaseURL = "Reports/LeaveBalanceReport",
                    BreadCrumbTitle = "सञ्चित बिदा विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    //YearNp = Today.Result.NepYear,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    //DDLEmployee = empModel,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListLeaveBalanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.NepYear == year);
                return PartialView(new LeaveBalanceReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveBalanceReport",
                    BreadCrumbBaseURL = "Reports/LeaveBalanceReport",
                    BreadCrumbActionName = "_ListLeaveBalanceReportAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    YearNp = year,
                    IdJobStatus = idJobStatus,
                    DBModelPageList = await _leaveBalanceReportServices.GetPagedList(Condition(), idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, year, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintLeaveBalanceReportAsync(long? idHRCompany, int? idJobStatus, long? idHREmployee, int year)
        {
            try
            {
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.NepYear == year);
                var model = await _hREmployeeServices.GetEmployee(idHRCompany, idHREmployee);
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);

                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                LeaveBalanceReportViewModel modeldata = new LeaveBalanceReportViewModel();

                modeldata = new LeaveBalanceReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveBalanceReport",
                    BreadCrumbBaseURL = "Reports/LeaveBalanceReport",
                    BreadCrumbActionName = "_PrintLeaveBalanceReportAsync",
                    ModalTitle = "सञ्चित बिदा विवरण सुची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    FormModelName = "LeaveBalanceReport",
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdJobStatus = idJobStatus,
                    YearNp = year,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = idHRCompany,
                    DBModel = model,
                    DBModelList = await this._leaveBalanceReportServices.GetsLeavePerYearReport(idHREmployee, year),
                    LeaveMgmtList = await this._hREmployeeLeaveManagementServices.FindAllAsync(x => x.IdHREmployee == idHREmployee && x.LeaveYearNP == year),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year} सालको {model.HRDesignationTitle} पदमा कार्यरत कर्मचारी {model.HREmployeeNameNP} को सञ्चित बिदा विवरण"
                    }
                };

                return PartialView(modeldata);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadLeaveBalanceAsync(long? idHRCompany, long? idHREmployee, int year)
        {
            try
            {
                var model = await _hREmployeeServices.GetEmployee(idHRCompany, idHREmployee);
                return PartialView(new LeaveBalanceReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveBalanceReport",
                    BreadCrumbBaseURL = "Reports/LeaveBalanceReport",
                    BreadCrumbTitle = "सञ्चित बिदा विवरण",
                    BreadCrumbActionName = "_ReadLeaveBalanceAsync",
                    CRUDAction = CRUDType.READ,
                    ModalTitle = "सञ्चित बिदा विवरण",
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModel = model,
                    DBModelList = await this._leaveBalanceReportServices.GetsLeavePerYearReport(idHREmployee, year),
                    LeaveMgmtList = await this._hREmployeeLeaveManagementServices.FindAllAsync(x => x.IdHREmployee == idHREmployee && x.LeaveYearNP == year),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = SessionDetail.HRCompanyName,
                        ParentCompanyName = this.SessionDetail.ParentHRCompanyName,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year}   सालको   {model.HRDesignationTitle} पदमा कार्यरत  कर्मचारी {model.HREmployeeNameNP} को  सञ्चित बिदा विवरण"
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> CompanyList()
        {
            try
            {
                var list = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                var YearNp = Today.Result.NepYear;
                var DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year);
                return Json(new { list = list, dDLDivision = hrDivisionModel, dDLEmployee = empModel, dDLYearNp = DDLYearNp, yearNp = YearNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

    }
}