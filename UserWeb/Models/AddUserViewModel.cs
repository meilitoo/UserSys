using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;
using UserSysCore.TagHelpers;

namespace UserWeb.Models
{
    public class AddUserViewModel:UserInfo
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

        public IList<RoleInfo> AllRoleList { get; set; }
        public IList<RoleInfo> UserRoleList { get; set; }

        public IList<CheckBoxItem> GetCheckBoxItem()
        {
            var list = new List<CheckBoxItem>();
            if (AllRoleList == null)
                return list;
            foreach (var role in AllRoleList)
            {
                bool checkStatus = false;
                if (IsEdit)
                {
                    checkStatus = UserRoleList.Contains(role);
                }
                CheckBoxItem item = new CheckBoxItem
                {
                    Text = role.RoleName,
                    Value = role.RoleId.ToString(),
                    Checked = checkStatus

                };

                list.Add(item);
            }
            return list;
        }
    }
}
