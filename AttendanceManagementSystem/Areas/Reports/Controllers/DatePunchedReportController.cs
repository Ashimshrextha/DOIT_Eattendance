using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AttendanceManagementSystem.Controllers.BaseController;
using static SystemStores.ENUMData.EnumGlobal;
using SystemViewModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemServices.SystemAuthentication;


using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemModels.SystemAuthentication;
using SystemServices.EmployeeManagement;
using SystemServices.SystemAuthentication;
using SystemViewModels.EmployeeManagement;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
using SystemViewModels.DeviceManagement;
using SystemServices.DeviceManagement;
using SystemViewModels.Reports;
using DeviceManagement.Models;
using SystemServices.Reports;
using SystemServices.SystemSecurity;
using SystemModels.SystemSecurity;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class DatePunchedReportController : BaseController
    {

        private readonly LoginsServices _loginsServices;
        private readonly HREmployeeServices _hREmployeeservices;
        private readonly MembershipProviderServices _MembershipProviderServices;
        private readonly DeviceLogsServices _DeviceLogsServices;
        private readonly MonthlyAttendanceSummaryServices _monthlyAttendanceSummaryServices;
        private readonly HRCalendarServices _HRCalendarServices;

        [NonAction]
        public Func<proc_GetMonthlyWorkingSummaryReport_Result, bool> Condition(string searchKey = "")
        {
            Func<proc_GetMonthlyWorkingSummaryReport_Result, bool> returnData = (x => false);
            returnData = (x =>
            (x.EmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.PISNumber.ToString().Contains(searchKey) || searchKey == ""));
            return returnData;
        }

        public DatePunchedReportController()
        {
            this._loginsServices = new LoginsServices(this._unitOfWork);
            this._hREmployeeservices = new HREmployeeServices(this._unitOfWork);
            this._MembershipProviderServices = new MembershipProviderServices(this._unitOfWork);
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._monthlyAttendanceSummaryServices = new MonthlyAttendanceSummaryServices(this._unitOfWork);
            this._DeviceLogsServices = new DeviceLogsServices(this._unitOfWork);
        }
        // GET: Reports/DatePunchedReport
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                var startEndDate = GetStartEndDate(today.NepYear, today.NepMonth);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, "");
              
                return View(new DatePunchedReportViewModel
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "DatePunchedReport",
                    BreadCrumbBaseURL = "Reports/DatePunchedReport",
                    BreadCrumbTitle = "हाजिरी सारांश विवरण",
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
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus,

                    DBModelList = await _monthlyAttendanceSummaryServices.GetPagedList(Condition(""), this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, null, this.SessionDetail.IdHREmployee, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    DBModelEmp = model,
                    DDLHREmployeePositionStatusType = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                }); ;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

     
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var startEndDate = GetStartEndDate(year, month);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, "");
                return PartialView(new DatePunchedReportViewModel
                {
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "DatePunchedReport",
                    BreadCrumbBaseURL = "Reports/DatePunchedReport",
                    BreadCrumbActionName = "_ListHREmplployeeAsync",
                    BreadCrumbTitle = "हाजिरी सारांश विवरण",
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
                   
                    DBModelList = await _monthlyAttendanceSummaryServices.GetPagedList(Condition(), idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, startEndDate.Key, startEndDate.Value, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    DBModelEmp = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadDatePunchedReportAsync(string PISNumber,long idHRCompany, long? idHREmployee, int year, int month)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var startEndDate = GetStartEndDate(year, month);
                var NpMoth = await GetNpMonth(month);
                var empModel = await this._hREmployeeservices.GetByIdAsync(idHREmployee);
                var designation = GetDesignationByIDhremployee(idHREmployee);

                var EnrollId=GetEnrollByPiSNumber(PISNumber);

                var model = await _DeviceLogsServices.GetResultByPunchedDate(EnrollId, idHRCompany, startEndDate.Key,startEndDate.Value);

                return PartialView(new DeviceDatePunchedLogsViewModelList
                {

                    DbGetDeviceLogs = model,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "DatePunchedReport",
                    BreadCrumbBaseURL = "Reports/_ListDatePunchedReport",
                    BreadCrumbTitle = "उपकरण लगहरू",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    FormModelName = "DatePunchedReport",
                    ModalTitle = "पन्च लग",

                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,

                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = SessionDetail.HRCompanyName,
                        ParentCompanyName = this.SessionDetail.ParentHRCompanyName,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year}({NpMoth}) {designation} {empModel.HREmployeeNameNP} पन्च लग"
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintDatePunchedReportAsync(long idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, int year, int month)
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


               return PartialView(new DatePunchedReportViewModel
               {

                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "DatePunchedReport",
                    BreadCrumbBaseURL = "Reports/DatePunchedReport",
                    BreadCrumbActionName = "_PrintDatePunchedReportAsync",
                    FormModelName = "DatePunchedReport",
                    ModalTitle = "कर्मचारी",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelCollection = await _monthlyAttendanceSummaryServices.Gets(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, startEndDate.Key, startEndDate.Value),
                    
                 
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{year}({NpMoth}) को {Section} (शाखा) को {jobStatus} कर्मचारीहरु"
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
}