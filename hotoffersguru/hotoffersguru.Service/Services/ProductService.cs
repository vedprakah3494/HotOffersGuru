using hotoffersguru.Entity;
using hotoffersguru.Entity.Models;
using hotoffersguru.Service.APIConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotoffersguru.Service
{
    public class ProductService : IProductService
    {
        private readonly IFlipkart _flipkart;
        public ProductService(IFlipkart flipkart)
        {
            _flipkart = flipkart;

        }
        public List<ProductDetail> GetProductDetail()
        {

            var productDetaillist = new List<ProductDetail>();
            string category = "bags wallets belts";
            productDetaillist = _flipkart.getProductDetailFlipkart(category);
            return productDetaillist;

        }

        public List<ProductDetail> GetProductDetailByKeword()
        {

            var productDetaillist = new List<ProductDetail>();
            string keyword = "sony+mobiless";
            productDetaillist = _flipkart.SearchProductDetailFlipkart(keyword);
            return productDetaillist;

        }
        public List<ProductDetail> ProductListHomePage()
        {

            var productDetaillist = new List<ProductDetail>();
            productDetaillist = _flipkart.AllOffer(); 
            return productDetaillist;

        }
    }
}
