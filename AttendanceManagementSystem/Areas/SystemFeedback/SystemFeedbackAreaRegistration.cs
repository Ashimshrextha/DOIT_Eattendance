using System.Web.Mvc;

namespace AttendanceManagementSystem.Areas.SystemFeedback
{
    public class SystemFeedbackAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SystemFeedback";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SystemFeedback_default",
                "SystemFeedback/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}