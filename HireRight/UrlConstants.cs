using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace HireRight
{
    public static class UrlConstants
    {
        public static readonly string HireRightBaseUrl;

        static UrlConstants()
        {
            HireRightBaseUrl = WebConfigurationManager.AppSettings["HireRightBaseUrl"];
        }
    }
}