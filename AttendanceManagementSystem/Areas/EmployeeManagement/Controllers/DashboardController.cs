using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSecurity;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly EmployeeDashboardServices _employeeDashboardServices;
        private readonly HRCalendarServices _HRCalendarServices;

        public DashboardController()
        {
            this._employeeDashboardServices = new EmployeeDashboardServices(this._unitOfWork);
            this._HRCalendarServices = new HRCalendarServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                var todayDate = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == todayDate);
                var startEndDate = GetStartEndDate(today.NepYear, today.NepMonth);
                return View(new DashboardViewModelList
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "Index",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/Dashboard",
                    BreadCrumbController = "Dashboard",
                    BreadCrumbTitle = "ड्याशबोर्ड",
                    CRUDAction = CRUDType.READ,
                    DBModelCalendar = await _employeeDashboardServices.GetCalendar(this.SessionDetail.IdHREmployee, startEndDate.Key, startEndDate.Value),
                   TodayDayNp = this.Date(DateTime.Now).NepDay.ToString(),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListDashboardAsync(long? idHREmployee)
        {
            try
            {
                var todayDate = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == todayDate);
                var startEndDate = GetStartEndDate(today.NepYear, today.NepMonth);
                return PartialView(new DashboardViewModelList
                {
                    DBModelCalendar = await _employeeDashboardServices.GetCalendar(idHREmployee, startEndDate.Key, startEndDate.Value),
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_ListDashboardAsync",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/Dashboard",
                    BreadCrumbController = "Dashboard",
                    BreadCrumbTitle = "Dashboard",
                    CRUDAction = CRUDType.READ,
                    TodayDayNp= this.Date(DateTime.Now).NepDay.ToString(),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
}