using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace UserSysCore
{
   public interface IApplicationContext
    {
        IHostingEnvironment HostingEnvironment { get; }
        bool IsAuthenticated { get; }

        string CurrentUserName { get;}

        int CurrentUserId { get; }

        /// <summary>
        /// ,分隔的角色ID字符串
        /// </summary>
        string CurrentUserRoles { get; }
    }
}
