using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SystemServices.Reports;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace AttendanceManagementSystem.App_Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthenticationFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserLogsServices _userLogsServices;

        public AuthenticationFilter()
        {
            this._unitOfWork = new UnitOfWork();
            this._userLogsServices = new UserLogsServices(this._unitOfWork);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            //UserLogsModel model = new UserLogsModel
            //{
            //    IdLogin=Guid.NewGuid(),
            //    UserName=ctx.User.Identity.Name,
            //    FullName="",
            //   SessionId= filterContext.HttpContext.Session.SessionID,
            //   LoginTime=DateTime.Now,
            //   LogoutTime=DateTime.Now,
            //   Area=ctx.Request.PathInfo,
            //   Controller="",
            //   ActionName="",
            //   DeviceName=ctx.Request.UserAgent,
            //   DeviceOS=ctx.Request.UserAgent,
            //   BrowserName="",
            //   BrowserVersion="",
            //   LocationName="",
            //   AccessURL=ctx.Request.RawUrl,
            //   IllegalAccessMessage="",
            //   EventMessage="",
            //   ErrorMessage="",
            //   TerminalIP="",
            //   CreatedOn=DateTime.Now
            //};
            //_userLogsServices.CommitSaveChangesAsync(model,SystemStores.ENUMData.EnumGlobal.CRUDType.CREATE);
            // If the browser session or authentication session has expired...
            if (ctx.Session["UserSession"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult { Data = "_Login_", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                    // simply displays a temporary 5 second notification that they have timed out, and
                    // will, in turn, redirect to the logon page.
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                            {"Area",""},
                        { "Controller", "Account" },
                        { "Action", "Login" }
                });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class LocsAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // If the browser session has expired...
            if (ctx.Session["UserSession"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult { Data = "_Login_" };
                }
                else
                {
                    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                    // simply displays a temporary 5 second notification that they have timed out, and
                    // will, in turn, redirect to the logon page.
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                            {"Area",""},
                        { "Controller", "Account" },
                        { "Action", "Login" }
                });
                }
            }
            else if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                // Otherwise the reason we got here was because the user didn't have access rights to the
                // operation, and a 403 should be returned.
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Area", ""},
                        { "Controller","Account" },
                        { "Action", "AccessDenied"}
                });
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        public string ActionName { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized && httpContext.Session["UserSession"] == null)
            {
                return false;
            }
            try
            {
                string area = (string)httpContext.Request.RequestContext.RouteData.DataTokens["area"];
                string controller = (string)httpContext.Request.RequestContext.RouteData.Values["controller"];
                string action = (string)httpContext.Request.RequestContext.RouteData.Values["action"];
                area = area ?? "";
                ActionName = ActionName ?? action;
                return SystemServices.SystemAuthentication.AccountRepo.CheckPermission(area, controller, ActionName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // If the browser session has expired...
            if (ctx.Session["UserSession"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult { Data = "_Login_" };
                }
                else
                {
                    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                    // simply displays a temporary 5 second notification that they have timed out, and
                    // will, in turn, redirect to the logon page.
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Area", "" },
                        { "Controller", "Account" },
                        { "Action", "Login" }
                });
                    ctx.Session["Permission"] = "Denied";
                }
            }
            else if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                // Otherwise the reason we got here was because the user didn't have access rights to the
                // operation, and a 403 should be returned.

                //filterContext.RequestContext.RouteData

                //2020-06-22

                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Area", ""},
                        { "Controller","Account" },
                        { "Action", "AccessDenied"}
                });


            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}