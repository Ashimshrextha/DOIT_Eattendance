using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
    public class EmployeePermissionController : BaseController
    {
        // GET: SystemSetting/EmployeePermission
        public ActionResult Index()
        {
            return View();
        }
    }
}