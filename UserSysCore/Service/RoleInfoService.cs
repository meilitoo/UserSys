using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UserSysCore.Models;

namespace UserSysCore.Service
{
  public  class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        public RoleInfoService(IApplicationContext applicationContext, UserContext context) : base(applicationContext, context)
        {

        }

        public bool AddRole(RoleInfo model, out string msg, params int[] menuIds)
        {
            BeginTransaction(() => {
                DbContext.RoleInfos.Add(model);
                DbContext.SaveChanges();
                List<RoleToMenu> list = new List<RoleToMenu>();
                foreach(int menuId in menuIds)
                {
                    list.Add(new RoleToMenu { MenuId = menuId, RoleId = model.RoleId });
                }
                DbContext.RoleToMenus.AddRange(list);
                DbContext.SaveChanges();
            });
            msg = "ok";
            return true;
        }

        public bool DelRole(int roleId)
        {
            BeginTransaction(() => {

                DbContext.UserToRoles.RemoveRange(DbContext.Set<UserToRole>().Where(p => p.RoleId == roleId));
                DbContext.RoleToMenus.RemoveRange(DbContext.Set<RoleToMenu>().Where(p => p.RoleId == roleId));
                Remove(roleId);
                DbContext.SaveChanges();
            });
            return true;
        }

        public RoleInfo GetRole(int roleId)
        {
           return Get(roleId);
        }

        public IList<RoleInfo> GetRoleList()
        {
            return Get().ToList();
        }

        public bool UpdateRole(RoleInfo model, out string msg, params int[] menuIds)
        {
            RoleInfo newModel = Get(model.RoleId);
            newModel.RoleName = model.RoleName;
            BeginTransaction(() => {
                DbContext.RoleInfos.Update(newModel);
                DbContext.RoleToMenus.RemoveRange(DbContext.RoleToMenus.Where(p => p.RoleId == model.RoleId));
                List<RoleToMenu> list = new List<RoleToMenu>();
                foreach (int menuId in menuIds)
                {
                    list.Add(new RoleToMenu { MenuId = menuId, RoleId = model.RoleId });
                }
                DbContext.RoleToMenus.AddRange(list);
                DbContext.SaveChanges();
            });
            msg = "ok";
            return true;
        }
    }
}
