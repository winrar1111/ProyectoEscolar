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
    public class CONCEPTOSPAGOController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: CONCEPTOSPAGO
        public ActionResult Index()
        {
            return View(db.CONCEPTOSPAGOes.ToList());
        }

        // GET: CONCEPTOSPAGO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONCEPTOSPAGO cONCEPTOSPAGO = db.CONCEPTOSPAGOes.Find(id);
            if (cONCEPTOSPAGO == null)
            {
                return HttpNotFound();
            }
            return View(cONCEPTOSPAGO);
        }

        // GET: CONCEPTOSPAGO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CONCEPTOSPAGO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConcepto,nombreConcepto,monto,mes,ano")] CONCEPTOSPAGO cONCEPTOSPAGO)
        {
            if (ModelState.IsValid)
            {
                db.CONCEPTOSPAGOes.Add(cONCEPTOSPAGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cONCEPTOSPAGO);
        }

        // GET: CONCEPTOSPAGO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONCEPTOSPAGO cONCEPTOSPAGO = db.CONCEPTOSPAGOes.Find(id);
            if (cONCEPTOSPAGO == null)
            {
                return HttpNotFound();
            }
            return View(cONCEPTOSPAGO);
        }

        // POST: CONCEPTOSPAGO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConcepto,nombreConcepto,monto,mes,ano")] CONCEPTOSPAGO cONCEPTOSPAGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONCEPTOSPAGO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONCEPTOSPAGO);
        }

        // GET: CONCEPTOSPAGO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONCEPTOSPAGO cONCEPTOSPAGO = db.CONCEPTOSPAGOes.Find(id);
            if (cONCEPTOSPAGO == null)
            {
                return HttpNotFound();
            }
            return View(cONCEPTOSPAGO);
        }

        // POST: CONCEPTOSPAGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONCEPTOSPAGO cONCEPTOSPAGO = db.CONCEPTOSPAGOes.Find(id);
            db.CONCEPTOSPAGOes.Remove(cONCEPTOSPAGO);
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
