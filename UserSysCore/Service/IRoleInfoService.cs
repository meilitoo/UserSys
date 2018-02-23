using System;
using System.Collections.Generic;
using System.Text;
using UserSysCore.Models;

namespace UserSysCore.Service
{
   public interface IRoleInfoService
    {
        bool AddRole(RoleInfo model,out string msg, params int[] menuIds);
        bool UpdateRole(RoleInfo model, out string msg, params int[] menuIds);
        bool DelRole(int roleId);
        IList<RoleInfo> GetRoleList();
        RoleInfo GetRole(int roleId);
    }
}
