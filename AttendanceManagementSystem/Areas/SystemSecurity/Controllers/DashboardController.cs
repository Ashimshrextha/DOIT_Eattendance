using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: SystemSecurity/Dashboard
        public DashboardController()
        {

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                await Task.WhenAll();
                return View(new DashboardViewModelList
                {
                    BreadCrumbActionName = "Index",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbBaseURL = "SystemSecurity/Dashboard",
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
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbBaseURL = "SystemSecurity/Dashboard",
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