using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaguaroFinal.DAL;
using SaguaroFinal.Models;
using System.IO;

namespace SaguaroFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private SaguaroContext db = new SaguaroContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
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

        // GET: Products/Create
        public ActionResult Create()
        {
            List<Category> categories = new List<Category>();
            categories = db.Categories.ToList();

            ViewBag.categories = categories;

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Category.Id,Image")] Product product)
        {
            Category categoria = db.Categories.Find(int.Parse(HttpContext.Request.Form["Category.Id"]));
            product.Category = categoria;
        ModelState["category"].Errors.Clear();
        if (ModelState.IsValid)
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"),
                                                   Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        product.Image = path;
                    }
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Category> categories = new List<Category>();
            categories = db.Categories.ToList();

            ViewBag.categories = categories;

            return View(product);
        }
        // GET: Products/Edit/5
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

            List<Category> categories = new List<Category>();
            categories = db.Categories.ToList();

            ViewBag.categories = categories;

            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price, Category,Image")] Product product)
        {
           //if (ModelState.IsValid)
            {
                Category categoria = db.Categories.Find(int.Parse(HttpContext.Request.Form["Category"]));
                product.Category = categoria;
                product.CategoryId = categoria.Id;
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"),
                                                   Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        product.Image = path;
                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Category> categories = new List<Category>();
            categories = db.Categories.ToList();

            ViewBag.categories = categories;

            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AllowAnonymous]
        public ActionResult Clothes(int? categoryId)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category(0, "Todas"));
            categories.AddRange(db.Categories.ToList());

            ViewBag.categoriesList = new SelectList(categories, "Id", "Name");

            if(categoryId == null || categoryId == 0)
            {
                ViewBag.categories = db.Categories.ToList();
            }
            else
            {
                ViewBag.categories = db.Categories.Where(c => c.Id == categoryId).ToList();
            }

            return View();
        }
    }
}
