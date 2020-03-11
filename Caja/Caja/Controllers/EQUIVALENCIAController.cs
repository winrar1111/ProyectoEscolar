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
    public class EQUIVALENCIAController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: EQUIVALENCIA
        public ActionResult Index()
        {
            return View(db.EQUIVALENCIAs.ToList());
        }

        // GET: EQUIVALENCIA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUIVALENCIA eQUIVALENCIA = db.EQUIVALENCIAs.Find(id);
            if (eQUIVALENCIA == null)
            {
                return HttpNotFound();
            }
            return View(eQUIVALENCIA);
        }

        // GET: EQUIVALENCIA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EQUIVALENCIA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEquivalencia,monto,fecha")] EQUIVALENCIA eQUIVALENCIA)
        {
            if (ModelState.IsValid)
            {
                db.EQUIVALENCIAs.Add(eQUIVALENCIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eQUIVALENCIA);
        }

        // GET: EQUIVALENCIA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUIVALENCIA eQUIVALENCIA = db.EQUIVALENCIAs.Find(id);
            if (eQUIVALENCIA == null)
            {
                return HttpNotFound();
            }
            return View(eQUIVALENCIA);
        }

        // POST: EQUIVALENCIA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEquivalencia,monto,fecha")] EQUIVALENCIA eQUIVALENCIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eQUIVALENCIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eQUIVALENCIA);
        }

        // GET: EQUIVALENCIA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUIVALENCIA eQUIVALENCIA = db.EQUIVALENCIAs.Find(id);
            if (eQUIVALENCIA == null)
            {
                return HttpNotFound();
            }
            return View(eQUIVALENCIA);
        }

        // POST: EQUIVALENCIA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EQUIVALENCIA eQUIVALENCIA = db.EQUIVALENCIAs.Find(id);
            db.EQUIVALENCIAs.Remove(eQUIVALENCIA);
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
