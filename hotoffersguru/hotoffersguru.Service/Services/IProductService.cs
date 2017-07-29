using System.Collections.Generic;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;

namespace hotoffersguru.Service.Services
{
    public interface IProductService
    {
        List<ProductDetail> GetAllOffersByStoreName(string storeName);
        List<ProductDetail> GetAllOffers();
        List<ProductDetail> DiscountDeals(double maxpercentageDiscount, double percentageDiscount);
        List<ProductDetail> GetProductsByKeyword(List<string> keyword);
    }
}