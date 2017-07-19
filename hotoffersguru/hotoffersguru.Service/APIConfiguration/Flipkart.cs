using hotoffersguru.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using hotoffersguru.Entity.Models;
using hotoffersguru.Entity.ServiceEntity;
using AutoMapper;

namespace hotoffersguru.Service.APIConfiguration
{
    public class Flipkart : IFlipkart
    {
        public const string baseUrl = "https://affiliate-api.flipkart.net/affiliate/";
        public const string AffiliatedID = "shopforgirlfr";
        public const string TokenID = "770b29ed2fa240b3978c2a2b744d90f5";

        public List<ProductDetail> getProductDetailFlipkart(string CategoryName)
        {
             HttpClient client = new HttpClient();

            var productDetailList = new List<ProductDetail>();

            try
            {
                var categorydetail = getCategoryDetailFlipkart().Where(m => m.CategoryName.Contains(CategoryName)).FirstOrDefault().CategoryURl.Replace(baseUrl,"");
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Id", AffiliatedID);
                client.DefaultRequestHeaders.Add("Fk-Affiliate-Token", TokenID);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ProductDetailresponse = client.GetAsync(categorydetail).Result;
                if (ProductDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = ProductDetailresponse.Content.ReadAsStringAsync().Result;
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
            catch (Exception ex)
            {
            }
            finally
            {
                client.Dispose();

            }
            return productDetailList;
        }

        public List<ProductDetail> SearchProductDetailFlipkart(string SearchKeyword,int resultItem=10)
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

                var ProductDetailresponse = client.GetAsync("search/json?query="+SearchKeyword+"&resultCount="+resultItem).Result;
                if (ProductDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = ProductDetailresponse.Content.ReadAsStringAsync().Result;
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

        public List<ProductDetail> AllOffer()
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

                var ProductDetailresponse = client.GetAsync("offers/v1/all/json").Result;
                if (ProductDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = ProductDetailresponse.Content.ReadAsStringAsync().Result;
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

        public List<ProductDetail> DealOfTheDay()
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

                var ProductDetailresponse = client.GetAsync("offers/v1/dotd/json").Result;
                if (ProductDetailresponse.IsSuccessStatusCode)
                {
                    var productListresponse = ProductDetailresponse.Content.ReadAsStringAsync().Result;
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
                        var abc = resultobject.SingleOrDefault();
                        categoryDetail.CategoryURl = abc["availableVariants"]["v0.1.0"]["get"].ToString();
                        categoryDetail.CategoryName = abc["availableVariants"]["v0.1.0"]["resourceName"].ToString().Replace("_", " ");
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
