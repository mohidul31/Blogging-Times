using System.Web.Mvc;

namespace Blogging_Times.Web.Areas.AjaxMagic
{
    public class AjaxMagicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AjaxMagic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AjaxMagic_default",
                "AjaxMagic/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}