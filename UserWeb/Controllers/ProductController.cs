using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserSysCore.Service;
using UserSysCore.Models;
using UserWeb.Models;

namespace UserWeb.Controllers
{
    public class ProductController : BaseController
    {
        IProductService _ProductService;
        public ProductController(IProductService productService, IApplicationContext applicationContext) : base(applicationContext)
        {
            _ProductService = productService;
        }
        public IActionResult AddProduct()
        {
            var vm = new AddProductViewModel
            {
                IsEdit = false
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
           
            if(_ProductService.AddProduct(product,out string msg))
            {
                return RedirectToAction("ProductList");
            }
            var vm = new AddProductViewModel
            {
                IsEdit = false,
                ProductModel=product
            };
            return View(vm);
        }
        public IActionResult DelProduct(int pid)
        {
            _ProductService.DelProduct(pid);
            return RedirectToAction("ProductList");
        }
        public IActionResult EditProduct(int pid)
        {
            var pModel = _ProductService.GetProduct(pid);
            var vm = new AddProductViewModel
            {
                IsEdit = true,
                 ProductModel=pModel
            };
            return View("AddProduct",vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {
            _ProductService.UpdateProduct(product, out string msg);
            return RedirectToAction("ProductList");
        }

        public IActionResult ProductList(int page=1)
        {
            int pageIndex = page - 1;
            if (pageIndex < 0)
                pageIndex = 0;

            Pagination pagination = new Pagination
            {
                PageIndex = pageIndex
            };
            ProductListViewModel vm = new ProductListViewModel
            {
                ProductList = _ProductService.GetProductList(pagination), PageInfo=pagination
            };
            return View(vm);
        }
    }
}