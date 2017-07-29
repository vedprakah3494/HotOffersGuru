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
        public List<ProductDetail> GetProductsByCategory(List<string> category)
        {
            var productDetaillist = _amazon.GetProductsByCatgegory(category);
            return productDetaillist;
        }

        public List<ProductDetail> GetProductsByKeyword(string keyword)
        {
            var productDetaillist = _flipkart.GetProductByKeyword(keyword);
            return productDetaillist;
        }
        public List<ProductDetail> GetAllOffers()
        {
            var productDetaillist = _amazon.AllOffer();
            return productDetaillist;

        }
        public List<ProductDetail> DiscountDeals(double maxpercentageDiscount, double minpercentageDiscount)
        {
            var amazonproductlist = _amazon.AllOffer().Where(x=>Convert.ToDouble(x.ProductAttribute.discountPercentage)>minpercentageDiscount && Convert.ToDouble(x.ProductAttribute.discountPercentage) <maxpercentageDiscount);
            var productDetaillist = amazonproductlist.ToList();
            return productDetaillist;
        }
    }
}
