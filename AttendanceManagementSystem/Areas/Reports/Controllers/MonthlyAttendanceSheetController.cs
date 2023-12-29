using AttendanceManagementSystem.App_Auth;
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
using SystemViewModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemModels.EmployeeManagement;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class MonthlyAttendanceSheetController : BaseController
    {
        private readonly HRCalendarServices _HRCalendarServices;
        private readonly MonthlyAttendanceSheetServices _MonthlyAttendanceSheetServices;
        //private readonly HREmployeeLeaveHistoryServices _HREmployeeLeaveHistoryServices;
        public MonthlyAttendanceSheetController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            _MonthlyAttendanceSheetServices = new MonthlyAttendanceSheetServices(this._unitOfWork);
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
                var model = await _MonthlyAttendanceSheetServices.GetResult(x => x.IdHREmployee == this.SessionDetail.IdHREmployee, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, this.SessionDetail.IdHREmployeeJobStatus, this.SessionDetail.IdHREmployee, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
                return View(new MonthlyAttendanceSheetViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(today.NepYear, today.NepMonth),
                    DBModel = model,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyAttendanceSheet",
                    BreadCrumbBaseURL = "Reports/MonthlyAttendanceSheet",
                    BreadCrumbTitle = "मासिक हाजिरी विवरण",
                    BreadCrumbActionName = "Index",
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
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListMonthlyAttendanceSheetAsync(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var startEndDate = GetStartEndDate(year, month);
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                var DBModel = await _MonthlyAttendanceSheetServices.GetResult(x => x.IdHREmployee == idHREmployee || idHREmployee == 0, idHRCompany, idHRCompanyDivision ?? 0, idJobStatus, idHREmployee ?? 0, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey);
                return PartialView(new MonthlyAttendanceSheetViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    YearNp = year,
                    MonthNp = month,
                    IdJobStatus = idJobStatus,
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModel = await _MonthlyAttendanceSheetServices.GetResult(x => x.IdHREmployee == idHREmployee || idHREmployee == 0, idHRCompany, idHRCompanyDivision ?? 0, idJobStatus, idHREmployee ?? 0, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "MonthlyAttendanceSheet",
                    BreadCrumbBaseURL = "Reports/MonthlyAttendanceSheet",
                    BreadCrumbActionName = "_ListMonthlyAttendanceSheetAsync",
                    BreadCrumbTitle = "Monthly Attendance Sheet",
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
        public async Task<ActionResult> _PrintMonthlyAttendanceSheetAsync(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month)
        {
            try
            {
                var Section = "सबै शाखा";
                var NpMoth = await GetNpMonth(month);
                var startEndDate = GetStartEndDate(year, month);
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);

                var information = await GetCompanyHeaderDetails(idHRCompany);
                Section = await GetDivisionNameNep(idHRCompanyDivision);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;

                MonthlyAttendanceSheetViewModelList modeldata = new MonthlyAttendanceSheetViewModelList();

                modeldata = new MonthlyAttendanceSheetViewModelList
                {
                    DBReportHeader = await _HRCalendarServices.GetMonthlyAttendanceHeader(year, month),
                    DBModelPrint = await _MonthlyAttendanceSheetServices.GetResult(idHRCompany, idHRCompanyDivision ?? 0, idJobStatus, idHREmployee ?? 0, startEndDate.Key, startEndDate.Value),
                    BreadCrumbArea = "Reports",
                    IdJobStatus = idJobStatus,
                    BreadCrumbController = "MonthlyAttendanceSheet",
                    BreadCrumbBaseURL = "Reports/MonthlyAttendanceSheet",
                    BreadCrumbActionName = "_PrintMonthlyAttendanceSheetAsync",
                    BreadCrumbTitle = "Monthly Attendance Sheet",
                    ModalTitle = "मासिक हाजिरी विवरण छाप्नुहोस्",
                    FormModelName = "MonthlyAttendanceSheet",
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
                        ReportName = $"{year}({NpMoth}) को {Section} (शाखा) को {jobStatus} सेवाका कर्मचारीहरुको मासिक हाजिरी विवरण"
                    },

                };

                return PartialView(modeldata);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        //    [AuthorizeUser]
        //    [AcceptVerbs(HttpVerbs.Get)]
        //    public async Task<ActionResult> _CreateHREmployeeLeaveHistoryAsyncPopup(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        //    {
        //        try
        //        {
        //            idHREmployee = idHREmployee == 0 ? this.SessionDetail.IdHREmployee : idHREmployee;
        //            int? yearNP = Today.Result.NepYear;
        //            var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
        //            var model = await _HREmployeeLeaveHistoryServices.GetsEmployeeDetail(idHREmployee, this.SessionDetail.IdHRCompany);
        //            return PartialView(new HREmployeeLeaveHistoryViewModel
        //            {
        //                BreadCrumbArea = "EmployeeManagement",
        //                BreadCrumbController = "HREmployeeLeaveHistory",
        //                BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveHistory",
        //                BreadCrumbActionName = "_CreateHREmployeeLeaveHistoryAsync",
        //                FormModelName = "HREmployeeLeaveHistory",
        //                ModalTitle = "बिदा माग गर्नुहोस्",
        //                BreadCrumbTitle = "बिदा अनुरोध",
        //                CRUDAction = CRUDType.CREATE,
        //                SubmitButtonType = SubmitButtonType.submit,
        //                SessionDetails = this.SessionDetail,
        //                SubmitButtonText = SubmitButtonText.Create,
        //                SubmitButtonID = SubmitButtonID.btnSubmit,
        //                CancelButtonID = CancelButtonID.btnCancel,
        //                CancelButtonText = CancelButtonText.Cancel,
        //                PageNumber = pagination.PageNumber,
        //                PageSize = pagination.PageSize,
        //                OrderingBy = pagination.OrderingBy,
        //                OrderingDirection = pagination.OrderingDirection,
        //                IdHREmployee = model.Id,
        //                SearchKey = string.Empty,
        //                DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, idHREmployee),
        //                DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, idHREmployee),
        //                DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, idHREmployee),
        //                DataModel = new HREmployeeLeaveHistoryModel
        //                {
        //                    LeaveValidFrom = this.Today.Result.EngDate,
        //                    LeaveValidTo = this.Today.Result.EngDate,
        //                    LeaveValidFromNP = this.Today.Result.Nepdate,
        //                    IdHREmployee = model.Id,
        //                    IdJob = (long)model.IdJob,
        //                    EmployeeName = model.HREmployeeNameNP,
        //                    IdHRCompany = (long)model.IdHRCompany,
        //                    Designation = model.HRDesignationTitle,
        //                    DesignationRank = model.HRDesignationRank,
        //                    Division = model.HRCompanyDivisionName,
        //                    LeaveYearNP = yearNP,
        //                    IsActive = true,
        //                    PISNumber = model.PISNumber,
        //                    Gender = model.HREmployeeSexTitle,
        //                    Mobile = model.MobileNumber,
        //                    JobStatus = model.JobStatus,
        //                    HRCompanyName = model.CompanyNameNP,
        //                    ImageName = model.ImageName,
        //                    AppoinmentDateNP = model.AppointmentDateNP,
        //                },
        //                DBModelLeaveSummary = await _HREmployeeLeaveHistoryServices.GetsEmployeeLeaveSummary(idHREmployee, yearNP)
        //            });
        //        }
        //        catch (Exception exp)
        //        {
        //            return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
        //        }
        //    }
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