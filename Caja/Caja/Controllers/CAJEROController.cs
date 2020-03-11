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
    public class CAJEROController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: CAJERO
        public ActionResult Index()
        {
            return View(db.CAJEROes.ToList());
        }

        // GET: CAJERO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAJERO cAJERO = db.CAJEROes.Find(id);
            if (cAJERO == null)
            {
                return HttpNotFound();
            }
            return View(cAJERO);
        }

        // GET: CAJERO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CAJERO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCajero,numCajero,nombre,apellido")] CAJERO cAJERO)
        {
            if (ModelState.IsValid)
            {
                db.CAJEROes.Add(cAJERO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cAJERO);
        }

        // GET: CAJERO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAJERO cAJERO = db.CAJEROes.Find(id);
            if (cAJERO == null)
            {
                return HttpNotFound();
            }
            return View(cAJERO);
        }

        // POST: CAJERO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCajero,numCajero,nombre,apellido")] CAJERO cAJERO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAJERO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cAJERO);
        }

        // GET: CAJERO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAJERO cAJERO = db.CAJEROes.Find(id);
            if (cAJERO == null)
            {
                return HttpNotFound();
            }
            return View(cAJERO);
        }

        // POST: CAJERO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAJERO cAJERO = db.CAJEROes.Find(id);
            db.CAJEROes.Remove(cAJERO);
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
