using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemServices.Reports;
using SystemServices.SystemSecurity;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class YearlyAttendanceController : BaseController
    {
        // GET: Reports/YearlyAttendance
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly YearlyAttendanceServices _yearlyAttendanceServices;

        public YearlyAttendanceController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._yearlyAttendanceServices = new YearlyAttendanceServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                return View(new YearlyAttendanceViewModelList
                {
                    DBModel = await _yearlyAttendanceServices.Gets(this.SessionDetail.IdHREmployee, null, Today.Result.NepYear),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "YearlyAttendance",
                    BreadCrumbBaseURL = "Reports/YearlyAttendance",
                    BreadCrumbTitle = "वार्षिक हाजिरी विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    //DDLEmployee = empModel,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListYearlyAttendanceAsync(long? idHREmployee, int? idJobStatus, int yearNP)
        {
            try
            {
                if (idHREmployee == 0 || idHREmployee == null)
                {
                    return await this.AlertNotification("Error", "Please Select Valid Employee", AlertNotificationType.error);
                }
                var empModel = await this.GetLongStringPairModel(PairModelTitle.EmployeeById, 0, idHREmployee);
                return PartialView(new YearlyAttendanceViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "YearlyAttendance",
                    BreadCrumbBaseURL = "Reports/YearlyAttendance",
                    BreadCrumbActionName = "_ListYearlyAttendanceAsync",
                    BreadCrumbTitle = "Yearly Attendance Reports",
                    CRUDAction = CRUDType.READ,
                    IdHREmployee = idHREmployee,
                    YearNp = yearNP,
                    HeaderTitle = $"{yearNP} सालको {empModel.FirstOrDefault().Value} को वार्षिक हाजिरी विवरण",
                    DBModel = await _yearlyAttendanceServices.Gets(idHREmployee, idJobStatus, yearNP)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintYearlyAttendanceAsync(long? idHRCompany, int? idJobStatus, long? idHREmployee,  int yearNP)
        {
            try
            {
                if (idHREmployee == 0 || idHREmployee == null)
                {
                    return await this.AlertNotification("Error", "Please Select Valid Employee", AlertNotificationType.error);
                }
                var empModel = await this.GetLongStringPairModel(PairModelTitle.EmployeeById, 0, idHREmployee);

                var information = await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                YearlyAttendanceViewModel modeldata = new YearlyAttendanceViewModel();

                modeldata = new YearlyAttendanceViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "YearlyAttendance",
                    BreadCrumbBaseURL = "Reports/YearlyAttendance",
                    BreadCrumbActionName = "_PrintYearlyAttendanceAsync",
                    FormModelName = "YearlyAttendance",
                    ModalTitle = "वार्षिक हाजिरी विवरण सुची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _yearlyAttendanceServices.Gets(idHREmployee, idJobStatus, yearNP),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{yearNP}  सालको  {empModel.FirstOrDefault().Value}  को  वार्षिक  हाजिरी  विवरण"
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