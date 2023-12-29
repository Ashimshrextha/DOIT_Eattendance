using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class DashboardController : BaseController
    {
        
        // GET: CompanyManagement/Dashboard
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
					SessionDetails = SessionDetail,
					BreadCrumbActionName = "Index",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbBaseURL = "CompanyManagement/Dashboard",
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

		[AuthorizeUser(ActionName = "Index")]
		[AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListDashboardAsync()
        {
            try
            {
                return PartialView(new DashboardViewModelList
                {
					SessionDetails = SessionDetail,
					///TreeCompany = CompanyTree(SessionDetail.IdHRCompany.Value),
					BreadCrumbActionName = "_ListDashboardAsync",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbBaseURL = "CompanyManagement/Dashboard",
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