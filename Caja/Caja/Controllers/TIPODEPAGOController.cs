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
    public class TIPODEPAGOController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: TIPODEPAGO
        public ActionResult Index()
        {
            return View(db.TIPODEPAGOes.ToList());
        }

        // GET: TIPODEPAGO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPODEPAGO tIPODEPAGO = db.TIPODEPAGOes.Find(id);
            if (tIPODEPAGO == null)
            {
                return HttpNotFound();
            }
            return View(tIPODEPAGO);
        }

        // GET: TIPODEPAGO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPODEPAGO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipo,nombre")] TIPODEPAGO tIPODEPAGO)
        {
            if (ModelState.IsValid)
            {
                db.TIPODEPAGOes.Add(tIPODEPAGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPODEPAGO);
        }

        // GET: TIPODEPAGO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPODEPAGO tIPODEPAGO = db.TIPODEPAGOes.Find(id);
            if (tIPODEPAGO == null)
            {
                return HttpNotFound();
            }
            return View(tIPODEPAGO);
        }

        // POST: TIPODEPAGO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipo,nombre")] TIPODEPAGO tIPODEPAGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPODEPAGO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPODEPAGO);
        }

        // GET: TIPODEPAGO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPODEPAGO tIPODEPAGO = db.TIPODEPAGOes.Find(id);
            if (tIPODEPAGO == null)
            {
                return HttpNotFound();
            }
            return View(tIPODEPAGO);
        }

        // POST: TIPODEPAGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPODEPAGO tIPODEPAGO = db.TIPODEPAGOes.Find(id);
            db.TIPODEPAGOes.Remove(tIPODEPAGO);
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
