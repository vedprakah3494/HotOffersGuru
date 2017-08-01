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
using System.Web.Configuration;
using System.Configuration;
using System.Web.Caching;
using hotoffersguru.Entity.ServiceEntity.Flipkart;

namespace hotoffersguru.Service.APIConfiguration.Flipkart
{
    public interface IFlipkart
    {
        List<ProductDetail> AllOffer();
        List<ProductDetail> GetProductsByCatgegory(string categoryName);
        List<ProductDetail> GetProductByKeyword(List<string> searchKeyword, int resultItem = 10);
    }
    public class Flipkart : IFlipkart
    {
        public const string baseUrl = "https://affiliate-api.flipkart.net/affiliate/";
        public const string AffiliatedID = "shopforgirlfr";
        public const string TokenID = "770b29ed2fa240b3978c2a2b744d90f5";
        private static string StoreUrl = "~/Areas/Portal/Content/StoreLogo/fklogo.png";
        private string hotoffercategory = "Mobile phones,Head Phones,Women Fashion,Men Jeans,Men T-shirt";//ConfigurationManager.AppSettings["hotoffercategory"].ToString();

        public List<ProductDetail> GetProductsByCatgegory(string CategoryName)
        {
             HttpClient client = new HttpClient();

            var productDetailList = new List<ProductDetail>();
            var allcategory = getCategoryDetailFlipkart();
            try
            {
                var firstOrDefault = allcategory.FirstOrDefault(m => m.CategoryName.Contains(CategoryName));
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

        public List<ProductDetail> GetProductByKeyword(List<string>searchKeyword,int resultItem=10)
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

                foreach (var keyword in searchKeyword)
                {
                    var productDetailresponse = client
                        .GetAsync("1.0/search.json?query=" + keyword + "&resultCount=" + resultItem).Result;
                    if (productDetailresponse.IsSuccessStatusCode)
                    {
                        var productListresponse = productDetailresponse.Content.ReadAsStringAsync().Result;
                        var productlist = JsonConvert.DeserializeObject<KeywordSearchResult>(productListresponse);
                        foreach (var pro in productlist.productInfoList.Where(x => x.productBaseInfoV1.imageUrls != null))
                        {
                            var productdetail = new ProductDetail();
                            productdetail.ProductID = pro.productBaseInfoV1.productId;
                            productdetail.StoreCode = "FK";
                            productdetail.StoreLogoUrl = StoreUrl;
                            productdetail.ProductAttribute=new Entity.Models.Productattributes();
                            productdetail.ProductAttribute.imageUrls=new Entity.Models.Imageurls();
                            if (pro.productBaseInfoV1.imageUrls._800x800 != null)
                            {
                                productdetail.ProductAttribute.imageUrls.LargeImage =
                                    pro.productBaseInfoV1.imageUrls._800x800;
                            }
                            if (pro.productBaseInfoV1.imageUrls._400x400 != null)
                            {
                                productdetail.ProductAttribute.imageUrls.MediumImage =
                                    pro.productBaseInfoV1.imageUrls?._400x400;
                            }
                            productdetail.ProductAttribute.maximumRetailPrice =
                                "₹" + pro.productBaseInfoV1.maximumRetailPrice.amount;
                            productdetail.ProductAttribute.sellingPrice = "₹" + pro.productBaseInfoV1.flipkartSellingPrice.amount;
                            productdetail.ProductAttribute.discountPercentage =pro.productBaseInfoV1.discountPercentage.ToString();
                            productdetail.ProductAttribute.savedAmount =(pro.productBaseInfoV1.maximumRetailPrice.amount -pro.productBaseInfoV1.flipkartSellingPrice.amount).ToString();
                            productdetail.ProductAttribute.productUrl =
                                pro.productBaseInfoV1.productUrl;
                            productdetail.ProductAttribute.title = pro.productBaseInfoV1.title;
                            productDetailList.Add(productdetail);
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

        public List<ProductDetail> AllOffer()
        {
            var productlist = new List<ProductDetail>();
            List<string> hotoffercategorylist = hotoffercategory.Split(',').ToList();
            productlist.AddRange(GetProductByKeyword(hotoffercategorylist));                     
            
            return productlist;
        }

        private FlipkartOffers AllOfferbyFlipkart()
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

        private FlipkartDeals GetDealOfTheDay()
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
