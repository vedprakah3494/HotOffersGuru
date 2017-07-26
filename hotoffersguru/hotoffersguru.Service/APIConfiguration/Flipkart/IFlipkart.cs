using System.Collections.Generic;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;

namespace hotoffersguru.Service.APIConfiguration
{
    public interface IFlipkart
    {
        FlipkartOffers AllOffer();
        FlipkartDeals GetDealOfTheDay();
        List<ProductDetail> GetProductsByCatgegory(string categoryName);
        List<ProductDetail> GetProductByKeword(string searchKeyword, int resultItem = 10);
    }
}