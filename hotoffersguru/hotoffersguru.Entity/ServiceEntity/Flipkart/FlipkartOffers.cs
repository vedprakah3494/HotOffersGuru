using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotoffersguru.Entity.ServiceEntity
{
    public class FlipkartOffers
    {
        public Allofferslist[] allOffersList { get; set; }
    }

    public class Allofferslist
    {
        public long startTime { get; set; }
        public long endTime { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public Imageurl[] imageUrls { get; set; }
        public string availability { get; set; }
    }

  
}
