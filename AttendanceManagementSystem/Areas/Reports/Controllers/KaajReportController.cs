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
    public class KaajReportController : BaseController
    {
        // GET: Reports/KaajReport
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly KaajReportServices _kaajReportServices;

        public KaajReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._kaajReportServices = new KaajReportServices(this._unitOfWork);
        }

        [NonAction]
        public Func<proc_GetMonthlyKaajReport_Result, bool> Condition(string searchKey = "")
        {
            Func<proc_GetMonthlyKaajReport_Result, bool> returnData = (x => false);
            returnData = (x =>
               (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.IdEnroll.ToString().Contains(searchKey) || searchKey == ""));
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
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return View(new KaajReportViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(today.NepYear, today.NepMonth),
                    DBModelKaajList = await _kaajReportServices.GetKaajReport(Condition(""), SessionDetail.IdHRCompany, SessionDetail.IdHREmployee, SessionDetail.IdHRCompanyDivision, null,startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "KaajReport",
                    BreadCrumbBaseURL = "Reports/KaajReport",
                    BreadCrumbTitle = "काज विवरण",
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
                    IdJobStatus=this.SessionDetail.IdHREmployeeJobStatus,
                    //DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month),
                    FromDateNp = Today.Result.Nepdate,
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListKaajReportAsync(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var startEndDate = GetStartEndDate(year, month);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new KaajReportViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModelKaajList = await _kaajReportServices.GetKaajReport(Condition(searchKey), idHRCompany, idHREmployee, idHRCompanyDivision, idJobStatus, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "KaajReport",
                    BreadCrumbBaseURL = "Reports/KaajReport",
                    BreadCrumbActionName = "_ListKaajReportAsync",
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
                    SearchKey=searchKey,
                    IdJobStatus=idJobStatus
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintKaajReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month)
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

                KaajReportViewModel modeldata = new KaajReportViewModel();

                modeldata = new KaajReportViewModel
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModelKaajList = await _kaajReportServices.GetKaajReport(idHRCompany, idHREmployee, idHRCompanyDivision, null, startEndDate.Key, startEndDate.Value),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "KaajReport",
                    BreadCrumbBaseURL = "Reports/KaajReport",
                    BreadCrumbActionName = "_PrintKaajReportAsync",
                    FormModelName = "KaajReport",
                    ModalTitle = "काज विवरण सुची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdJobStatus = idJobStatus,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year} ({NpMoth}) को {Section} (शाखा) को {jobStatus} सेवाका कर्मचारीहरुको मासिक काज विवरण"
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

                return Json(new { list = list, dDLDivision = hrDivisionModel, dDLEmployee = empModel, yearNp = DDLYearNp, monthNp = DDLMonthNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


    }
}