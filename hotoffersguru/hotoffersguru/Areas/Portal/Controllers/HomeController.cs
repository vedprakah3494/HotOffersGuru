using System.Collections.Generic;
using System.Dynamic;
using hotoffersguru.Service;
using System.Web.Mvc;
using hotoffersguru.Entity.Models;
using hotoffersguru.Service.Services;

namespace hotoffersguru.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;

        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Portal/Home
        public PartialViewResult ProductList()
        {
            var productlist = _productService.DiscountDeals();
            return PartialView("_ProductList", productlist);
        }
        public PartialViewResult HotOffersList()
        {
            var productlist = _productService.GetAllOffers();
            return PartialView("_HotOffersList", productlist);
        }
        public PartialViewResult CategoryDetail()
        {
            return PartialView();
        }


    }
}
