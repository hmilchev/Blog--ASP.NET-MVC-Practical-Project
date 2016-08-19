using System.Web;
using System.Web.Mvc;

namespace Blog__ASP.NET_MVC_Practical_Project
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
