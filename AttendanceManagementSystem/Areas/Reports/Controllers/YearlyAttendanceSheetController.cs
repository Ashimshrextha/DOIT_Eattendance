using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemServices.Reports;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class YearlyAttendanceSheetController : BaseController
    {
        // GET: Reports/YearlyAttendanceSheet

        private readonly YearlyAttendanceSheetServices _yearlyAttendanceSheetServices;

        public YearlyAttendanceSheetController()
        {
            this._yearlyAttendanceSheetServices = new YearlyAttendanceSheetServices(this._unitOfWork);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                //var hrcompanyModel = await GetLongStringPairModelReport(PairModelTitle.HRCompany);
                //var hrDivisionModel = await GetLongStringPairModelReport(PairModelTitle.Division, SessionDetail.IdHRCompany);
                //var empModel = await GetLongStringPairModelReport(PairModelTitle.Employee, this.SessionDetail.IdHRCompany);
                string employeeName = string.Empty;
                return View(new YearlyAttendanceSheetViewModel
                {
                    DBModelList = await _yearlyAttendanceSheetServices.Gets(this.SessionDetail.IdHREmployee, null, Today.Result.NepYear),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "YearlyAttendanceSheet",
                    BreadCrumbBaseURL = "Reports/YearlyAttendanceSheet",
                    BreadCrumbTitle = "वार्षिक हाजिरी विवरण",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdDivision = SessionDetail.IdHRCompanyDivision,
                    IdHREmployee = SessionDetail.IdHREmployee,
                    YearNp = Today.Result.NepYear,
                    //DDLCompany = hrcompanyModel,
                    //DDLDivision = hrDivisionModel,
                    //DDLEmployee = empModel,
                    //DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    HeaderTitle = $"{Today.Result.NepYear} सालको {employeeName} को वार्षिक हाजिरी विवरण",
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListYearlyAttendanceSheetAsync(long? idHREmployee, int? idJobStatus, int yearNP)
        {
            try
            {
                if (idHREmployee == 0 || idHREmployee == null)
                {
                    return await this.AlertNotification("Error", "Please Select Valid Employee", AlertNotificationType.error);
                }
                var empModel = await this.GetLongStringPairModel(PairModelTitle.EmployeeById, 0, idHREmployee);
                return PartialView(new YearlyAttendanceSheetViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "YearlyAttendanceSheet",
                    BreadCrumbBaseURL = "Reports/YearlyAttendanceSheet",
                    BreadCrumbActionName = "_ListYearlyAttendanceSheetAsync",
                    BreadCrumbTitle = "Yearly Attendance Sheet Reports",
                    CRUDAction = CRUDType.READ,
                    IdHREmployee = idHREmployee,
                    YearNp = yearNP,
                    HeaderTitle = $"{yearNP} सालको {empModel.FirstOrDefault().Value} को वार्षिक हाजिरी विवरण",
                    DBModelList = await _yearlyAttendanceSheetServices.Gets(idHREmployee, idJobStatus, yearNP)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintYearlyAttendanceSheetAsync(long? idHRCompany, int? idJobStatus, long? idHREmployee,  int yearNP )
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

                YearlyAttendanceSheetViewModel modeldata = new YearlyAttendanceSheetViewModel();

                modeldata = new YearlyAttendanceSheetViewModel
                {                                                                 
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "YearlyAttendanceSheet",
                    BreadCrumbBaseURL = "Reports/YearlyAttendanceSheet",
                    BreadCrumbActionName = "_PrintYearlyAttendanceSheetAsync",
                    FormModelName = "YearlyAttendanceSheet",
                    ModalTitle = "वार्षिक हाजिरी विवरण सुची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _yearlyAttendanceSheetServices.Gets(idHREmployee, idJobStatus, yearNP),
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
                var DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year);

                return Json(new { list = list, dDLDivision = hrDivisionModel, dDLEmployee = empModel, yearNp = DDLYearNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}