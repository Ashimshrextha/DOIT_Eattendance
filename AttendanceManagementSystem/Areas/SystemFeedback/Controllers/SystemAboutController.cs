using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Areas.SystemFeedback.Controllers
{
    public class SystemAboutController : BaseController
    {
        // GET: SystemFeedback/SystemAbout
        public ActionResult Index()
        {
            return View();
        }
    }
}