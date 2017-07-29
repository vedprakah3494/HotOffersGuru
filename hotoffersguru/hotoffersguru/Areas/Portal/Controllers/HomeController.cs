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
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public PartialViewResult Mobiles()
        {
            var productlist = _productService.GetProductsByCategory("Mobile Phones");
            return PartialView("_Mobiles", productlist);
        }
        // GET: Portal/Home   
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public PartialViewResult Computers()
        {
            var productlist = _productService.GetProductsByCategory("Computers");
            return PartialView("_Computers", productlist);
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public PartialViewResult SpecialEvent()
        {
            var productlist = _productService.GetProductsByCategory("Raksha Bandhan");
            return PartialView("_SpecialEvent", productlist);
        }

    }
}
