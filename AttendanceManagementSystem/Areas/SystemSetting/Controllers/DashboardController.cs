using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: SystemSetting/Dashboard
        public DashboardController()
        {

        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(new DashboardViewModelList
                {
                    //TreeCompany=CompanyTree(SessionDetail.IdHRCompany.Value),
                    BreadCrumbActionName = "Index",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbBaseURL = "SystemSetting/Dashboard",
                    BreadCrumbController = "Dashboard",
                    BreadCrumbTitle = "ड्याशबोर्ड",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance Management System"
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListDashboardAsync()
        {
            try
            {
                return PartialView(new DashboardViewModelList
                {
                    BreadCrumbActionName = "_ListDashboardAsync",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbBaseURL = "SystemSetting/Dashboard",
                    BreadCrumbController = "Dashboard",
                    BreadCrumbTitle = "ड्याशबोर्ड",
                    CRUDAction = CRUDType.READ
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
}