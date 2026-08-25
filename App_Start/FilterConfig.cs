using System.Web;
using System.Web.Mvc;

namespace HarborConnect
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection globalFilters)
        {
            globalFilters.Add(new HandleErrorAttribute());
        }
    }
}
