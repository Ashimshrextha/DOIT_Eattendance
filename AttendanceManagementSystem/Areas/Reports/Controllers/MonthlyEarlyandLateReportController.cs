using AttendanceManagementSystem.Controllers;
using System;
using System.Net;
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
    public class MonthlyEarlyandLateReportController : BaseController
    {
        // GET: Reports/MonthlyEarlyandLateReport
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly MonthlyAttendanceSummaryServices _monthlyAttendanceSummaryServices;
        private readonly HREmployeeServices _hREmployeeServices;
        public MonthlyEarlyandLateReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._monthlyAttendanceSummaryServices = new MonthlyAttendanceSummaryServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
        }

        [NonAction]
        public Func<proc_GetMonthlyWorkingSummaryReport_Result, bool> Condition(string searchKey = "")
        {
            Func<proc_GetMonthlyWorkingSummaryReport_Result, bool> returnData = (x => false);
            returnData = (x =>
            (x.EmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.PISNumber.ToString().Contains(searchKey) || searchKey == ""));
            return returnData;
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var startEndDate = GetStartEndDate(today.NepYear, today.NepMonth);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return View(new MonthlyEarlyandLateReportViewModellist
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyEarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/MonthlyEarlyandLateReport",
                    BreadCrumbTitle = "मासिक हाजिरी सारांश विवरण",
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
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    //DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month),
                    DBModelList = await _monthlyAttendanceSummaryServices.GetPagedList(Condition(""), this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, null, this.SessionDetail.IdHREmployee, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusReportType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListMonthlyEarlyandLateReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var startEndDate = GetStartEndDate(year, month);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new MonthlyEarlyandLateReportViewModellist
                {
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyEarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/MonthlyEarlyandLateReport",
                    BreadCrumbActionName = "_ListMonthlyEarlyandLateReportAsync",
                    BreadCrumbTitle = "मासिक सारांश विवरण",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    IdHREmployee = idHREmployee,
                    IdDivision = idHRCompanyDivision,
                    YearNp = year,
                    MonthNp = month,
                    DBModelList = await _monthlyAttendanceSummaryServices.GetPagedList(Condition(), idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


      
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadMonthlyLateReportAsync(long? idHRCompany, long? idHREmployee, int year, int month)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var startEndDate = GetStartEndDate(year, month);
                var NpMoth = await GetNpMonth(month);
                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                var currentdesignation = GetDesignationByIDhremployee(idHREmployee);

                return PartialView(new MonthlyEarlyandLateReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyEarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/MonthlyEarlyandLateReport",
                    BreadCrumbActionName = "_ReadMonthlyLateReportAsync",
                    FormModelName = "MonthlyLateReport",
                    ModalTitle = "ढिलो आएको मासिक विवरण",
                    CRUDAction = CRUDType.READ,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelAttendanceList = await _monthlyAttendanceSummaryServices.Gets(idHRCompany, idHREmployee, startEndDate.Key, startEndDate.Value, year),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = SessionDetail.HRCompanyName,
                        ParentCompanyName = this.SessionDetail.ParentHRCompanyName,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{currentdesignation} {empModel.HREmployeeNameNP} को {year}({NpMoth})मा ढिलो आएको मासिक विवरण "
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadMonthlyEarlyCheckOutReportAsync(long? idHRCompany, long? idHREmployee, int year, int month)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var startEndDate = GetStartEndDate(year, month);
                var NpMoth = await GetNpMonth(month);
                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                var currentdesignation = GetDesignationByIDhremployee(idHREmployee);
                return PartialView(new MonthlyEarlyandLateReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyEarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/MonthlyEarlyandLateReport",
                    BreadCrumbActionName = "_ReadMonthlyEarlyCheckOutReportAsync",
                    FormModelName = "MonthlyEarlyCheckOutReport",
                    ModalTitle = "छिटो गएको मासिक विवरण",
                    CRUDAction = CRUDType.READ,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelAttendanceList = await _monthlyAttendanceSummaryServices.Gets(idHRCompany, idHREmployee, startEndDate.Key, startEndDate.Value, year),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = SessionDetail.HRCompanyName,
                        ParentCompanyName = this.SessionDetail.ParentHRCompanyName,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{currentdesignation} {empModel.HREmployeeNameNP} को {year}({NpMoth})मा छिटो गएको मासिक विवरण "
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }




        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintMonthlyEarlyandLateReport(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month)
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

                MonthlyEarlyandLateReportViewModel modeldata = new MonthlyEarlyandLateReportViewModel();

                modeldata = new MonthlyEarlyandLateReportViewModel
                {
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyEarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/MonthlyEarlyandLateReport",
                    BreadCrumbActionName = "_PrintMonthlyEarlyandLateReport",
                    FormModelName = "MonthlyEarlyandLateReport",
                    ModalTitle = "मासिक ढिलो आएको/ छिटो गएको छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _monthlyAttendanceSummaryServices.Gets(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, startEndDate.Key, startEndDate.Value),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year}({NpMoth}) को {Section} (शाखा) मा ढिलो आएको/ छिटो गएको रिपोट"
                    },
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

                return Json(new { list = list, dDLDivision = hrDivisionModel, dDLEmployee = empModel, yearNp = DDLYearNp, monthNp = DDLMonthNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}