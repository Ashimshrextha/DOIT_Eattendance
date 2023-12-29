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
    public class EarlyandLateReportController : BaseController
    {
        // GET: Reports/EarlyandLateReport
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly MonthlyAttendanceSummaryServices _monthlyAttendanceSummaryServices;
        private readonly HREmployeeServices _hREmployeeServices;
        public EarlyandLateReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._monthlyAttendanceSummaryServices = new MonthlyAttendanceSummaryServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
        }

        [NonAction]
        public Func<proc_GetDateRangeWorkingSummaryReport_Result, bool> Condition(string searchKey = "")
        {
            Func<proc_GetDateRangeWorkingSummaryReport_Result, bool> returnData = (x => false);
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
                var Adddate =date.AddDays(1);

                var TodateDateNp = this.GetNepaliDate(Adddate);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return View(new MonthlyEarlyandLateReportViewModellist
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/EarlyandLateReport",
                    BreadCrumbTitle = " हाजिरी सारांश विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    //DDLEmployee = empModel,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    FromDateNp = this.Today.Result.Nepdate,
                    TodateDateNp = TodateDateNp,
                    DBModelListRange = await _monthlyAttendanceSummaryServices.GetRangeUpdate(Condition(""), this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, null, this.SessionDetail.IdHREmployee, date, Adddate, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusReportType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListEarlyandLateReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, string startDateNP, string toDateNp, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                DateTime Startdate = (DateTime)this.GetEnglishDate(startDateNP);
                DateTime Todate = (DateTime)this.GetEnglishDate(toDateNp);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new MonthlyEarlyandLateReportViewModellist
                {
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/EarlyandLateReport",
                    BreadCrumbActionName = "_ListEarlyandLateReportAsync",
                    BreadCrumbTitle = "सारांश विवरण",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    FromDateNp = startDateNP,
                    TodateDateNp = toDateNp,
                    IdHREmployee = idHREmployee,
                    IdDivision = idHRCompanyDivision,
                    DBModelListRange = await _monthlyAttendanceSummaryServices.GetRangeUpdate(Condition(), idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, Startdate, Todate, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadLateReportAsync(long? idHRCompany, long? idHREmployee, string startDateNp, string ToDateNp)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DateTime Startdate = (DateTime)this.GetEnglishDate(startDateNp);
                DateTime Todate = (DateTime)this.GetEnglishDate(ToDateNp);

                var StartYear = GetDateByDateNP(startDateNp).NepYear;
                var ToYear = GetDateByDateNP(ToDateNp).NepYear;

                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                var currentdesignation = GetDesignationByIDhremployee(idHREmployee);

                return PartialView(new MonthlyEarlyandLateReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/EarlyandLateReport",
                    BreadCrumbActionName = "_ReadLateReportAsync",
                    FormModelName = "LateCommerReport",
                    ModalTitle = "ढिलो आएको विवरण",
                    CRUDAction = CRUDType.READ,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelAttendanceWorkingRangeList = await _monthlyAttendanceSummaryServices.GetsRange(idHRCompany, idHREmployee, StartYear, ToYear, Startdate, Todate),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = SessionDetail.HRCompanyName,
                        ParentCompanyName = this.SessionDetail.ParentHRCompanyName,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{currentdesignation} {empModel.HREmployeeNameNP} को {startDateNp} देखि {ToDateNp} सम्ममा ढिलो आएको  विवरण "
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadEarlyCheckOutReportAsync(long? idHRCompany, long? idHREmployee, string startDateNp, string ToDateNp)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DateTime Startdate = (DateTime)this.GetEnglishDate(startDateNp);
                DateTime Todate = (DateTime)this.GetEnglishDate(ToDateNp);

                var StartYear = GetDateByDateNP(startDateNp).NepYear;
                var ToYear = GetDateByDateNP(ToDateNp).NepYear;

                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                var currentdesignation = GetDesignationByIDhremployee(idHREmployee);
                return PartialView(new MonthlyEarlyandLateReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/EarlyandLateReport",
                    BreadCrumbActionName = "_ReadEarlyCheckOutReportAsync",
                    FormModelName = "EarlyCheckOutReport",
                    ModalTitle = "छिटो गएको विवरण",
                    CRUDAction = CRUDType.READ,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelAttendanceWorkingRangeList = await _monthlyAttendanceSummaryServices.GetsRange(idHRCompany, idHREmployee, StartYear, ToYear, Startdate, Todate),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = SessionDetail.HRCompanyName,
                        ParentCompanyName = this.SessionDetail.ParentHRCompanyName,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{currentdesignation} {empModel.HREmployeeNameNP}  को {startDateNp} देखि {ToDateNp} सम्ममा छिटो गएको  विवरण "
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }




        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintEarlyandLateReport(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, string startDateNP, string toDateNp)
        {
            try
            {
                var Section = "सबै शाखा";
                DateTime Startdate = (DateTime)this.GetEnglishDate(startDateNP);
                DateTime Todate = (DateTime)this.GetEnglishDate(toDateNp);
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
                    BreadCrumbController = "EarlyandLateReport",
                    BreadCrumbBaseURL = "Reports/EarlyandLateReport",
                    BreadCrumbActionName = "_PrintEarlyandLateReport",
                    FormModelName = "EarlyandLateReport",
                    ModalTitle = "आएको/ छिटो गएको छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelAttendanceRangeList = await _monthlyAttendanceSummaryServices.GetRange(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, Startdate, Todate),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $" {startDateNP} देखि {toDateNp} सम्ममा {Section} (शाखा) को ढिलो आएको/ छिटो गएको रिपोट"
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
                var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
              

                return Json(new { list = hrcompanyModel, dDLDivision = hrDivisionModel, dDLEmployee = empModel}, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}