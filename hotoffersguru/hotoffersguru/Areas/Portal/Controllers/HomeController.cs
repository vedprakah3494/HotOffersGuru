using System.Collections.Generic;
using System.Web.Mvc;
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
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public PartialViewResult HotOffersList()
        {
            var productlist = _productService.GetAllOffers();
            return PartialView("_HotOffersList", productlist);
        }
        // GET: Portal/Home   
        public PartialViewResult HomeCategory(string CategoryName)
        {
            ViewBag.TabID = CategoryName.Replace(" ","").Trim();
            var categoryList = new List<string>();
            categoryList.Add(CategoryName);
            var productlist = _productService.GetProductsByCategory(categoryList);
            return PartialView("_HomeCategory", productlist);
        }

    }
}
