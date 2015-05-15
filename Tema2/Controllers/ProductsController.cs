using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tema2.Models;

namespace Tema2.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        private CartDBContext cart = new CartDBContext();

        private static decimal total = 0;
        // GET: /Products/
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: /Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Title,Stock,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,Stock,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = cart.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            cart.Products.Remove(product);
            cart.SaveChanges();
            total += product.Price;
            //int i = product.ID;
            //product = db.Products.Find(id);
            //product.Stock = product.Stock + 1;
            //db.Products.Remove(product);
            //db.SaveChanges();
            //db.Products.Add(product);
            //db.SaveChanges();
            return RedirectToAction("Cart");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cart()
        {
            return View(cart.Products.ToList());
        }

        public ActionResult AddToCart(int? id)
        {
            Product product = db.Products.Find(id);
            
            product.Stock = product.Stock - 1;
            db.Products.Remove(product);
            db.SaveChanges();
            db.Products.Add(product);
            db.SaveChanges();
            product.Stock = 1;
            cart.Products.Add(product);
            cart.SaveChanges();
            total += product.Price;
            return RedirectToAction("Index");
        }

        public enum ExportTypes { Json, Csv};

        public Exporter CreateExporter(String t)
        {
            Exporter e;
            if (t == "Csv")
            {
                e = new CSVExporter();
                return e;
            }
            if (t == "Json")
            {
                e = new JsonExporter();
                return e;
            }
            return null;
        }

        public ActionResult Export(String t)
        {
            Exporter e;
           
            e = CreateExporter(t);
            e.export(db);
            return RedirectToAction("Index");
        }
    }
}
