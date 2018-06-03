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
        public List<ProductDetail> GetAllOffersByStoreName(string storeName)
        {
            var productDetaillist = new List<ProductDetail>();
            if (storeName == "AZ")
            {

                productDetaillist.AddRange(_amazon.AllOffer());

            }
            else if (storeName == "FK")
            {

                productDetaillist.AddRange(_flipkart.AllOffer());

            }
            return productDetaillist.OrderByDescending(x => x.ProductAttribute.discountPercentage).ToList();

        }
        public List<ProductDetail> GetProductsByKeyword(List<string> keywordluList)
        {
            var productDetaillist = new List<ProductDetail>();
            productDetaillist.AddRange(_amazon.GetProductByKeyword(keywordluList));
            productDetaillist.AddRange(_flipkart.GetProductByKeyword(keywordluList));
            return productDetaillist.OrderByDescending(x => x.ProductAttribute.discountPercentage).ToList();
        }
        public List<ProductDetail> GetAllOffers()
        {
            var productDetaillist = new List<ProductDetail>();
            productDetaillist.AddRange(_amazon.AllOffer());
            productDetaillist.AddRange(_flipkart.AllOffer());
            return productDetaillist.OrderByDescending(x => x.ProductAttribute.discountPercentage).ToList();

        }
        public List<ProductDetail> DiscountDeals(double maxpercentageDiscount, double minpercentageDiscount)
        {
            var amazonproductlist = _amazon.AllOffer().Where(x => Convert.ToDouble(x.ProductAttribute.discountPercentage) > minpercentageDiscount && Convert.ToDouble(x.ProductAttribute.discountPercentage) < maxpercentageDiscount);
            var productDetaillist = amazonproductlist.ToList();
            return productDetaillist;
        }
    }
}
