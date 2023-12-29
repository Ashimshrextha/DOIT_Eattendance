using Antlr.Runtime.Misc;
using AttendanceManagementSystem.App_Auth;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR.Messaging;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using SystemDatabase;
using SystemInterfaces.EmployeeManagement;
using SystemModels.SystemSecurity;
using SystemServices.CompanyManagement;
using SystemServices.EmployeeManagement;
using SystemServices.Home;
using SystemServices.SystemSecurity;
using SystemViewModels.Home;
using SystemViewModels.Shared;
using static System.Collections.Specialized.BitVector32;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HRCompanyServices _hRCompanyServices;
        private readonly HREmployeeServices _hREmployeeServices;
        private readonly HREmployeeAttendanceHistoryServices _hREmployeeAttendanceHistoryServices;
        private readonly HRDeviceServices _hRDeviceServices;
        private readonly HomeServices _homeServices;
        private readonly HRCalendarServices _HRCalendarServices;


        private readonly HREmployeeLeaveBalanceServices _hREmployeeLeaveBalanceServices;
        private readonly HREmployeeLeaveHistoryServices _HREmployeeLeaveHistoryServices;
        private readonly HREmployeeKaajHistoryServices _HREmployeeKaajHistoryServices;



        public HomeController()
        {
            this._hRCompanyServices = new HRCompanyServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
            this._hREmployeeAttendanceHistoryServices = new HREmployeeAttendanceHistoryServices(this._unitOfWork);
            this._hRDeviceServices = new HRDeviceServices(this._unitOfWork);
            this._homeServices = new HomeServices(this._unitOfWork);
            this._HRCalendarServices = new HRCalendarServices(this._unitOfWork);
            this._hREmployeeLeaveBalanceServices = new HREmployeeLeaveBalanceServices(this._unitOfWork);
            this._HREmployeeLeaveHistoryServices = new HREmployeeLeaveHistoryServices(this._unitOfWork);
            this._HREmployeeKaajHistoryServices = new HREmployeeKaajHistoryServices(this._unitOfWork);
        }


        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                bool userPermission = this.SessionDetail.IdRoleType == (int)RoleType.SuperUser || this.SessionDetail.IdRoleType == (int)RoleType.Admin || this.SessionDetail.IdRoleType == (int)RoleType.RootUser;
                var todayDate = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == todayDate);
                var startEndDate = await GetStartEndDateNew(today.NepYear, today.NepMonth);

                var company = this.SessionDetail.IdHRCompany;
                var role = this.SessionDetail.IdRoleType;
                var emp = this.SessionDetail.IdHREmployee;
                var roletype = role == 0 ? 0 : company;

                HomeDashboardViewModelList Information = new HomeDashboardViewModelList();


                Information = new HomeDashboardViewModelList
                {

                    BreadCrumbTitle = "ड्यासबोर्ड",
                    BreadCrumbArea = "",
                    BreadCrumbController = "Home",
                    BreadCrumbActionName = "Index",
                    BreadCrumbBaseURL = "Home/Index",


                    DbGender = null,
                    idRoleType = role,
                    DBModel = null,
                    DBModelPresentEmpList = null,
                    DBModelAbsentEmpList = null,
                    DBModelKaajEmpList = null,
                    DBModelLeaveEmpList = null,
                    OfficeCount = null,
                    ActiveOfficeCount = null,
                    EmployeeCount = null,
                    OfficeDeviceList = null,
                    DBModelShift = null,
                    DBModelLeavBalance = null,
                    EmployeePresentCount = null,

                    DBModelCalendar = null,
                    DbEmployeeTodayStat = await _hREmployeeServices.GetEmployeeTodayStatusForDashboard(roletype, role),

                    DBModelLeaveHistory = null,
                    DBModelKaajHistory = null,
                    DBModelRequestedKaajHistory = null,




                    DBModelKaajApprovalHistory = null,
                    DBModelLeaveApprovalHistory = null,
                    DBModelRequestedLeaveHistory = null,
                 


                    ParentChildViewModelList = null,
                    OfficeStatus = null,

                    SessionDetails = this.SessionDetail,
                    TodayDayNp = this.Date(DateTime.Now).NepDay.ToString(),
                    YearNp = this.Date(DateTime.Now).NepYear,
                    MonthNp = this.Date(DateTime.Now).NepMonth,

                };

                return View(Information);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("", exp.Message, AlertNotificationType.error);
            }
        }


        #region OLD_WORKING
        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _List()
        {
            try
            {
                return PartialView(new HomeDashboardViewModelList
                {
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _GetCountEmployee()
        {
            try
            {
                return PartialView(new HighChartModel { HighChart = TotalEmployeeCountByGender() });
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public Highcharts TotalEmployeeCountByGender()
        {
            try
            {
                var model = _hREmployeeServices.GetsEmployeeCountByGender(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType).Result;

                Highcharts chart = new Highcharts("EmployeeCount")
                    .InitChart(new Chart
                    {
                        PlotBackgroundColor = null,
                        PlotBorderWidth = 0,
                        PlotShadow = false
                    })
                    .SetTitle(new Title
                    {
                        Text = "Total Employees",
                        Align = HorizontalAligns.Center,
                        VerticalAlign = VerticalAligns.Top,
                        Y = 10
                    })
                    .SetCredits(new Credits { Enabled = false })
                    .SetExporting(new Exporting { Enabled = false })
                    .SetPlotOptions(new PlotOptions
                    {
                        Pie = new PlotOptionsPie
                        {
                            DataLabels = new PlotOptionsPieDataLabels
                            {
                                Enabled = true,
                                Distance = -50
                            },
                            StartAngle = -90,
                            EndAngle = 90,
                            Center = new PercentageOrPixel[] { new PercentageOrPixel(50, true), new PercentageOrPixel(75, true) }
                        }
                    })
                   .SetSeries(new Series
                   {
                       Name = "Total",
                       Type = ChartTypes.Pie,
                       Data = new Data(new object[] { new object[] { "Male", model.FirstOrDefault(x => x.HREmployeeSexTitle == "Male").TotalNumber }, new object[] { "Female", model.FirstOrDefault(x => x.HREmployeeSexTitle == "Female").TotalNumber } })
                   });
                return chart;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> HRCompanyAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompanyType", "ASC");
                return View(new HomeDashboardViewModelList
                {
                    HRCompanyList = await _hRCompanyServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "HRCompanyAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/HRCompanyAsync",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "Active Office"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompanyType", "ASC");
                return PartialView(new HomeDashboardViewModelList
                {
                    HRCompanyList = await _hRCompanyServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ListHRCompanyAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/HRCompanyAsync",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "Active Office"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> HREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompany", "DESC");
                return View(new HomeDashboardViewModelList
                {
                    DBModelEmp = await _hREmployeeServices.GetPagedList(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "HREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "कर्मचारी"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {

            try
            {

                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompany", "DESC");
                return PartialView(new HomeDashboardViewModelList
                {
                    DBModelEmp = await _hREmployeeServices.GetPagedList(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ListHRCompanyAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "कर्मचारी"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> PresentHREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return View(new HomeDashboardViewModelList
                {
                    DBModelPresentEmp = await _homeServices.GetPresentEmployee(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType, 0, DateTime.Now, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "PresentHREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "उपस्थित कर्मचारीहरू"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintPresentHREmployeeAsync()
        {
            var idHRCompany = this.SessionDetail.IdHRCompany;

            var information = await GetCompanyHeaderDetails(idHRCompany);
            string CompanyNameNP = information.CompanyNameNP;
            string ParentCompanyNameNP = information.ParentCompanyNameNP;

            try
            {

                return PartialView(new HomeDashboardViewModelList
                {
                    DBModelPresentEmpList = await _homeServices.GetPresentEmployee(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, 0, this.SessionDetail.IdRoleType, DateTime.Now),
                    BreadCrumbActionName = "_PrintPresentHREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/_PrintPresentHREmployeeAsync",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "उपस्थित कर्मचारी सूची छाप्नुहोस्",
                    ModalTitle = "उपस्थित कर्मचारी सूची छाप्नुहोस्",
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
                        ReportName = $"{Today.Result.Nepdate}  को निजामती सेवाका कर्मचारीहरुको दैनिक उपस्थित"
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListPresentHREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new HomeDashboardViewModelList
                {
                    DBModelPresentEmp = await _homeServices.GetPresentEmployee(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType, 0, DateTime.Now, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ListPresentHREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "उपस्थित कर्मचारीहरू"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> HRDevicesAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "DeviceNumber", "DESC");
                var model = await _hRDeviceServices.GetsByCompany(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
                return View(new HomeDashboardViewModelList
                {
                    HRDeviceList = model,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "HRDevicesAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "उपकरणहरू"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRDevicesAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "DeviceNumber", "DESC");
                return PartialView(new HomeDashboardViewModelList
                {
                    HRDeviceList = await _hRDeviceServices.GetPagedList((x => x.DeviceNumber.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || x.HRCompany.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || x.IPAddress.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || x.SerialNumber.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || x.HRDeviceName.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || x.HRDeviceName.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || x.CommunicationPort.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || x.HRDeviceModel1.HRDeviceModel1.ToString().ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ListHRDevicesAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "उपकरणहरू"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        #endregion



        #region TODO

        #region TODO
        [AllowAnonymous]

        public async Task<JsonResult> OfficeCount()
        {
            try
            {
                int OfficeCount = await _hRCompanyServices.CountAsync(x => x.IsActive || x.IsActive == false);
                return Json(OfficeCount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]

        public async Task<JsonResult> ActiveOfficeCount()
        {
            try
            {
                int ActiveOfficeCount = await _hRCompanyServices.CountAsync(x => x.IsActive);
                return Json(ActiveOfficeCount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }




        [AllowAnonymous]
        public async Task<JsonResult> ShiftInformation()
        {
            try
            {
                //var todayDate = DateTime.Now.Date;
                //var DBModelShift = await _homeServices.GetShiftRoaterAsync(0, 0, todayDate, "");
                int ActiveDevice = await _hRDeviceServices.CountAsync(x => (x.DeviceNumber > 0) && x.IsActive == true);
                int DeactiveDevice = await _hRDeviceServices.CountAsync(x => (x.DeviceNumber > 0) && x.IsActive == false);

                return Json(new { ActiveDevice = ActiveDevice, DeactiveDevice = DeactiveDevice }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        [AllowAnonymous]
        public async Task<JsonResult> AssignUnAssign()
        {
            try
            {

                var DBModelShift = await _homeServices.GetShiftRoaterAsync(0, 0, DateTime.Now.Date, "");
                var assign = DBModelShift.Where(x => x.LoginTime != null && x.LogoutTime != null).ToList();
                int assignCount = assign.Count;
                var unAssigned = DBModelShift.Where(x => x.LoginTime == null && x.LogoutTime == null).ToList();
                int unassignCount = unAssigned.Count;

                return Json(new { assign = assign.Take(20), unAssigned = unAssigned.Take(20), assignCount = assignCount, unassignCount = unassignCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region TODO1







        [AllowAnonymous]
        public async Task<JsonResult> PresentEmployeeList()
        {
            try
            {

                var PresentEmpList = await _homeServices.GetPresentEmployee(0, 0, 0, DateTime.Now);
                int listCount = PresentEmpList.Count;
                return Json(new { list = PresentEmpList.Take(20), listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GenderList()
        {
            try

            {

                var list = await _hREmployeeServices.GetsEmployeeCountByGender(0, 0);
                int? totalSum = list.Sum(x => x.TotalNumber);
                return Json(new { list = list, total = totalSum == null ? 0 : totalSum }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region TODO 3





        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> AbsentHREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return View(new HomeDashboardViewModelList
                {
                    DBModelAbsentEmp = await _homeServices.GetAbsentEmployee(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType, 0, DateTime.Now, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "AbsentHREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "अनुपस्थित कर्मचारीहरू"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintAbsentHREmployeeAsync()
        {
            var idHRCompany = this.SessionDetail.IdHRCompany;

            var information = await GetCompanyHeaderDetails(idHRCompany);
            string CompanyNameNP = information.CompanyNameNP;
            string ParentCompanyNameNP = information.ParentCompanyNameNP;

            try
            {

                return PartialView(new HomeDashboardViewModelList
                {
                    DBModelAbsentEmpList = await _homeServices.GetAbsentEmployeeList(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType, 0, DateTime.Now),
                    BreadCrumbActionName = "_PrintAbsentHREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/_PrintAbsentHREmployeeAsync",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "अनुपस्थित कर्मचारी सूची छाप्नुहोस्",
                    ModalTitle = "अनुपस्थित कर्मचारी सूची छाप्नुहोस्",
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
                        ReportName = $"{Today.Result.Nepdate}  को निजामती सेवाका कर्मचारीहरुको दैनिक अनुपस्थित"
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListAbsentHREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new HomeDashboardViewModelList
                {
                    DBModelAbsentEmp = await _homeServices.GetAbsentEmployee(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType, 0, DateTime.Now, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ListAbsentHREmployeeAsync",
                    BreadCrumbArea = "",
                    BreadCrumbBaseURL = "Home/Index",
                    BreadCrumbController = "Home",
                    BreadCrumbTitle = "अनुपस्थित कर्मचारीहरू"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        #endregion




        #endregion




        #region SUPERADMIN_WORKING
        [AllowAnonymous]
        public async Task<JsonResult> SuperAdminTotalEmployeeList(long IdHRCompany, int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {
                var idRoleType = this.SessionDetail.IdRoleType;

                var EmployeeList = await _hREmployeeServices.GetPagedList(IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, PageNumber, 10, "HRDesignationOrder", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> EmployeeCount()
        {
            try
            {
                int EmployeeCount = await _hREmployeeServices.GetEmployeeCountNew(0, 0);
                return Json(EmployeeCount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> EmployeePresentCount()
        {
            try
            {
                var todayDate = DateTime.Now.Date;
                var EmployeePresentCount = await _hREmployeeAttendanceHistoryServices.CountAsync(x => x.AttendanceDate == todayDate);
                return Json(EmployeePresentCount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> KaajEmployeeList()
        {
            try
            {

                var KaajEmpList = await _homeServices.GetEmployeeOnKaajList(0, 0, 0, DateTime.Now);
                int listCount = KaajEmpList.Count;
                return Json(new { list = KaajEmpList.Take(20), listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public async Task<JsonResult> AbsentEmployeeList()
        {
            try
            {

                var AbsentEmpList = await _homeServices.GetAbsentEmployeeList(0, 0, 0, DateTime.Now);
                int listCount = AbsentEmpList.Count;
                return Json(new { list = AbsentEmpList.Take(20), listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> SuperAdminleaveEmployeeByOfficeList()
        {
            try
            {
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                var EmployeeList = await _homeServices.GetTodayEmployeeOnLeaveList(0, idRoleType, 0, TodayDate);
                int listCount = EmployeeList.Count;
                return Json(new { listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        #region ADMIN_WORKING

        [AllowAnonymous]

        public async Task<JsonResult> AdminEmployeeCount()
        {
            try
            {
                var company = this.SessionDetail.IdHRCompany;
                var role = this.SessionDetail.IdRoleType;
                int EmployeeCount = await _hREmployeeServices.GetEmployeeCount(company, role);
                return Json(EmployeeCount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> AdminGenderList()
        {
            try

            {
                var company = this.SessionDetail.IdHRCompany;
                var role = this.SessionDetail.IdRoleType;
                var list = await _hREmployeeServices.GetsEmployeeCountByGender(company, role);
                int? totalSum = list.Sum(x => x.TotalNumber);
                return Json(new { list = list, total = totalSum == null ? 0 : totalSum }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> AbsentEmployeeListToday()
        {
            try
            {

                var AbsentEmpList = await _homeServices.GetAbsentEmployeeList(this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHRCompany, this.SessionDetail.IdRoleType, 0, DateTime.Now);
                int listCount = AbsentEmpList.Count;
                return Json(new { listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> KaajEmployeeByOfficeList()
        {
            try
            {
                var IdHrCompany = this.SessionDetail.IdHRCompany;
                var KaajEmpList = await _homeServices.GetEmployeeOnKaajList(IdHrCompany, 0, 0, DateTime.Now);
                int listCount = KaajEmpList.Count;
                return Json(new { list = KaajEmpList.Take(20), listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public async Task<JsonResult> leaveEmployeeByOfficeList()
        {
            try
            {
                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                var EmployeeList = await _homeServices.GetTodayEmployeeOnLeaveList(idHrCompany, idRoleType, 0, TodayDate);
                int listCount = EmployeeList.Count;
                return Json(new { listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        #region TABLE

        [AllowAnonymous]
        public async Task<JsonResult> AdminTotalPresentEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetPresentEmployee(idHrCompany, idRoleType, 0, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> AdminTotalAbsentEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetAbsentEmployee(idHrCompany, idRoleType, 0, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> AdminTotalonLeaveEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetEmployeeOnLeaveList(idHrCompany, idRoleType, 0, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> AdminTotalonKaajEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetEmployeeOnKaajList(idHrCompany, idRoleType, 0, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> AdminTotalLateEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                DateTime TodayDate = DateTime.Now.Date;
                //pagination format
                var EmployeeList = await _homeServices.GetLateAttendanceList(idHrCompany, 0, null, 0, TodayDate, PageNumber, 10, "Id", "ASC", "");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        [AllowAnonymous]
        public async Task<JsonResult> AdminEarlyCheckoutEmpList(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                DateTime TodayDate = DateTime.Now.AddDays(-1).Date;
                //pagination format
                var EmployeeList = await _homeServices.GetEarlyCheckoutEmpList(idHrCompany, 0, null, 0, TodayDate, PageNumber, 10, "Id", "ASC", "");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> AdminOfficeLogsEmpList(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                DateTime TodayDate = DateTime.Now.Date;
                //pagination format
                var EmployeeList = await _homeServices.GetOfficeLogsEmpList(idHrCompany, 0,  0, TodayDate, PageNumber, 10, "Id", "ASC", "");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion









        #endregion



        #region SectionAdmin_WORKING

        [AllowAnonymous]

        public async Task<JsonResult> SectionAdminEmployeeCount()
        {
            try
            {
                var company = this.SessionDetail.IdHRCompany;
                var role = this.SessionDetail.IdRoleType;
                var idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                int EmployeeCount = await _hREmployeeServices.GetSectionEmployeeCount(company, role, idHRCompanyDivision);
                return Json(EmployeeCount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminGenderList()
        {
            try

            {
                var company = this.SessionDetail.IdHRCompany;
                var role = this.SessionDetail.IdRoleType;
                var idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;

                var list = await _hREmployeeServices.GetsSectionEmployeeCountByGender(company, role, idHRCompanyDivision);
                int? totalSum = list.Sum(x => x.TotalNumber);
                return Json(new { list = list, total = totalSum == null ? 0 : totalSum }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> AbsentSectionEmployeeListToday()
        {
            try
            {
                var AbsentEmpList = await _homeServices.GetSectionAbsentEmployeeList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, DateTime.Now);
                int listCount = AbsentEmpList.Count;
                return Json(new { list = AbsentEmpList.Take(20), listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminKaajEmployeeByOfficeList()
        {
            try
            {
                var IdHrCompany = this.SessionDetail.IdHRCompany;
                var IdRoleType = this.SessionDetail.IdRoleType;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                var KaajEmpList = await _homeServices.GetEmployeeOnKaajList(IdHrCompany, IdRoleType, IdHRCompanyDivision, DateTime.Now);
                int listCount = KaajEmpList.Count;
                return Json(new { list = KaajEmpList.Take(20), listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminleaveEmployeeByOfficeList()
        {
            try
            {
                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                var idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                DateTime TodayDate = DateTime.Now;
                var EmployeeList = await _homeServices.GetTodayEmployeeOnLeaveList(idHrCompany, idRoleType, idHRCompanyDivision, TodayDate);
                int listCount = EmployeeList.Count;
                return Json(new { listCount = listCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


  

        #region TABLE

        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminTotalPresentEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;

                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetPresentEmployee(idHrCompany, idRoleType, IdHRCompanyDivision, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminTotalAbsentEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetAbsentEmployee(idHrCompany, idRoleType, IdHRCompanyDivision, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminTotalonLeaveEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetEmployeeOnLeaveList(idHrCompany, idRoleType, IdHRCompanyDivision, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminTotalonKaajEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                var idRoleType = this.SessionDetail.IdRoleType;
                DateTime TodayDate = DateTime.Now;
                //pagination format
                var EmployeeList = await _homeServices.GetEmployeeOnKaajList(idHrCompany, idRoleType, IdHRCompanyDivision, TodayDate, PageNumber, 10, "HRDesignationRank", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminTotalLateEmployee(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;

                DateTime TodayDate = DateTime.Now.Date;
                //pagination format
                var EmployeeList = await _homeServices.GetLateAttendanceList(idHrCompany, IdHRCompanyDivision, null, 0, TodayDate, PageNumber, 10, "Id", "ASC", "");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminEarlyCheckoutEmpList(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var IdHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                DateTime TodayDate = DateTime.Now.AddDays(-1).Date;
                //pagination format
                var EmployeeList = await _homeServices.GetEarlyCheckoutEmpList(idHrCompany, IdHRCompanyDivision, null, 0, TodayDate, PageNumber, 10, "Id", "ASC", "");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> SectionAdminOfficeLogsEmpList(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {

                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision;

                DateTime TodayDate = DateTime.Now.Date;
                //pagination format
                var EmployeeList = await _homeServices.GetOfficeLogsEmpList(idHrCompany, idHRCompanyDivision, 0, TodayDate, PageNumber, 10, "Id", "ASC", "");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion



        #endregion














        #region DO_CHECK







        [AllowAnonymous]
        public async Task<JsonResult> AdminDeviceInformation()
        {
            try
            {
                var company = this.SessionDetail.IdHRCompany;
                int ActiveDevice = await _hRDeviceServices.CountAsync(x => (x.DeviceNumber > 0) && x.IdHRCompany == company && x.IsActive == true);
                int DeactiveDevice = await _hRDeviceServices.CountAsync(x => (x.DeviceNumber > 0) && x.IdHRCompany == company && x.IsActive == false);

                return Json(new { ActiveDevice = ActiveDevice, DeactiveDevice = DeactiveDevice }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> AdminAssignUnAssign()
        {
            try
            {
                var company = this.SessionDetail.IdHRCompany;
                var DBModelShift = await _homeServices.GetShiftRoaterAsync(company, 0, DateTime.Now.Date, "");
                var assign = DBModelShift.Where(x => x.LoginTime != null && x.LogoutTime != null).ToList();
                int assignCount = assign.Count;
                var unAssigned = DBModelShift.Where(x => x.LoginTime == null && x.LogoutTime == null).ToList();
                int unassignCount = unAssigned.Count;

                return Json(new { assign = assign.Take(20), unAssigned = unAssigned.Take(20), assignCount = assignCount, unassignCount = unassignCount }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion







        #region NO_WORK

        #region NONEED
        #region MENU

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetMenuInformation()
        {
            List<proc_GetSystemLeftMenu_Result> leftMenu = new List<proc_GetSystemLeftMenu_Result>();

            List<NavMenuListModel> navMenuViewListModel = new List<NavMenuListModel>();
            string area = "";
            string controller = "Dashboard";
            string action = "Index";
            try
            {
                using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
                {
                    var HeaderList = db.proc_GetNavbarMenu(this.SessionDetail.IdHREmployeeRole, this.SessionDetail.IdHREmployee, this.SessionDetail.Language).ToList();
                    if (HeaderList.Count > 0)
                    {
                        foreach (var head in HeaderList)
                        {
                            NavMenuListModel navMenuListModel = new NavMenuListModel();
                            #region  MainList
                            navMenuListModel.name = head.Menu;
                            navMenuListModel.icon = head.CSSIcon;
                            #endregion

                            area = head.Area;
                            leftMenu = db.proc_GetSystemLeftMenu(this.SessionDetail.IdHREmployeeRole, this.SessionDetail.IdHREmployee, head.Area, controller, action, this.SessionDetail.Language).ToList();
                            List<NaveMenuModel> NaveMenuListModel = new List<NaveMenuModel>();



                            foreach (var item in leftMenu.OrderBy(x => x.IdParent_SystemStructure))
                            {
                                NaveMenuModel naveMenuModel = new NaveMenuModel();

                                if (item.IdParent_SystemStructure == 0 && leftMenu.Where(x => x.IdParent_SystemStructure == item.Id).Count() == 0)
                                {
                                    if (item.Header == null)
                                    {
                                        continue;
                                    }
                                    naveMenuModel.name = item.Header;
                                    naveMenuModel.hasChildren = false;
                                    naveMenuModel.url = item.DefaultURL;
                                    naveMenuModel.children = null;
                                    NaveMenuListModel.Add(naveMenuModel);

                                }
                                else if (leftMenu.Where(x => x.IdParent_SystemStructure == item.Id).Count() > 0)
                                {
                                    if (item.Header == null)
                                    {
                                        continue;
                                    }
                                    naveMenuModel.name = item.Header;
                                    naveMenuModel.hasChildren = true;
                                    List<MenuChildModel> menuChildListModel = new List<MenuChildModel>();
                                    foreach (var secondLevel in leftMenu.Where(x => x.IdParent_SystemStructure == item.Id).OrderBy(x => x.ChildOrder))
                                    {
                                        MenuChildModel menuChildModel = new MenuChildModel();
                                        menuChildModel.name = secondLevel.Header;
                                        menuChildModel.url = secondLevel.DefaultURL;

                                        menuChildListModel.Add(menuChildModel);
                                    }
                                    naveMenuModel.children = menuChildListModel;

                                    NaveMenuListModel.Add(naveMenuModel);

                                }
                            }

                            navMenuListModel.menu = NaveMenuListModel;


                            navMenuViewListModel.Add(navMenuListModel);
                        }



                    }
                }
            }
            catch
            {
                return Json(navMenuViewListModel, JsonRequestBehavior.AllowGet);
            }
            return Json(navMenuViewListModel, JsonRequestBehavior.AllowGet);

        }


        #endregion
        #region COMMON
        [AllowAnonymous]
        public async Task<JsonResult> GetMonthlyDetailsByID(int? year, int? month)
        {
            try
            {
                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idHrCompanyDivision = this.SessionDetail.IdHRCompanyDivision;
                var idJobStatus = this.SessionDetail.IdHREmployeeJobStatus;
                var idHrEmployee = this.SessionDetail.IdHREmployee;
                if (year == null)
                {
                    year = this.Date(DateTime.Now).NepYear;
                }
                if (month == null)
                {
                    month = this.Date(DateTime.Now).NepMonth;
                }
                var startEndDate = GetStartEndDate(year.Value, month.Value);
                var MonthlyDetails = await _homeServices.GetMonthlyDetailsByID(idHrCompany, idHrCompanyDivision, idJobStatus, idHrEmployee, startEndDate.Key, startEndDate.Value, "");
                if (MonthlyDetails == null)
                {
                    return Json(new { data = MonthlyDetails }, JsonRequestBehavior.AllowGet);

                }
                MonthlyDetails.AbsentDays = (int)(MonthlyDetails.TotalWorkingDays - MonthlyDetails.TotalPresent);
                return Json(new { data = MonthlyDetails }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public async Task<JsonResult> EmployeeOwnLeaveStatusByOfficeList()
        {
            try
            {
                var IdHREmployee = this.SessionDetail.IdHREmployee;
                int Year = Date(DateTime.Now).NepYear;
                var EmpleaveStatus = await _homeServices.GetEmployeeLeaveStatusList(IdHREmployee, Year);
                var onlyEmpleaveStatus = EmpleaveStatus.Take(3).ToList();
                return Json(new { wholeList = EmpleaveStatus.Take(10).ToList(), list = onlyEmpleaveStatus }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public async Task<JsonResult> GetEmployeeCurrentShift()
        {
            try
            {
                var IdHRCompany = this.SessionDetail.IdHRCompany;
                var IdHREmployee = this.SessionDetail.IdHREmployee;
                var CurrentShift = await _homeServices.GetShiftForEmployee(IdHRCompany, IdHREmployee);
                return Json(new { list = CurrentShift }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetUpcomingHoliday()
        {
            try
            {
                var IdHRCompany = this.SessionDetail.IdHRCompany;
                var holidayList = await _homeServices.GetUpcomingHoliday(IdHRCompany);
                return Json(new { list = holidayList }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        [HttpGet]   
        public async Task<JsonResult> GetMonthNp()
        {
            try
            {
                var DDLMonthNp = await GetIntStringPairModel(PairModelTitle.Month);

                return Json(new { monthNp = DDLMonthNp }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        //FOR BOTH IDDIVISION TAKEN FROM PROCEDURE USING IDHREMPLOYEE
        [AllowAnonymous]
        public async Task<JsonResult> AdminTotalEmployeeList(int PageNumber)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            try
            {
                var idHrCompany = this.SessionDetail.IdHRCompany;
                var idRoleType = this.SessionDetail.IdRoleType;

                var EmployeeList = await _hREmployeeServices.GetPagedList(idHrCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, PageNumber, 10, "HRDesignationOrder", "ASC", " ");
                PaginationHomeModel pagination = new PaginationHomeModel();
                pagination.Count = EmployeeList.Count;
                pagination.FirstItemOnPage = EmployeeList.FirstItemOnPage;
                pagination.HasNextPage = EmployeeList.HasNextPage;
                pagination.HasPreviousPage = EmployeeList.HasPreviousPage;
                pagination.IsFirstPage = EmployeeList.IsFirstPage;
                pagination.IsLastPage = EmployeeList.IsLastPage;
                pagination.LastItemOnPage = EmployeeList.LastItemOnPage;
                pagination.PageCount = EmployeeList.PageCount;
                pagination.PageNumber = EmployeeList.PageNumber;
                pagination.PageSize = EmployeeList.PageSize;



                return Json(new { list = EmployeeList, pagination = pagination }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }



        [AllowAnonymous]
        public async Task<JsonResult> GetEmployeeOwnKaajInfo()
        {

            try
            {
                var YearNp = Date(DateTime.Now).NepYear;
                string Information = "तपाईले हाल सम्म कुनैपनि बिदा लिनु भएको छैन|";
                string KaajStatus = "";
                string totalDay = "";
                var IdHREmployee = this.SessionDetail.IdHREmployee;
                var kaajHistory = await _homeServices.GetEmployeeKaajHistory(IdHREmployee);
                if (kaajHistory == null)
                {
                    return Json(new { info = Information, status = KaajStatus }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Kaaj = kaajHistory.OrderByDescending(x => x.Id).FirstOrDefault();
                    Information = ConvertToUnicodeNepaliNumber(Kaaj.KaajFromNP) + " देखि " + ConvertToUnicodeNepaliNumber(Kaaj.KaajToNP);
                    KaajStatus = ConvertToUnicodeNepaliNumber(Kaaj.LeaveStatusTypeNP);
                    totalDay = Kaaj.TotalDay.ToString();
                    kaajHistory = kaajHistory.Where(x => x.KaajYearNP == YearNp).ToList();
                }

                return Json(new { history = kaajHistory, info = Information, status = KaajStatus, totalday = totalDay }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { info = "", status = "", totalday = "" }, JsonRequestBehavior.AllowGet);
            }
        }






        #endregion


        #region LEAVE_KAAJ

        public async Task<JsonResult> MyPendingKaajListInformation()
        {
            var IdHrCompany = this.SessionDetail.IdHRCompany;
            var IdHREmployee = this.SessionDetail.IdHREmployee;
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            var RequestedkaajList = await db.HREmployeeKaajHistories.Where(x => x.IdHrCompany == IdHrCompany && x.IdHREmployee == IdHREmployee && (x.IdKaajStatus == 1 || x.IdKaajStatus == 2)).Select(x => new
            {
                Id = x.Id,
                IdHrCompany = x.IdHrCompany,
                ApprovedBy = x.ApprovedBy,
                ApprovedOn = x.ApprovedOn,
                CheckInTime = x.CheckInTime,
                CheckOutTime = x.CheckOutTime,
                FiscalYear = x.FiscalYear,
                IdApprovedBy = x.IdApprovedBy,
                IdCountry = x.IdCountry,
                IdHREmployee = x.IdHREmployee,
                IdJob = x.IdJob,
                IdKaajStatus = x.IdKaajStatus,
                IdKaajType = x.IdKaajType,
                IdRecommendedBy = x.IdRecommendedBy,
                IsNational = x.IsNational,
                KaajExpenses = x.KaajExpenses,
                KaajFromDate = x.KaajFromDate,
                KaajFromNP = x.KaajFromNP,
                KaajLocation = x.KaajLocation,
                KaajPurpose = x.KaajPurpose,
                KaajRegistrationNumber = x.KaajRegistrationNumber,
                KaajShortName = x.KaajShortName,
                KaajTakenNumber = x.KaajTakenNumber,
                KaajTitle = x.KaajTitle,
                KaajToDate = x.KaajToDate,
                KaajToNP = x.KaajToNP,
                KaajYearNP = x.KaajYearNP,
                RecommendationBy = x.KaajToNP,
                RecommendedOn = x.RecommendedOn,
                RemarkApprovedBy = x.RemarkApprovedBy,
                RemarkRecommended = x.RemarkRecommended,
                VehicleUsedInKaaj = x.VehicleUsedInKaaj
            }).ToListAsync();
            if (RequestedkaajList.Count == 0)
            {
                return Json(new {  isData = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { list = RequestedkaajList, showing = RequestedkaajList.Take(3), isData = true }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> MyPendingLeaveListInformation()
        {
            var IdHrCompany = this.SessionDetail.IdHRCompany;
            var IdHREmployee = this.SessionDetail.IdHREmployee;
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            var RequestedLeaveList = await db.HREmployeeLeaveHistory.Where(x => x.IdHRCompany == IdHrCompany && x.IdHREmployee == IdHREmployee && (x.IdLeaveStatus == 1 || x.IdLeaveStatus == 2)).Select(x => new
            {
                Id = x.Id,
                IdHRCompany = x.IdHRCompany,
                IdLeaveStatus = x.IdLeaveStatus,
                ApprovalRemark = x.ApprovalRemark,
                ApprovedOn = x.ApprovedOn,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                Document = x.Document,
                Document1 = x.Document1,
                Document2 = x.Document2,
                IdApprovedBy = x.IdApprovedBy,
                IdHREmployee = x.IdHREmployee,
                IdJob = x.IdJob,
                IdLeaveCancelStatus = x.IdLeaveCancelStatus,
                IdLeaveType = x.IdLeaveType,
                IdMasterLeaveTitle = x.IdMasterLeaveTitle,
                IdRecommandationBy = x.IdRecommandationBy,
                IsActive = x.IsActive,
                IsHalfDayCount = x.IsHalfDayCount,
                LeaveCancelFrom = x.LeaveCancelFrom,
                LeaveCancelFromNP = x.LeaveCancelFromNP,
                LeaveReason = x.LeaveReason,
                LeaveRegistrationNumber = x.LeaveRegistrationNumber,
                LeaveValidFrom = x.LeaveValidFrom,
                LeaveValidFromNP = x.LeaveValidFromNP,
                LeaveValidTo = x.LeaveValidTo,
                LeaveValidToNP = x.LeaveValidToNP,
                LeaveYear = x.LeaveYear,
                LeaveYearNP = x.LeaveYearNP,
                RecommandationDate = x.RecommandationDate,
                UpdatedBy = x.UpdatedBy,
                RecommandationRemark = x.RecommandationRemark,
                TerminalIP = x.TerminalIP,
                UpdatedOn = x.UpdatedOn

            }).ToListAsync();
            if (RequestedLeaveList.Count == 0)
            {
                return Json(new {  isData = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { list = RequestedLeaveList, showing = RequestedLeaveList, isData = true }, JsonRequestBehavior.AllowGet);
        }



        #endregion


        #region Notification

        [AllowAnonymous]
        public async Task<JsonResult> RequestToLeaveRecomend()
        {

            var idHrCompany = this.SessionDetail.IdHRCompany;
            var idHREmployee = this.SessionDetail.IdHREmployee;

            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            var RequestToLeaveRecomendList = await db.HREmployeeLeaveHistory.Where(x =>x.IdRecommandationBy==idHREmployee && x.IdHRCompany == idHrCompany && x.IdLeaveStatus == 1).Select(x => new
            {
                LeaveTitleNP = x.MasterLeaveTitle.LeaveTitleNP,
                PISNumber = x.HREmployee1.PISNumber,
                RequestedEmployeeName=x.HREmployee1.HREmployeeNameNP,
                Remarks= "श्री " +x.HREmployee1.HREmployeeNameNP +" बाट " + x.MasterLeaveTitle.LeaveTitleNP+ " सिफारिसको लागि अनुरोध भएको छ ।",
                LinkURl= "EmployeeManagement/HREmployeeLeaveRecommend?id="+x.Id,
                LeaveValidFromNP = x.LeaveValidFromNP,
                LeaveValidToNP = x.LeaveValidToNP,
                PopUrl = "/EmployeeManagement/HREmployeeLeaveRecommend/_UpdateHREmployeeLeaveRecommendAsync?id=" + x.Id+"&pageNumber=1&pageSize=10&orderingBy=id&orderingDirection=asc&searchKey=''",
            }).ToListAsync();
            var RequestToLeaveApproveList = await db.HREmployeeLeaveHistory.Where(x => x.IdApprovedBy == idHREmployee && x.IdHRCompany == idHrCompany && x.IdLeaveStatus == 2).Select(x => new
            {
                LeaveTitleNP = x.MasterLeaveTitle.LeaveTitleNP,
                PISNumber = x.HREmployee1.PISNumber,
                RequestedEmployeeName = x.HREmployee1.HREmployeeNameNP,
                Remarks = "श्री " + x.HREmployee1.HREmployeeNameNP + " बाट " + x.MasterLeaveTitle.LeaveTitleNP + " स्वीकृतिको लागि अनुरोध भएको छ ।",
                LinkURl = "EmployeeManagement/HREmployeeLeaveApproved?id=" + x.Id,
                LeaveValidFromNP = x.LeaveValidFromNP,
                LeaveValidToNP = x.LeaveValidToNP,
                PopUrl = "/EmployeeManagement/HREmployeeLeaveApproved/_UpdateHREmployeeLeaveApprovedAsync?id=" + x.Id + "&pageNumber=1&pageSize=10&orderingBy=id&orderingDirection=asc&searchKey=''",

            }).ToListAsync();
            var RequestToKaajApproveList = await db.HREmployeeKaajHistories.Where(x => x.IdApprovedBy == idHREmployee && x.IdHrCompany == idHrCompany && x.IdKaajStatus == 2).Select(x => new
            {
                LeaveTitleNP = x.KaajType.TitleNP,
                PISNumber = x.HREmployee1.PISNumber,
                RequestedEmployeeName = x.HREmployee1.HREmployeeNameNP,
                Remarks = "श्री " + x.HREmployee1.HREmployeeNameNP + " बाट " + x.KaajType.TitleNP + " स्वीकृतिको लागि अनुरोध भएको छ ।",
                LinkURl = "EmployeeManagement/HREmployeeKaajApproved?id=" + x.Id,
                LeaveValidFromNP = x.KaajFromNP,
                LeaveValidToNP = x.KaajToNP,
                PopUrl = "/EmployeeManagement/HREmployeeKaajApproved/_UpdateHrEmployeeKaajApprovedAsync?id=" + x.Id + "&pageNumber=1&pageSize=10&orderingBy=id&orderingDirection=asc&searchKey=''",


            }).ToListAsync();

            return Json(new { RequestToLeaveRecomendList, RequestToLeaveApproveList, RequestToKaajApproveList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Calling



      

        public async Task<ICollection<HREmployeeLeaveHistory>> LeaveDataList(long? company)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            var LeaveList = await db.HREmployeeLeaveHistory.Where(x => x.IdHRCompany == company).ToListAsync();
            return LeaveList;
        }
        public async Task<ICollection<HREmployeeLeaveHistory>> RequestedLeaveDataList(long? company, long? emp)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            var RequestedLeaveList = await db.HREmployeeLeaveHistory.Where(x => x.IdHRCompany == company && x.IdHREmployee == emp && (x.IdLeaveStatus == 1 || x.IdLeaveStatus == 2)).ToListAsync();
            return RequestedLeaveList;
        }
      
        #endregion



        #region CONVETION
        public string ConvertToUnicodeNepaliNumber(string number)
        {
            Dictionary<char, char> engToNepali = new Dictionary<char, char>()
        {
            {'0', '०'}, {'1', '१'}, {'2', '२'}, {'3', '३'}, {'4', '४'},
            {'5', '५'}, {'6', '६'}, {'7', '७'}, {'8', '८'}, {'9', '९'}
            };

            char[] chars = number.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (engToNepali.ContainsKey(chars[i]))
                {
                    chars[i] = engToNepali[chars[i]];
                }
            }
            return new string(chars);
        }
        #endregion
        #endregion



    }





}

