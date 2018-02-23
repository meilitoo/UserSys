using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserSysCore.Models;
using UserSysCore.Service;
using UserWeb.Models;


namespace UserWeb.Components
{
    [ViewComponent(Name = "Pagination")]
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Pagination pagination)
        {
            return View(pagination);
        }
    }
}
