using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hotoffersguru.Service.Services;

namespace hotoffersguru.Areas.Portal.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        public StoreController(IProductService productService)
        {
            _productService = productService;

        }
        // GET: Portal/Store
        public ActionResult ProductsbyStore(string StoreName)
        {
            var productlist = _productService.GetAllOffersByStoreName(StoreName);
            return View("_ProductsbyStore", productlist);
        }
    }
}