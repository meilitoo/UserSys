using System;
using System.Collections.Generic;
using System.Text;
using UserSysCore.Models;

namespace UserSysCore.Service
{
  public  interface IProductService
    {
        bool AddProduct(Product productInfo, out string msg);
        bool DelProduct(int productId);
        bool UpdateProduct(Product productInfo, out string msg);
        IList<Product> GetProductList();
        IList<Product> GetProductList(Pagination pageInfo);
        Product GetProduct(int productId);
    }
}
