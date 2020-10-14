using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDBFirstApproach_Udemy.Models;

namespace EFDBFirstApproach_Udemy.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(String search="")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.search = search;
            List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();
            return View(products);
        }

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            catch(Exception e)
            {
                ViewBag.message = e;
                return View(ViewBag.message);
            }
        }

        public ActionResult Update(int id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
           // db.SaveChanges();
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Update(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
            existingProduct.ProductName = p.ProductName;
            existingProduct.Price = p.Price;
            existingProduct.DateOfPurchase = p.DateOfPurchase;
            existingProduct.AvailabilityStatus = p.AvailabilityStatus;
            existingProduct.CategoryID = p.CategoryID;
            existingProduct.BrandID = p.BrandID;
            existingProduct.Active = p.Active;
            db.SaveChanges();
            return RedirectToAction("Index");
                }
        public ActionResult Delete(int id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
           
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product p, int id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(existingProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
   
}