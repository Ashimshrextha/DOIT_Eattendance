using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using SystemDatabase;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
	public class HREmployeeServiceTypeGroupController : BaseController
    {
		[NonAction]
		public Expression<Func<HREmployeeServiceTypeGroup, bool>> Condition(long? idHRCompany, string searchKey = "")
		{
			Expression<Func<HREmployeeServiceTypeGroup, bool>> retutndata = (x => false);

			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => (x.GroupName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2) retutndata = (x => false);

			else retutndata = (x => false);

			return retutndata;
		}
		[AuthorizeUser]
		public ActionResult Index()
        {
            return View();
        }
    }
}