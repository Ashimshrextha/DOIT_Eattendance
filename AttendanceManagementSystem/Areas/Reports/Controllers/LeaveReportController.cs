using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.Reports;
using SystemServices.SystemSecurity;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class LeaveReportController : BaseController
    {
        // GET: Reports/LeaveReport

        private readonly HRCalendarServices _HRCalendarServices;
        private readonly LeaveReportServices _leaveReportServices;

        public LeaveReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._leaveReportServices = new LeaveReportServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try 
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var startEndDate = GetStartEndDate(today.NepYear, today.NepMonth);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
               
                return View(new LeaveReportViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(Today.Result.NepYear, Today.Result.NepMonth),
                    DBModelList = await _leaveReportServices.GetMonthlyLeaveReport(SessionDetail.IdHRCompany, SessionDetail.IdHREmployee, SessionDetail.IdHRCompanyDivision, null,0,startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveReport",
                    BreadCrumbBaseURL = "Reports/LeaveReport",
                    BreadCrumbTitle = "बिदा विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    YearNp = today.NepYear,
                    MonthNp = today.NepMonth,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    //DDLEmployee = empModel,
                    //DDLEmployeeCategory = hrEmployeeCategoryModel,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    //DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month),
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListLeaveReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, long? idEmployeeCategory, int year, int month, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var startEndDate = GetStartEndDate(year, month);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new LeaveReportViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModelList = await _leaveReportServices.GetMonthlyLeaveReport(idHRCompany, idHREmployee, idHRCompanyDivision, idJobStatus, idEmployeeCategory, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    //DBModelList = await _leaveReportServices.GetMonthlyLeaveReport(idHRCompany, idHREmployee, idHRCompanyDivision, idJobStatus, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveReport",
                    BreadCrumbBaseURL = "Reports/LeaveReport",
                    BreadCrumbActionName = "_ListLeaveReportAsync",
                    BreadCrumbTitle = "Leave Reports",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    IdJobStatus = idJobStatus,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    YearNp = year,
                    MonthNp = month
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintLeaveReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, long? idEmployeeCategory, int year, int month)
        {
            try
            {
                var Section = "सबै शाखा";
                var NpMoth = await GetNpMonth(month);
                var startEndDate = GetStartEndDate(year, month);
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);
                Section = await GetDivisionNameNep(idHRCompanyDivision);
                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                LeaveReportViewModel modeldata = new LeaveReportViewModel();

                modeldata = new LeaveReportViewModel
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModelList = await _leaveReportServices.GetMonthlyLeaveReport(idHRCompany, idHREmployee, idHRCompanyDivision, idJobStatus, idEmployeeCategory, startEndDate.Key, startEndDate.Value),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "LeaveReport",
                    BreadCrumbBaseURL = "Reports/LeaveReport",
                    BreadCrumbActionName = "_PrintLeaveReportAsync",
                    FormModelName = "LeaveReport",
                    ModalTitle = "बिदा विवरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Leave Report Report",
                    CRUDAction = CRUDType.PRINT,
                    IdJobStatus = idJobStatus,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    YearNp = year,
                    MonthNp = month,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year} ({NpMoth}) को {Section} (शाखा) को {jobStatus} सेवाका कर्मचारीहरुको मासिक  बिदा  विवरण"
                    }

                };


                return PartialView(modeldata);
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
                var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var hrEmployeeCategoryModel = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, SessionDetail.IdHRCompany);
                var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                var DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year);
                var DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month);

                return Json(new { list = hrcompanyModel, dDLDivision = hrDivisionModel, dDLEmployee = empModel,empCategory= hrEmployeeCategoryModel, yearNp = DDLYearNp, monthNp = DDLMonthNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


    }
}