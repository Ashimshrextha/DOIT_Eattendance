using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Areas.DeviceManagement.Controllers
{
    public class DeviceManagerController : Controller
    {
        // GET: DeviceManagement/DeviceManager
        public ActionResult Index()
        {
            return View();
        }
    }
}