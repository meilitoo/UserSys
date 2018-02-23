using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using UserSysCore;

namespace UserWeb.Controllers
{
    [Authorize(Policy = "Permission")]
    public class BaseController : Controller
    {

        public BaseController(IApplicationContext applicationContext)
        {
            AppContext= applicationContext;
         
        }

     

        protected IApplicationContext AppContext
        {
            get;
            set;
        }
        
     
    }
}