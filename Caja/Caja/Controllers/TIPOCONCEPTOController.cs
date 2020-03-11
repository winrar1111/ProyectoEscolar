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
    public class TIPOCONCEPTOController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: TIPOCONCEPTO
        public ActionResult Index()
        {
            return View(db.TIPOCONCEPTOes.ToList());
        }

        // GET: TIPOCONCEPTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOCONCEPTO tIPOCONCEPTO = db.TIPOCONCEPTOes.Find(id);
            if (tIPOCONCEPTO == null)
            {
                return HttpNotFound();
            }
            return View(tIPOCONCEPTO);
        }

        // GET: TIPOCONCEPTO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOCONCEPTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConcepto,nombreConcepto")] TIPOCONCEPTO tIPOCONCEPTO)
        {
            if (ModelState.IsValid)
            {
                db.TIPOCONCEPTOes.Add(tIPOCONCEPTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOCONCEPTO);
        }

        // GET: TIPOCONCEPTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOCONCEPTO tIPOCONCEPTO = db.TIPOCONCEPTOes.Find(id);
            if (tIPOCONCEPTO == null)
            {
                return HttpNotFound();
            }
            return View(tIPOCONCEPTO);
        }

        // POST: TIPOCONCEPTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConcepto,nombreConcepto")] TIPOCONCEPTO tIPOCONCEPTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOCONCEPTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOCONCEPTO);
        }

        // GET: TIPOCONCEPTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOCONCEPTO tIPOCONCEPTO = db.TIPOCONCEPTOes.Find(id);
            if (tIPOCONCEPTO == null)
            {
                return HttpNotFound();
            }
            return View(tIPOCONCEPTO);
        }

        // POST: TIPOCONCEPTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPOCONCEPTO tIPOCONCEPTO = db.TIPOCONCEPTOes.Find(id);
            db.TIPOCONCEPTOes.Remove(tIPOCONCEPTO);
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
