using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.SystemSecurity;
using SystemViewModels.Reports;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly HRCalendarServices _HRCalendarServices;
        public DashboardController()
        {
            _HRCalendarServices = new HRCalendarServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                var date = DateTime.Now.Date;
                HRCalendarModel today = await _HRCalendarServices.GetModelFindAsync(x => x.EngDate == date);
                await Task.WhenAll();
                return View(new DashboardViewModelList
                {
                    BreadCrumbActionName = "Index",
                    BreadCrumbArea = "Reports",
                    BreadCrumbBaseURL = "Reports/Dashboard",
                    BreadCrumbController = "Dashboard",
                    BreadCrumbTitle = "ड्याशबोर्ड",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance Management System"
                   
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListDashboardAsync()
        {
            try
            {
                await Task.WhenAll();
                return PartialView(new DashboardViewModelList
                {
                    BreadCrumbActionName = "_ListDashboardAsync",
                    BreadCrumbArea = "Reports",
                    BreadCrumbBaseURL = "Reports/Dashboard",
                    BreadCrumbController = "Dashboard",
                    BreadCrumbTitle = "Dashboard",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance Management System"
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }
    }
}