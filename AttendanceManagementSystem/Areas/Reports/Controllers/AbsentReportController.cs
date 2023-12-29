using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemServices.Reports;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class AbsentReportController : BaseController
    {
        // GET: Reports/AbsentReport

        private readonly AbsentReportServices _AbsentReportServices;
        public AbsentReportController()
        {
            _AbsentReportServices = new AbsentReportServices(this._unitOfWork);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy = "HRDesignationRank", string orderingDirection = "ASC")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new AbsentReportViewModelList
                {
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    DBModelPageList = await _AbsentReportServices.GetResult(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, this.SessionDetail.IdHREmployeeJobStatus, DateTime.Now.Date, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "AbsentReport",
                    BreadCrumbBaseURL = "Reports/AbsentReport",
                    BreadCrumbTitle = "अनुपस्थित विवरण",
                    BreadCrumbActionName = "Index",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    Date = DateTime.Now.Date,
                    SessionDetails = SessionDetail,
                    DateNp = this.Today.Result.Nepdate,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType),
                    //DDLCompany = await GetLongStringPairModelReport(PairModelTitle.HRCompany),
                    //DDLDivision = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListAbsentReportAsync(long? idHRCompany, long? idDivision, int? idJobStatus, string dateNP, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                DateTime date = (DateTime)this.GetEnglishDate(dateNP);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new AbsentReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idDivision,
                    Date = date,
                    DateNp = dateNP,
                    DBModelPageList = await _AbsentReportServices.GetResult(idHRCompany, idDivision, idJobStatus, date, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "AbsentReport",
                    BreadCrumbBaseURL = "Reports/AbsentReport",
                    BreadCrumbActionName = "_ListAbsentReportAsync",
                    BreadCrumbTitle = "अनुपस्थित विवरण",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdJobStatus = idJobStatus,
                    SessionDetails = SessionDetail,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintAbsentReportAsync(long? idHRCompany, long? idDivision, int? idJobStatus, string dateNP)
        {
            try
            {
                var Section = "सबै शाखा";
                var NpMoth = await GetNpMonth(dateNP);
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);
                Section = await GetDivisionNameNep(idDivision);
                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                AbsentReportViewModelList modeldata = new AbsentReportViewModelList();

                modeldata = new AbsentReportViewModelList
                {
                    DBModelList = await _AbsentReportServices.GetResult(idHRCompany, idDivision, idJobStatus, (DateTime)this.GetEnglishDate(dateNP)),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "AbsentReport",
                    BreadCrumbBaseURL = "Reports/AbsentReport",
                    BreadCrumbActionName = "_PrintAbsentReportAsync",
                    FormModelName = "AbsentReport",
                    ModalTitle = "अनुपस्थित भएका कर्मचारीहरुको विवरण छाप्नुहोस्",
                    BreadCrumbTitle = "अनुपस्थित विवरण",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    SessionDetails = SessionDetail,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{this.Today.Result.NepYear}({NpMoth}) को {Section} (शाखा) को {jobStatus} सेवाका कर्मचारीहरुको अनुपस्थित विवरण"
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
                var DDLDivision = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var DDLEmployee = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return Json(new { list = list, dDLDivision = DDLDivision }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}