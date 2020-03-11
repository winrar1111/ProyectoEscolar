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
    public class CAJAController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: CAJA
        public ActionResult Index()
        {
            return View(db.CAJAs.ToList());
        }

        // GET: CAJA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAJA cAJA = db.CAJAs.Find(id);
            if (cAJA == null)
            {
                return HttpNotFound();
            }
            return View(cAJA);
        }

        // GET: CAJA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CAJA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCaja,numCaja")] CAJA cAJA)
        {
            if (ModelState.IsValid)
            {
                db.CAJAs.Add(cAJA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cAJA);
        }

        // GET: CAJA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAJA cAJA = db.CAJAs.Find(id);
            if (cAJA == null)
            {
                return HttpNotFound();
            }
            return View(cAJA);
        }

        // POST: CAJA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCaja,numCaja")] CAJA cAJA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAJA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cAJA);
        }

        // GET: CAJA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAJA cAJA = db.CAJAs.Find(id);
            if (cAJA == null)
            {
                return HttpNotFound();
            }
            return View(cAJA);
        }

        // POST: CAJA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAJA cAJA = db.CAJAs.Find(id);
            db.CAJAs.Remove(cAJA);
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
