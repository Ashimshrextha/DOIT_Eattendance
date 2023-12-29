using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.SystemSecurity;
using SystemServices.Reports;
using SystemServices.SystemSecurity;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;


namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class MonthlyAttendanceController : BaseController
    {
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly MonthlyAttendanceServices _MonthlyAttendanceServices;

        public MonthlyAttendanceController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            _MonthlyAttendanceServices = new MonthlyAttendanceServices(this._unitOfWork);
        }
        public Func<proc_MonthlyAttendance_Result, bool> Condition(string searchKey = "")
        {
            Func<proc_MonthlyAttendance_Result, bool> retutndata = (x => false);
            retutndata = (x =>
            (x.EmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.PISNumber.Contains(searchKey) || searchKey == ""));
            return retutndata;
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
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                var model = await _MonthlyAttendanceServices.GetResult(Condition(""), SessionDetail.IdHRCompany, SessionDetail.IdHRCompanyDivision, null, SessionDetail.IdHREmployee, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
                return View(new MonthlyAttendanceReportViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(today.NepYear, today.NepMonth),
                    DBModel = model,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyAttendance",
                    BreadCrumbBaseURL = "Reports/MonthlyAttendance",
                    BreadCrumbTitle = "मासिक हाजिरी",
                    BreadCrumbActionName = "Index",
                    //CRUDAction = CRUDType.READ,
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
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    //DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month),
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListMonthlyAttendanceAsync(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var startEndDate = GetStartEndDate(year, month);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new MonthlyAttendanceReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    YearNp = year,
                    MonthNp = month,
                    IdJobStatus = idJobStatus,
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModel = await _MonthlyAttendanceServices.GetResult(Condition(searchKey), idHRCompany, idHRCompanyDivision ?? 0, idJobStatus, idHREmployee ?? 0, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyAttendance",
                    BreadCrumbBaseURL = "Reports/MonthlyAttendance",
                    BreadCrumbActionName = "_ListMonthlyAttendanceAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintMonthlyAttendanceAsync(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month)
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

                MonthlyAttendanceReportViewModelList modeldata = new MonthlyAttendanceReportViewModelList();

                modeldata = new MonthlyAttendanceReportViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModelPrint = await _MonthlyAttendanceServices.GetResult(idHRCompany, idHRCompanyDivision ?? 0, idJobStatus, idHREmployee ?? 0, startEndDate.Key, startEndDate.Value),
                    BreadCrumbArea = "Reports",
                    IdJobStatus = idJobStatus,
                    BreadCrumbController = "MonthlyAttendance",
                    BreadCrumbBaseURL = "Reports/MonthlyAttendance",
                    BreadCrumbActionName = "_PrintMonthlyAttendanceAsync",
                    ModalTitle = "मासिक हाजिरी छाप्नुहोस्",
                    FormModelName = "MonthlyAttendance",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year}({NpMoth}) को {Section} (शाखा) को {jobStatus} कर्मचारीहरुको मासिक हाजिरी"
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
                var list = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                var DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year);
                var DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month);

                return Json(new { list = list, dDLDivision = hrDivisionModel, dDLEmployee = empModel, yearNp = DDLYearNp,monthNp= DDLMonthNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


    }
}