using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;

namespace UserWeb.Models
{
    public class TreeMenu
    {
        public Menu ParentMenu
        {
            get;
            set;
        }

        public IList<Menu> SubMenus
        {
            get;
            set;
        }
    }
}
