using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore;
using UserSysCore.Models;

namespace UserWeb
{
    public static class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();
            if(!context.UserInfos.Any())
            {
                
                var users = new UserInfo[]
                {
                    new UserInfo{ LoginName="user1", LoginPwd="e10adc3949ba59abbe56e057f20f883e", UserName="sss", CreateTime=DateTime.Now},
                    new UserInfo{ LoginName="user12", LoginPwd="e10adc3949ba59abbe56e057f20f883e", UserName="sss", CreateTime=DateTime.Now},
                    new UserInfo{ LoginName="user11", LoginPwd="e10adc3949ba59abbe56e057f20f883e", UserName="sss", CreateTime=DateTime.Now}
                };
                foreach(var u in users)
                {
                    context.UserInfos.Add(u);
                }
                context.SaveChanges();
            }
        }
    }
}
