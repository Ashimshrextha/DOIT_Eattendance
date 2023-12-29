using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SystemDatabase;
using SystemStores.GlobalModels;

namespace AttendanceManagementSystem.Services
{
    public class CommonServices
    {
        public static ICollection<int> PageNumber { get { return new List<int>() { 20, 25, 50, 75, 90, 100, 150, 200, 250, 300, 350 }; } }

        public static ICollection<proc_GetNavbarMenu_Result> Get_SystemMenu()
        {
            try
            {
                if (UserSession() != null)
                {
                    using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
                    {
                        return db.proc_GetNavbarMenu(UserSession().IdHREmployeeRole, UserSession().IdHREmployee, UserSession().Language).ToList();
                    }
                }
                else
                    RedirectToLogin();
                return null;
            }
            catch (HttpException exp)
            {
                throw new ArgumentNullException(exp.Message);
            }
        }

        public static ICollection<proc_GetSystemLeftMenu_Result> Get_SystemLeftMenu()
        {
            string area = (string)HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];
            string controller = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];
            string action = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["action"];
            area = area ?? "";
            try
            {
                using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
                {
                    var result = db.proc_GetSystemLeftMenu(UserSession().IdHREmployeeRole, UserSession().IdHREmployee, area, controller, action, UserSession().Language).ToList();
                    return result;

                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<HREmployeeLeaveHistory> GetLeaveNotification()
        {
            try
            {
                var id = UserSession().IdHREmployee;
                EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
                var result = db.HREmployeeLeaveHistory.Where(x => x.IdRecommandationBy == id && x.IdLeaveStatus ==1).ToList();
                return result;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<HREmployeeLeaveHistory> GetLeaveapprovalNotification()
        {
            try
            {
                var id = UserSession().IdHREmployee;
                EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
                var result = db.HREmployeeLeaveHistory.Where(x => x.IdApprovedBy == id && x.IdLeaveStatus ==2 ).ToList();
                return result;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<HREmployeeKaajHistory> GetKaajNotification()
        {
            try
            {
                var id = UserSession().IdHREmployee;
                EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
                var result = db.HREmployeeKaajHistories.Where(x => x.IdApprovedBy == id && x.IdKaajStatus <= 2 || x.IdRecommendedBy == id && x.IdKaajStatus <= 2).ToList();
                return result;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<HREmployeeAttendanceHistory> GetLateComeNotification()
        {
            try
            {
                var id = UserSession().IdHREmployee;
                EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
                var result = db.HREmployeeAttendanceHistories.Where(x => x.IdApprovedBy == id && x.IsLateReasonApproved == false).ToList();
                return result;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<HREmployeeKaajHistory> GetIndvidualKaajNoti()
        {
            try
            {
                var date = DateTime.Now.AddDays(-7);
                var id = UserSession().IdHREmployee;
                EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
                {
                    var result = db.HREmployeeKaajHistories.Where(x => x.IdHREmployee == id && x.IdKaajStatus > 1 && x.CreatedOn > date).ToList();
                    return result;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<HREmployeeLeaveHistory> GetIndvidualRequestNoti()
        {
            try
            {
                var date = DateTime.Now.AddDays(-7);
                var id = UserSession().IdHREmployee;
                EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
                {
                    var result = db.HREmployeeLeaveHistory.Where(x => x.IdHREmployee == id && x.IdLeaveStatus > 1 && x.CreatedOn > date).ToList();
                    return result;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static ICollection<proc_GetSystemTabbed_Result> Get_SystemTabbed(int idSystemStructure)
        {
            string area = (string)HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];
            string controller = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];
            string action = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["action"];
            area = area ?? "";
            try
            {
                using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
                {
                    return db.proc_GetSystemTabbed(UserSession().IdHREmployeeRole, UserSession().IdHREmployee, area, controller, action, idSystemStructure, UserSession().Language).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public static UserSessionModel UserSession() => (UserSessionModel)HttpContext.Current.Session["UserSession"];

        private static ActionResult RedirectToLogin()
        {
            try
            {
                return new RedirectToRouteResult(new RouteValueDictionary
                {
                      {"Area",""},
                        { "Controller", "Account" },
                        { "Action", "Login" }
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentNullException(exp.Message);
            }
        }

        public static string GetImagePath(string FolderName, string ImageName)
        {
            string url = string.Empty;
            string serverPath = string.Empty;
            try
            {
                var FileUrlPath = System.Configuration.ConfigurationManager.AppSettings["FileUrlPath"];
                url = ImageName == null ? "/images/avatars/avatar6.png" : $"{FileUrlPath}Image/{FolderName}/{ImageName}";
                serverPath = HttpContext.Current.Server.MapPath("~") + url;
                if (!System.IO.File.Exists(serverPath))
                    url = "/images/avatars/avatar6.png";
                return url;
            }
            catch
            {
                return "/images/avatars/avatar6.png";
            }
        }
        public static string GetCompanyName(long idHRCompany)
        {          
            using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
                {

                var result = db.HRCompanies.Where(x => x.IdCompanyCode == idHRCompany).ToList();

                foreach (var item in result)
                {
                    if (result != null)
                    {
                        string npname = item.CompanyNameNP;
                    }
                    else
                        return string.Empty;
                }
            }

            return string.Empty;

        }
        public static bool TodayPresent(long? idhremployee)
        {
            using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
            {
                var today = DateTime.Now.Date;
                var result = db.HREmployeeAttendanceHistories.Any(x => x.IdHREmployee == idhremployee && x.AttendanceDate == today);
                return result;
            }

        }
    }
}