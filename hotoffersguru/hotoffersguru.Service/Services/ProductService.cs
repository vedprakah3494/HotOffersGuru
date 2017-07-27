using hotoffersguru.Entity;
using hotoffersguru.Entity.Models;
using hotoffersguru.Service.APIConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotoffersguru.Entity.ServiceEntity;
using hotoffersguru.Service.APIConfiguration.Amazon;
using hotoffersguru.Service.APIConfiguration.Flipkart;
using hotoffersguru.Service.Services;

namespace hotoffersguru.Service
{
    public class ProductService : IProductService
    {
        private readonly IFlipkart _flipkart;
        private readonly IAmazon _amazon;
        public ProductService(IFlipkart flipkart, IAmazon amazon)
        {
            _flipkart = flipkart;
            _amazon = amazon;

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
            productDetaillist = _flipkart.GetProductByKeyword(keyword);
            return productDetaillist;

        }
        public List<ProductDetail> GetAllOffers()
        {
            var productDetaillist = _flipkart.GetProductByKeyword("Hot offers");
            return productDetaillist;

        }
        public List<ProductDetail> DiscountDeals()
        {
            var productDetaillist = new List<ProductDetail>();
            var amazonproductlist = _amazon.DiscountDeals();
            productDetaillist = amazonproductlist;
            return productDetaillist;
        }
    }
}
