using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore;
using UserSysCore.Models;

namespace UserWeb.Models
{
    public class ProductListViewModel
    {
        public IList<Product> ProductList { get; set; }

        public Pagination PageInfo { get; set; }
    }
}
