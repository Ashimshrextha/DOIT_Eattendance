using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemServices.Reports;
using SystemStores.PairModels;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class DailyReportController : BaseController
    {
        // GET: Reports/DailyReport

        private readonly DailyReportServices _dailyReportServices;

        public DailyReportController()
        {
            this._dailyReportServices = new DailyReportServices(this._unitOfWork);
        }


        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize)
        {

            var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");


            return View(new DailyReportViewModelList
            {

                DBModelPageList = await _dailyReportServices.GetPagedList(SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, this.SessionDetail.IdHREmployeeJobStatus, this.SessionDetail.IdHREmployee, this.Today.Result.EngDate, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                BreadCrumbArea = "Reports",
                BreadCrumbController = "DailyReport",
                BreadCrumbBaseURL = "Reports/DailyReport",
                BreadCrumbTitle = "दैनिक विवरण",
                BreadCrumbActionName = "Index",
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                OrderingBy = pagination.OrderingBy,
                OrderingDirection = pagination.OrderingDirection,
                IdHRCompany = SessionDetail.IdHRCompany,
                IdDivision = SessionDetail.IdHRCompanyDivision,
                IdHREmployee = SessionDetail.IdHREmployee,
                Date = this.Today.Result.EngDate,
                DateNp = this.Today.Result.Nepdate,
                IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,
                DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
            });



        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListDailyReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, string dateNP, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                DateTime date = (DateTime)this.GetEnglishDate(dateNP);
                return PartialView(new DailyReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    DateNp = dateNP,
                    DBModelPageList = await _dailyReportServices.GetPagedList(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "DailyReport",
                    BreadCrumbBaseURL = "Reports/DailyReport",
                    BreadCrumbActionName = "_ListDailyReportAsync",
                    BreadCrumbTitle = "दैनिक विवरण",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdJobStatus = idJobStatus
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintDailyReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, string dateNP)
        {
            
            try
            {
                var Section = "सबै शाखा";
                DateTime date = (DateTime)this.GetEnglishDate(dateNP);
                var NpMoth = await GetNpMonth(dateNP);
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);
                Section = await GetDivisionNameNep(idHRCompanyDivision);
                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                DailyReportViewModelList modeldata = new DailyReportViewModelList();

                modeldata = new DailyReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    DateNp = dateNP,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "DailyReport",
                    BreadCrumbBaseURL = "Reports/DailyReport",
                    BreadCrumbActionName = "_PrintDailyReportAsync",
                    FormModelName = "DailyReport",
                    ModalTitle = "दैनिक विवरण छाप्नुहोस्",
                    BreadCrumbTitle = "दैनिक विवरण",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdRole = SessionDetail.IdRoleType,
                    IdJobStatus = idJobStatus,
                    DBModelList = await _dailyReportServices.GetResult(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{dateNP} को {Section} (शाखा) को {jobStatus} सेवाका कर्मचारीहरुको दैनिक हाजिरी"
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
                var list = await  GetLongStringPairModelReport(PairModelTitle.HRCompany);
                var DDLDivision = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var DDLEmployee = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                var DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType);
                return Json(new { list = list, dDLDivision = DDLDivision, dDLEmployee= DDLEmployee, dDLHREmployeePositionStatusType= DDLHREmployeePositionStatusType }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}