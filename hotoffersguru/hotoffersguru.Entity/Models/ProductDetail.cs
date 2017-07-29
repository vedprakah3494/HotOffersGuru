using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using hotoffersguru.Entity.Common;

namespace hotoffersguru.Entity.Models
{
    public class ProductDetail : Entity
    {
        public string ProductID { get; set; }
        public Productattributes ProductAttribute { get; set; }
        public CategoryDetail ProductCategory { get; set; }
        public CompanyName[] Company { get; set; }

    }

    public class Productattributes
    {
        public string title { get; set; }
        public string productDescription { get; set; }
        public Imageurls imageUrls { get; set; }
        public string maximumRetailPrice { get; set; }
        public string sellingPrice { get; set; }
        public string productUrl { get; set; }
        public string productBrand { get; set; }
        public string discountPercentage { get; set; }
        public string savedAmount { get; set; }
        public object[] offers { get; set; }
    }

    public class Imageurls
    {
        public string LargeImage { get; set; }
        public string MediumImage { get; set; }
        public string SmallImage { get; set; }
        public string TinyImage { get; set; }

    }




}
