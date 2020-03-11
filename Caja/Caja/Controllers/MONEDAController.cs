using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caja.Context;

namespace Caja.Controllers
{
    public class MONEDAController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: MONEDA
        public ActionResult Index()
        {
            return View(db.MONEDAs.ToList());
        }

        // GET: MONEDA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONEDA mONEDA = db.MONEDAs.Find(id);
            if (mONEDA == null)
            {
                return HttpNotFound();
            }
            return View(mONEDA);
        }

        // GET: MONEDA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MONEDA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMoneda,nombre")] MONEDA mONEDA)
        {
            if (ModelState.IsValid)
            {
                db.MONEDAs.Add(mONEDA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mONEDA);
        }

        // GET: MONEDA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONEDA mONEDA = db.MONEDAs.Find(id);
            if (mONEDA == null)
            {
                return HttpNotFound();
            }
            return View(mONEDA);
        }

        // POST: MONEDA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMoneda,nombre")] MONEDA mONEDA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mONEDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mONEDA);
        }

        // GET: MONEDA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONEDA mONEDA = db.MONEDAs.Find(id);
            if (mONEDA == null)
            {
                return HttpNotFound();
            }
            return View(mONEDA);
        }

        // POST: MONEDA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MONEDA mONEDA = db.MONEDAs.Find(id);
            db.MONEDAs.Remove(mONEDA);
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
    }
}
