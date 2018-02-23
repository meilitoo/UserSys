using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UserSysCore.Models;

namespace UserSysCore.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IApplicationContext applicationContext, UserContext context) : base(applicationContext, context)
        {

        }
        public bool AddProduct(Product productInfo, out string msg)
        {
            productInfo.CreatorId = ApplicationContext.CurrentUserId;
            productInfo.CreateTime = DateTime.Now;
            var newProduct = Add(productInfo);
            if (newProduct.ProductId > 0)
            {
                msg = newProduct.ProductId.ToString();
                return true;
            }
            else
            {
                msg = "添加失败";
                return false;
            }
        }

        public bool DelProduct(int productId)
        {
            Remove(productId);
            return true;
        }

        public Product GetProduct(int productId)
        {
            return Get(productId);
        }

        public IList<Product> GetProductList()
        {
            return Get().ToList();
        }

        public IList<Product> GetProductList(Pagination pageInfo)
        {
            return Get(null, pageInfo);
        }

        public bool UpdateProduct(Product productInfo, out string msg)
        {
            var pdModel=Get(productInfo.ProductId);
            if (pdModel == null)
            {
                msg = "未找到产品信息";
                return false;
            }

            pdModel.ProductDes = productInfo.ProductDes;
            pdModel.ProductName = productInfo.ProductName;
            Update(pdModel);
            msg = "更新成功";
            return true;
        }
    }
}
