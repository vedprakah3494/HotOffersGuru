using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hotoffersguru.Entity.ServiceEntity.Flipkart
{
    public class KeywordSearchResult
    {
        public Product[] products { get; set; }
    }

    public class Product
    {
        public Productbaseinfov1 productBaseInfoV1 { get; set; }
        public Productshippinginfov1 productShippingInfoV1 { get; set; }
        public Categoryspecificinfov1 categorySpecificInfoV1 { get; set; }
    }

    public class Productbaseinfov1
    {
        public string productId { get; set; }
        public string title { get; set; }
        public string productDescription { get; set; }
        public Imageurls imageUrls { get; set; }
        public string[] productFamily { get; set; }
        public Maximumretailprice maximumRetailPrice { get; set; }
        public Flipkartsellingprice flipkartSellingPrice { get; set; }
        public Flipkartspecialprice flipkartSpecialPrice { get; set; }
        public string productUrl { get; set; }
        public string productBrand { get; set; }
        public bool inStock { get; set; }
        public bool codAvailable { get; set; }
        public float discountPercentage { get; set; }
        public string[] offers { get; set; }
        public string categoryPath { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Imageurls
    {
        [JsonProperty("200x200")]
        public string _200x200 { get; set; }
        [JsonProperty("400x400")]
        public string _400x400 { get; set; }
        [JsonProperty("800x800")]
        public string _800x800 { get; set; }
    }

    public class Maximumretailprice
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Flipkartsellingprice
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Flipkartspecialprice
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Attributes
    {
        public string size { get; set; }
        public string color { get; set; }
        public string storage { get; set; }
        public string sizeUnit { get; set; }
        public string displaySize { get; set; }
    }

    public class Productshippinginfov1
    {
        public Shippingcharges shippingCharges { get; set; }
        public string estimatedDeliveryTime { get; set; }
        public string sellerName { get; set; }
        public float sellerAverageRating { get; set; }
        public int sellerNoOfRatings { get; set; }
        public int sellerNoOfReviews { get; set; }
    }

    public class Shippingcharges
    {
        public float amount { get; set; }
        public string currency { get; set; }
    }

    public class Categoryspecificinfov1
    {
        public string[] keySpecs { get; set; }
        public string[] detailedSpecs { get; set; }
        public Specificationlist[] specificationList { get; set; }
        public Booksinfo booksInfo { get; set; }
        public Lifestyleinfo lifeStyleInfo { get; set; }
    }

    public class Booksinfo
    {
        public object language { get; set; }
        public object binding { get; set; }
        public object pages { get; set; }
        public object publisher { get; set; }
        public int year { get; set; }
        public object[] authors { get; set; }
    }

    public class Lifestyleinfo
    {
        public object sleeve { get; set; }
        public object neck { get; set; }
        public object idealFor { get; set; }
    }

    public class Specificationlist
    {
        public string key { get; set; }
        public Value[] values { get; set; }
    }

    public class Value
    {
        public string key { get; set; }
        public string[] value { get; set; }
    }

}
