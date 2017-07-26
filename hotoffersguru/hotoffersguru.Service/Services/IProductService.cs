using System.Collections.Generic;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;

namespace hotoffersguru.Service
{
    public interface IProductService
    {
        FlipkartOffers GetAllOffers();
        FlipkartDeals GetDealsOfTheDay();
        List<ProductDetail> GetProductsByCategory();
        List<ProductDetail> GetProductsByKeyword();
    }
}