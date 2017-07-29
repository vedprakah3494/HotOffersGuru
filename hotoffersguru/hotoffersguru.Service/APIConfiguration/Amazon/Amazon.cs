using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using hotoffersguru.Entity.Models;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;

namespace hotoffersguru.Service.APIConfiguration.Amazon
{
    public interface IAmazon
    {
        List<ProductDetail> AllOffer();
        List<ProductDetail> GetProductByKeyword(List<string> categoryName, int resultItem = 10);
    }

    public class Amazon : IAmazon
    {
        readonly AmazonAuthentication _authentication = new AmazonAuthentication();
        private const string AssociateTag = "hotoffersguru-21";
        private static string StoreUrl = "localhost:3529/Areas/Portal/Content/StoreLogo/azlogo.png";
        List<string> CategoryList = new List<string>();

        public Amazon()
        {
            _authentication.AccessKey = "AKIAJX4A7H4FYEHWY4IA";
            _authentication.SecretKey = "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";
            CategoryList.Add("Mobile");
            CategoryList.Add("Mobile Accessories");
            CategoryList.Add("Televisions");
            CategoryList.Add("computer storage");
            CategoryList.Add("bags wallets belts");
            CategoryList.Add("mens footwear");
            CategoryList.Add("mens clothing");
        }


        public List<ProductDetail> GetProductByKeyword(List<string> categoryName,int resultItem=10)
        {
            var productlist = new List<ProductDetail>();
            var wrapper = new AmazonWrapper(_authentication, AmazonEndpoint.IN, AssociateTag);
            var responseGroup = AmazonResponseGroup.Large;
            foreach (var item in categoryName)
            {
                var result = wrapper.Search(item, AmazonSearchIndex.All, responseGroup);
                var productresponse = new List<ProductDetail>();
                if (result != null)
                {
                    productresponse = MapAmazonProductDetails(result);
                }

                productlist.AddRange(productresponse);
            }
            return productlist;
        }


        public List<ProductDetail> AllOffer()
        {

            var productlist = new List<ProductDetail>();
            var wrapper = new AmazonWrapper(_authentication, AmazonEndpoint.IN, AssociateTag);
            var responseGroup = AmazonResponseGroup.Large;
            foreach (var item in CategoryList)
            {
                var result = wrapper.Search(item, AmazonSearchIndex.All, responseGroup);
                var productresponse = new List<ProductDetail>();
                if (result != null)
                {
                    productresponse = MapAmazonProductDetailsOfferDetails(result);
                }
                productlist.AddRange(productresponse);
            }

            return productlist;
        }
        private static List<ProductDetail> MapAmazonProductDetails(AmazonItemResponse result)
        {
            var productlist = new List<ProductDetail>();
            foreach (var item in result.Items.Item.Where(m => m.Offers != null && m.Offers.Offer != null ))
            {
                var productdetail = new ProductDetail();
                productdetail.StoreCode = "AZ";
                productdetail.StoreLogoUrl = StoreUrl;
                productdetail.ProductAttribute = new Productattributes();

                productdetail.ProductAttribute.title = item.ItemAttributes.Title;

                productdetail.ProductAttribute.productUrl = item.DetailPageURL;
                productdetail.ProductAttribute.imageUrls = new Imageurls();
                if (item.LargeImage != null && item.MediumImage != null && item.SmallImage != null)
                {
                    productdetail.ProductAttribute.imageUrls.LargeImage = item.LargeImage?.URL;
                    productdetail.ProductAttribute.imageUrls.MediumImage = item.MediumImage?.URL;
                    productdetail.ProductAttribute.imageUrls.SmallImage = item.SmallImage?.URL;
                }
                else if (item.ImageSets != null)
                {
                    productdetail.ProductAttribute.imageUrls.LargeImage = item.ImageSets[0].LargeImage.URL;
                    productdetail.ProductAttribute.imageUrls.MediumImage = item.ImageSets[0].MediumImage.URL;
                    productdetail.ProductAttribute.imageUrls.SmallImage = item.ImageSets[0].SmallImage.URL;
                }
                productdetail.ProductID = item.ASIN;

                var salePriceFormattedPrice = item.Offers.Offer[0].OfferListing[0].SalePrice?.FormattedPrice;
                if (salePriceFormattedPrice != null)
                    productdetail.ProductAttribute.sellingPrice =
                        salePriceFormattedPrice.Replace("INR", "₹");
                if (salePriceFormattedPrice != null)
                {
                    var priceFormattedPrice = item.Offers.Offer[0].OfferListing[0].Price?.FormattedPrice;
                    if (priceFormattedPrice != null)
                        productdetail.ProductAttribute.maximumRetailPrice =
                            priceFormattedPrice.Replace("INR", "₹");
                }
                else
                {
                    var priceFormattedPrice = item.Offers.Offer[0].OfferListing[0].Price?.FormattedPrice;
                    if (priceFormattedPrice != null)
                        productdetail.ProductAttribute.sellingPrice =
                            priceFormattedPrice.Replace("INR", "₹");
                    var maximumprice = (Convert.ToDouble(item.Offers.Offer[0].OfferListing[0].Price?.Amount) + Convert.ToDouble(item.Offers.Offer[0].OfferListing[0].AmountSaved?.Amount)) / 100;

                    productdetail.ProductAttribute.maximumRetailPrice = "₹" + maximumprice;
                }


                productdetail.ProductAttribute.discountPercentage = item.Offers.Offer[0].OfferListing[0]?.PercentageSaved;
                var amountSaved = item.Offers.Offer[0].OfferListing[0]?.AmountSaved;
                if (amountSaved != null)
                    productdetail.ProductAttribute.savedAmount =
                        (amountSaved?.FormattedPrice).Replace("INR", "₹");
                productlist.Add(productdetail);
            }
            return productlist;
        }
        private static List<ProductDetail> MapAmazonProductDetailsOfferDetails(AmazonItemResponse result)
        {
            var productlist = new List<ProductDetail>();
            foreach (var item in result.Items.Item.Where(m => m.Offers != null && m.Offers.Offer != null && m.Offers.Offer.Length > 0 && m.Offers.Offer[0].OfferListing != null && m.Offers.Offer[0].OfferListing[0].PercentageSaved != null))
            {
                var productdetail = new ProductDetail();
                productdetail.StoreCode = "AZ";
                productdetail.StoreLogoUrl = StoreUrl;
                productdetail.ProductAttribute = new Productattributes();

                productdetail.ProductAttribute.title = item.ItemAttributes.Title;

                productdetail.ProductAttribute.productUrl = item.DetailPageURL;
                productdetail.ProductAttribute.imageUrls = new Imageurls();
                if (item.LargeImage != null && item.MediumImage!=null &&item.SmallImage!=null)
                {
                    productdetail.ProductAttribute.imageUrls.LargeImage = item.LargeImage?.URL;
                    productdetail.ProductAttribute.imageUrls.MediumImage = item.MediumImage?.URL;
                    productdetail.ProductAttribute.imageUrls.SmallImage = item.SmallImage?.URL;
                }
               else if (item.ImageSets != null)
                {
                    productdetail.ProductAttribute.imageUrls.LargeImage = item.ImageSets[0].LargeImage.URL;
                    productdetail.ProductAttribute.imageUrls.MediumImage = item.ImageSets[0].MediumImage.URL;
                    productdetail.ProductAttribute.imageUrls.SmallImage = item.ImageSets[0].SmallImage.URL;
                }
                productdetail.ProductID = item.ASIN;

                var salePriceFormattedPrice = item.Offers.Offer[0].OfferListing[0].SalePrice?.FormattedPrice;
                if (salePriceFormattedPrice != null)
                    productdetail.ProductAttribute.sellingPrice =
                        salePriceFormattedPrice.Replace("INR", "₹");
                if (salePriceFormattedPrice != null)
                {
                    var priceFormattedPrice = item.Offers.Offer[0].OfferListing[0].Price?.FormattedPrice;
                    if (priceFormattedPrice != null)
                        productdetail.ProductAttribute.maximumRetailPrice =
                            priceFormattedPrice.Replace("INR", "₹");
                }
                else
                {
                    var priceFormattedPrice = item.Offers.Offer[0].OfferListing[0].Price?.FormattedPrice;
                    if (priceFormattedPrice != null)
                        productdetail.ProductAttribute.sellingPrice =
                            priceFormattedPrice.Replace("INR", "₹");
                    var maximumprice = (Convert.ToDouble(item.Offers.Offer[0].OfferListing[0].Price?.Amount) + Convert.ToDouble(item.Offers.Offer[0].OfferListing[0].AmountSaved?.Amount)) / 100;

                    productdetail.ProductAttribute.maximumRetailPrice = "₹" + maximumprice;
                }


                productdetail.ProductAttribute.discountPercentage = item.Offers.Offer[0].OfferListing[0]?.PercentageSaved;
                var amountSaved = item.Offers.Offer[0].OfferListing[0]?.AmountSaved;
                if (amountSaved != null)
                    productdetail.ProductAttribute.savedAmount =
                        (amountSaved?.FormattedPrice).Replace("INR", "₹");
                productlist.Add(productdetail);
            }
            return productlist;
        }

    }
}
