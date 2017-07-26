using hotoffersguru.Entity;
using hotoffersguru.Entity.Models;
using hotoffersguru.Service.APIConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotoffersguru.Entity.ServiceEntity;

namespace hotoffersguru.Service
{
    public class ProductService : IProductService
    {
        private readonly IFlipkart _flipkart;
        public ProductService(IFlipkart flipkart)
        {
            _flipkart = flipkart;

        }
        public List<ProductDetail> GetProductsByCategory()
        {

            var productDetaillist = new List<ProductDetail>();
            string category = "bags wallets belts";
            productDetaillist = _flipkart.GetProductsByCatgegory(category);
            return productDetaillist;

        }

        public List<ProductDetail> GetProductsByKeyword()
        {

            var productDetaillist = new List<ProductDetail>();
            string keyword = "sony+mobiless";
            productDetaillist = _flipkart.GetProductByKeword(keyword);
            return productDetaillist;

        }
        public FlipkartOffers GetAllOffers()
        {
            //var amazonOffer=
           var productDetaillist = _flipkart.AllOffer(); 
            return productDetaillist;

        }
        public FlipkartDeals GetDealsOfTheDay()
        {
            var productDetaillist = _flipkart.GetDealOfTheDay();
            return productDetaillist;
        }
    }
}
