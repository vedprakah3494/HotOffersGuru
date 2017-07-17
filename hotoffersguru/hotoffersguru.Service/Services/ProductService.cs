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
    public class ProductService 
    {
        public List<ProductDetail> GetProductDetail()
        {

            var productDetail = new List<ProductDetail>();
            Flipkart fp = new Flipkart();
            string category = "bags wallets belts";
            productDetail= fp.getProductDetailFlipkart(category);
            return productDetail;

        }

        public List<ProductDetail> GetProductDetailByKeword()
        {

            var productDetail = new List<ProductDetail>();
            Flipkart fp = new Flipkart();
            string keyword = "sony+mobiless";
            productDetail = fp.SearchProductDetailFlipkart(keyword);
            return productDetail;

        }
    }
}
