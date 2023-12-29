using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Areas.SystemFeedback.Controllers
{
    public class SystemManualController : BaseController
    {
        // GET: SystemFeedback/SystemManual      
            public ActionResult Index()
            {
                return View();
            }
        
        }
    }
