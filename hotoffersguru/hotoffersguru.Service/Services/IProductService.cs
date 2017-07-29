using System.Collections.Generic;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;

namespace hotoffersguru.Service.Services
{
    public interface IProductService
    {
        List<ProductDetail> GetAllOffers();
        List<ProductDetail> DiscountDeals(double maxpercentageDiscount, double percentageDiscount);
        List<ProductDetail> GetProductsByCategory(List<string> category);
        List<ProductDetail> GetProductsByKeyword(string keyword);
    }
}