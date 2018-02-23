using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserWeb.Models
{
    public class MenuSidebarViewModel
    {
        public IList<TreeMenu> UserMenuTree { get; set; }

        public int SelectedMenuId { get; set; }
    }
}
