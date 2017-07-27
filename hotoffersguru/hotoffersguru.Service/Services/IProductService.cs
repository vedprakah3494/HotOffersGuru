using System.Collections.Generic;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;

namespace hotoffersguru.Service.Services
{
    public interface IProductService
    {
        List<ProductDetail> GetAllOffers();
        List<ProductDetail> DiscountDeals();
        List<ProductDetail> GetProductsByCategory();
        List<ProductDetail> GetProductsByKeyword();
    }
}