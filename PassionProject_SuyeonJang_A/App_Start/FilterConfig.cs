using System.Web;
using System.Web.Mvc;

namespace PassionProject_SuyeonJang_A
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
