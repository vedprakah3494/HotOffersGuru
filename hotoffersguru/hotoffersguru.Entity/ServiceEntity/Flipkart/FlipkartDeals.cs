using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotoffersguru.Entity.ServiceEntity
{

    public class FlipkartDeals
    {
        public Dotdlist[] dotdList { get; set; }
    }

    public class Dotdlist
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public Imageurl[] imageUrls { get; set; }
        public string availability { get; set; }
    }

    public class Imageurl
    {
        public string url { get; set; }
        public string resolutionType { get; set; }
    }
   

}
