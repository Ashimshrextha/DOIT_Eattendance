using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
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
    public class WorkedReportController : BaseController
    {
        // GET: Reports/WorkedReport
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly WorkedReportServices _workedReportServices;

        public WorkedReportController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._workedReportServices = new WorkedReportServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = _workedReportServices.GetPagedList(SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, null, this.SessionDetail.IdHREmployee, today.EngDate, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return View(new WorkedReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "WorkedReport",
                    BreadCrumbBaseURL = "Reports/WorkedReport",
                    BreadCrumbTitle = "दैनिक काम गरेको विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    Date = today.EngDate,
                    DateNp = today.NepDate,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    DBModelList = model,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    //DDLEmployee = empModel,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWorkedReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new WorkedReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    Date = date,
                    IdJobStatus = idJobStatus,
                    IdHREmployee = idHREmployee,
                    DBModelList = _workedReportServices.GetPagedList(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "WorkedReport",
                    BreadCrumbBaseURL = "Reports/WorkedReport",
                    BreadCrumbActionName = "_ListWorkedReportAsync",
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
        public async Task<ActionResult> _PrintWorkedReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date)
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

                WorkedReportViewModelList modeldata = new WorkedReportViewModelList();

                modeldata = new WorkedReportViewModelList
                {
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "WorkedReport",
                    BreadCrumbBaseURL = "Reports/WorkedReport",
                    BreadCrumbActionName = "_PrintWorkedReportAsync",
                    FormModelName = "WorkedReport",
                    ModalTitle = "दैनिक काम गरेको विवरण छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBList = _workedReportServices.Gets(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{today.NepYear}({NpMoth}) को {Section} (शाखा) को {jobStatus} कर्मचारीहरुको दैनिक काम गरेको विवरण"
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