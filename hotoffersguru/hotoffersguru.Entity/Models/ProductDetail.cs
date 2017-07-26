using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotoffersguru.Entity.Models
{
    public class ProductDetail:Entity
    {
        public string ProductID { get; set; }
        public Productattributes ProductAttribute { get; set; }
        public CategoryDetail ProductCategory { get; set; }
        public object[] Company { get; set; }

    }

    public class Productattributes
    {
        public string title { get; set; }
        public string productDescription { get; set; }
        public Imageurls imageUrls { get; set; }
        public Maximumretailprice maximumRetailPrice { get; set; }
        public Sellingprice sellingPrice { get; set; }
        public string productUrl { get; set; }
        public string productBrand { get; set; }
        public bool inStock { get; set; }
        public bool isAvailable { get; set; }
        public bool codAvailable { get; set; }
        public object emiAvailable { get; set; }
        public float discountPercentage { get; set; }
        public object cashBack { get; set; }
        public object[] offers { get; set; }
    }

    public class Imageurls
    {
        public string _200x200 { get; set; }
        public string _400x400 { get; set; }
        public string _800x800 { get; set; }
        public string unknown { get; set; }
        public string _97x6 { get; set; }
        public string _4x4 { get; set; }
        public string _4x8 { get; set; }
        public string _7x3 { get; set; }
        public string _5x9 { get; set; }
        public string _9x68 { get; set; }
        public string _47x9 { get; set; }
        public string _3x4 { get; set; }
        public string _9x4 { get; set; }
        public string _8x4 { get; set; }
    }

    public class Maximumretailprice
    {
        public float amount { get; set; }
    }

    public class Sellingprice
    {
        public float amount { get; set; }
    }

 
}
