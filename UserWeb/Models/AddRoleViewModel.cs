using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;
using UserSysCore.TagHelpers;

namespace UserWeb.Models
{
    public class AddRoleViewModel
    {
        public RoleInfo RoleInfo
        {
            get;
            set;
        }

        public IList<Menu> RoleMenus
        {
            get;
            set;
        }

        public IList<TreeMenu> MenuList
        {
            get;
            set;
        }

        public bool IsEdit
        {
            get;
            set;
        }

        public IList<CheckBoxItem> GetCheckBoxItem(IList<Menu> menuList)
        {
            var list = new List<CheckBoxItem>();
            foreach(var menu in menuList)
            {
                bool checkStatus = false;
                if (IsEdit)
                {
                    checkStatus = RoleMenus.Contains(menu);
                }
                CheckBoxItem item = new CheckBoxItem
                {
                    Text = menu.MenuName,
                    Value = menu.MenuId.ToString(),
                     Checked=checkStatus
                   
                };
               
                list.Add(item);
            }
            return list;
        }

        
    }
}
