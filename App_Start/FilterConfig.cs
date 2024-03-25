using System.Web;
using System.Web.Mvc;

namespace DBSD_00013940_00014016_00013782
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
