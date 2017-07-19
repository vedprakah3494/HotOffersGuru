using System.Collections.Generic;
using hotoffersguru.Entity.Models;

namespace hotoffersguru.Service
{
    public interface IProductService
    {
        List<ProductDetail> GetProductDetail();
        List<ProductDetail> GetProductDetailByKeword();
        List<ProductDetail> ProductListHomePage();
    }
}