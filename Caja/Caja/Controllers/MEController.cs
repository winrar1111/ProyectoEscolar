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
    public class MEController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: ME
        public ActionResult Index()
        {
            return View(db.MES.ToList());
        }

        // GET: ME/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ME mE = db.MES.Find(id);
            if (mE == null)
            {
                return HttpNotFound();
            }
            return View(mE);
        }

        // GET: ME/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ME/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMes,nombreMes")] ME mE)
        {
            if (ModelState.IsValid)
            {
                db.MES.Add(mE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mE);
        }

        // GET: ME/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ME mE = db.MES.Find(id);
            if (mE == null)
            {
                return HttpNotFound();
            }
            return View(mE);
        }

        // POST: ME/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMes,nombreMes")] ME mE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mE);
        }

        // GET: ME/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ME mE = db.MES.Find(id);
            if (mE == null)
            {
                return HttpNotFound();
            }
            return View(mE);
        }

        // POST: ME/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ME mE = db.MES.Find(id);
            db.MES.Remove(mE);
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
