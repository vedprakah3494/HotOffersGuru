using hotoffersguru.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace hotoffersguru.Areas.Portal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;
        public CategoryController(IProductService productService)
        {
            _productService = productService;

        }
        [OutputCache(Duration = 3600, VaryByParam = "CategoryName")]
        public ActionResult ProductByCategory(string CategoryName)
        {
            var categoryList = new List<string>();
            categoryList = CategoryName.Split(',').ToList();
            var productlist = _productService.GetProductsByKeyword(categoryList);
            return PartialView("_ProductByCategory", productlist);
        }
    }
}