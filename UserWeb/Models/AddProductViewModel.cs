using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;

namespace UserWeb.Models
{
    public class AddProductViewModel
    {
        public Product ProductModel { get; set; }

        public bool IsEdit { get; set; }
    }
}
