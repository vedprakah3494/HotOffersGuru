using System.Collections.Generic;
using hotoffersguru.Entity.Models;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;

namespace hotoffersguru.Service.APIConfiguration.Amazon
{
    class Amazon
    {
        readonly AmazonAuthentication _authentication = new AmazonAuthentication();
        private const string AssociateTag = "hotoffersguru-21";
        public Amazon()
        {
            _authentication.AccessKey = "AKIAJX4A7H4FYEHWY4IA";
            _authentication.SecretKey = "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";
        }


        public List<ProductDetail> AllOffer(AmazonSearchIndex amzonAmazonSearchIndex, AmazonResponseGroup amazonResponseGroup)
        {
            var productlist = new List<ProductDetail>();
            var wrapper = new AmazonWrapper(_authentication, AmazonEndpoint.IN, AssociateTag);
            var result = wrapper.Search("canon eos", amzonAmazonSearchIndex, amazonResponseGroup);
            return productlist;
        }

    }
}
