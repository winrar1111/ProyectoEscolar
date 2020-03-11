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
    public class TBANIOS_CURSARController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: TBANIOS_CURSAR
        public ActionResult Index()
        {
            return View(db.TBANIOS_CURSAR.ToList());
        }

        // GET: TBANIOS_CURSAR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBANIOS_CURSAR tBANIOS_CURSAR = db.TBANIOS_CURSAR.Find(id);
            if (tBANIOS_CURSAR == null)
            {
                return HttpNotFound();
            }
            return View(tBANIOS_CURSAR);
        }

        // GET: TBANIOS_CURSAR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBANIOS_CURSAR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CURSO,STR_CURSO")] TBANIOS_CURSAR tBANIOS_CURSAR)
        {
            if (ModelState.IsValid)
            {
                db.TBANIOS_CURSAR.Add(tBANIOS_CURSAR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBANIOS_CURSAR);
        }

        // GET: TBANIOS_CURSAR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBANIOS_CURSAR tBANIOS_CURSAR = db.TBANIOS_CURSAR.Find(id);
            if (tBANIOS_CURSAR == null)
            {
                return HttpNotFound();
            }
            return View(tBANIOS_CURSAR);
        }

        // POST: TBANIOS_CURSAR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CURSO,STR_CURSO")] TBANIOS_CURSAR tBANIOS_CURSAR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBANIOS_CURSAR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBANIOS_CURSAR);
        }

        // GET: TBANIOS_CURSAR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBANIOS_CURSAR tBANIOS_CURSAR = db.TBANIOS_CURSAR.Find(id);
            if (tBANIOS_CURSAR == null)
            {
                return HttpNotFound();
            }
            return View(tBANIOS_CURSAR);
        }

        // POST: TBANIOS_CURSAR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBANIOS_CURSAR tBANIOS_CURSAR = db.TBANIOS_CURSAR.Find(id);
            db.TBANIOS_CURSAR.Remove(tBANIOS_CURSAR);
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
