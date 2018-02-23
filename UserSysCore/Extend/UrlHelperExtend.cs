using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace UserSysCore.Extend
{
    public static class UrlHelperExtend
    {
        public static string PageUrl(this IUrlHelper helper, int pageIndex)
        {
            int page = pageIndex + 1;
            if (page < 1)
                page = 1;
            return QueryHelpers.AddQueryString(helper.ActionContext.HttpContext.Request.Path, "page", page.ToString());
        }
    }
}
