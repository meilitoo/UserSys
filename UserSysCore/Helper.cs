using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace UserSysCore
{
   public static class Helper
    {
        public static string GetMD5Hash(string strVal)
        {
            return String.Join("", MD5.Create()
                         .ComputeHash(Encoding.UTF8.GetBytes(strVal))
                         .Select(b => b.ToString("x2")));
        }
    }
}
