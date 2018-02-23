using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using UserSysCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UserWeb.Models
{
    public class AddMenuViewModel:Menu
    {
        public string ErrorMsg
        {
            get;
            set;
        }

        public bool IsEdit
        {
            get;
            set;
        }

        public bool IsParentMenu
        {
            get;
            set;
        }

        public SelectList ControllerList
        {
            get {
                if (!IsParentMenu)
                {
                    
                    Assembly asm = Assembly.GetExecutingAssembly();
                    var controllers = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).Select(p => p.Name.Replace("Controller",""));
                   var _controllerList= new SelectList(controllers);

                    return _controllerList;

                }
                else
                    return null;
            }
           
            
        }

        public SelectList ParentMenuList
        {
            get;
            set;
        }
    }
}
