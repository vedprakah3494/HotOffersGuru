using System.Collections.Generic;
using hotoffersguru.Entity.Models;

namespace hotoffersguru.Service.APIConfiguration
{
    public interface IFlipkart
    {
        List<ProductDetail> AllOffer();
        List<ProductDetail> DealOfTheDay();
        List<ProductDetail> getProductDetailFlipkart(string CategoryName);
        List<ProductDetail> SearchProductDetailFlipkart(string SearchKeyword, int resultItem = 10);
    }
}