using hotoffersguru.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotoffersguru.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Portal/Home
        public ActionResult Index()
        {
            ProductService productService = new ProductService();
            productService.GetProductDetail();
            productService.GetProductDetailByKeword();
            return View();
        }

        // GET: Portal/Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Portal/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portal/Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Portal/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Portal/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Portal/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Portal/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
