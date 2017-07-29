using hotoffersguru.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotoffersguru.Areas.Portal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;
        public CategoryController(IProductService productService)
        {
            _productService = productService;

        }
       
        public ActionResult ProductByCategory(string CategoryName)
        {
            var categoryList = new List<string>();
            categoryList.Add(CategoryName);
            var productlist = _productService.GetProductsByCategory(categoryList);
            return PartialView("_ProductByCategory", productlist);
        }
    }
}