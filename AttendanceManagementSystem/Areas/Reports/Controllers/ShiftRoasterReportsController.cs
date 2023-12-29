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
    public class ShiftRoasterReportsController : BaseController
    {
        // GET: Reports/ShiftRoasterReports
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly ShiftRoasterReportServices _shiftRoasterReportServices;

        public ShiftRoasterReportsController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._shiftRoasterReportServices = new ShiftRoasterReportServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", "ASC");
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                var shiftTitleModel = await GetLongStringPairModelReport(PairModelTitle.ShiftTitle, this.SessionDetail.IdHRCompany);
                return View(new ShiftRoasterReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "ShiftRoasterReports",
                    BreadCrumbBaseURL = "Reports/ShiftRoasterReports",
                    BreadCrumbTitle = "शिफ्ट रोस्टर",
                    BreadCrumbActionName = "Index",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DateNp = today.NepDate,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    DDLShiftTitle = shiftTitleModel,
                    DBModelList = await this._shiftRoasterReportServices.GetResult(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, 0, DateTime.Now)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListShiftRoasterReports(long? idHRCompany, long? idHRCompanyDivision, long? idShiftTitle, string dateNP, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.NepDate == dateNP);
            var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", "ASC");
            try
            {
                return PartialView(new ShiftRoasterReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "ShiftRoasterReports",
                    BreadCrumbBaseURL = "Reports/ShiftRoasterReports",
                    BreadCrumbTitle = "शिफ्ट रोस्टर",
                    BreadCrumbActionName = "_ListShiftRoasterReports",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    idShiftTitle = idShiftTitle,
                    DateNp = dateNP,
                    DBModelList = await this._shiftRoasterReportServices.GetResult(idHRCompany, idHRCompanyDivision, idShiftTitle, today.EngDate, searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintShiftRoasterReportAsync(long? idHRCompany, long? idHRCompanyDivision, long? idShiftTitle, string dateNP)
        {
            try
            {
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.NepDate == dateNP);

                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                ShiftRoasterReportViewModelList modeldata = new ShiftRoasterReportViewModelList();

                modeldata = new ShiftRoasterReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "ShiftRoasterReports",
                    BreadCrumbBaseURL = "Reports/ShiftRoasterReports",
                    BreadCrumbTitle = "शिफ्ट रोस्टर छाप्नुहोस्",
                    ModalTitle = "शिफ्ट रोस्टर छाप्नुहोस्",
                    BreadCrumbActionName = "_PrintShiftRoasterReportAsync",
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    idShiftTitle = idShiftTitle,
                    DateNp = dateNP,
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await this._shiftRoasterReportServices.GetResult(idHRCompany, idHRCompanyDivision, idShiftTitle, today.EngDate),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{dateNP}  को  कर्मचारीहरुको  शिफ्ट  रोस्टर  विवरण"
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
                return Json(new { list = list, dDLDivision = hrDivisionModel }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}