using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemViewModels.DeviceManagement;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.DeviceManagement.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: DeviceManagement/Dashboard
        public DashboardController()
        {

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                await Task.WhenAll();
                return View(new DashbaordViewModelList
                {
                    BreadCrumbActionName="Index",
                    BreadCrumbArea= "DeviceManagement",
                    BreadCrumbBaseURL= "DeviceManagement/Dashboard",
                    BreadCrumbController= "Dashboard",
                    BreadCrumbTitle= "ड्याशबोर्ड",
                    CRUDAction=CRUDType.READ,
                    HeaderTitle="Attendance Management System"
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
                return PartialView(new DashbaordViewModelList
                {
                    BreadCrumbActionName = "_ListDashboardAsync",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbBaseURL = "DeviceManagement/Dashboard",
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