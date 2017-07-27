using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace hotoffersguru.Service.APIConfiguration.Flipkart
{
    public interface IFlipkart
    {
        FlipkartOffers AllOffer();
        FlipkartDeals GetDealOfTheDay();
        List<ProductDetail> GetProductsByCatgegory(string categoryName);
        List<ProductDetail> GetProductByKeyword(string searchKeyword, int resultItem = 10);
    }
    public class Flipkart : IFlipkart
    {
        public const string baseUrl = "https://affiliate-api.flipkart.net/affiliate/";
        public const string AffiliatedID = "shopforgirlfr";
        public const string TokenID = "770b29ed2fa240b3978c2a2b744d90f5";

        public List<ProductDetail> GetProductsByCatgegory(string CategoryName)
        {
             HttpClient client = new HttpClient();

            var productDetailList = new List<ProductDetail>();

            try
            {
                var firstOrDefault = getCategoryDetailFlipkart().FirstOrDefault(m => m.CategoryName.Contains(CategoryName));
                if (firstOrDefault != null)
                {
                    var categorydetail = firstOrDefault.CategoryURl.Replace(baseUrl,"");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", AffiliatedID);
                    client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", TokenID);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var productDetailresponse = client.GetAsync(categorydetail).Result;
                    if (productDetailresponse.IsSuccessStatusCode)
                    {
                        var productListresponse = productDetailresponse.Content.ReadAsStringAsync().Result;
                        var productlist = JsonConvert.DeserializeObject<FlipkartProductDetail>(productListresponse);
                        foreach(var product in productlist.productInfoList)
                        {
                            var productdetail = new ProductDetail();
                            Mapper.Map<hotoffersguru.Entity.ServiceEntity.Productattributes, hotoffersguru.Entity.Models.Productattributes>(product.productBaseInfo.productAttributes,productdetail.ProductAttribute);
                            productdetail.ProductCategory.CategoryName = product.productBaseInfo.productIdentifier.categoryPaths.categoryPath[0][0].title;
                            productdetail.ProductID = product.productBaseInfo.productIdentifier.productId;
                     
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                client.Dispose();

            }
            return productDetailList;
        }

        public List<ProductDetail> GetProductByKeyword(string searchKeyword,int resultItem=10)
        {
            HttpClient client = new HttpClient();

            var productDetailList = new List<ProductDetail>();

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", AffiliatedID);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", TokenID);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var productDetailresponse = client.GetAsync("search/json?query="+searchKeyword+"&resultCount="+resultItem).Result;
                if (productDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = productDetailresponse.Content.ReadAsStringAsync().Result;
                    var productlist = JsonConvert.DeserializeObject<FlipkartProductDetail>(productListresponse);


                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                client.Dispose();

            }
            return productDetailList;
        }

        public FlipkartOffers AllOffer()
        {
            HttpClient client = new HttpClient();

            var hotOffersList = new FlipkartOffers();

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", AffiliatedID);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", TokenID);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var productDetailresponse = client.GetAsync("offers/v1/all/json").Result;
                if (productDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = productDetailresponse.Content.ReadAsStringAsync().Result;
                    hotOffersList = JsonConvert.DeserializeObject<FlipkartOffers>(productListresponse);


                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                client.Dispose();

            }
            return hotOffersList;
        }

        public FlipkartDeals GetDealOfTheDay()
        {
            HttpClient client = new HttpClient();

            var productDetailList = new FlipkartDeals();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", AffiliatedID);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", TokenID);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var productDetailresponse = client.GetAsync("offers/v1/dotd/json").Result;
                if (productDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = productDetailresponse.Content.ReadAsStringAsync().Result;
                     productDetailList = JsonConvert.DeserializeObject<FlipkartDeals>(productListresponse);


                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                client.Dispose();

            }
            return productDetailList;
        }

        private List<CategoryDetail> getCategoryDetailFlipkart()
        {
             HttpClient client = new HttpClient();

            var categoryDetaillist = new List<CategoryDetail>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", AffiliatedID);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", TokenID);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var categoryDetailresponse = client.GetAsync("api/shopforgirlfr.json").Result;
                if (categoryDetailresponse.IsSuccessStatusCode)
                {
                    var respose = categoryDetailresponse.Content.ReadAsStringAsync().Result;
                    JObject result = JObject.Parse(respose);
                    foreach (var resultobject in result["apiGroups"]["affiliate"]["apiListings"])
                    {
                        var categoryDetail = new CategoryDetail();
                        var categorylist = resultobject.SingleOrDefault();
                        if (categorylist != null)
                        {
                            categoryDetail.CategoryURl = categorylist["availableVariants"]["v0.1.0"]["get"].ToString();
                            categoryDetail.CategoryName = categorylist["availableVariants"]["v0.1.0"]["resourceName"].ToString().Replace("_", " ");
                        }
                        categoryDetaillist.Add(categoryDetail);

                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return categoryDetaillist;

        }

    }
}
