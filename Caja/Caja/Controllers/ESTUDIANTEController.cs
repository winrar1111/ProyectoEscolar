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
    public class ESTUDIANTEController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: ESTUDIANTE
        public ActionResult Index()
        {
            var tBESTUDIANTEs = db.TBESTUDIANTEs.Include(t => t.TBANIOS_CURSAR);
            return View(tBESTUDIANTEs.ToList());
        }

        // GET: ESTUDIANTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBESTUDIANTE tBESTUDIANTE = db.TBESTUDIANTEs.Find(id);
            if (tBESTUDIANTE == null)
            {
                return HttpNotFound();
            }
            return View(tBESTUDIANTE);
        }

        // GET: ESTUDIANTE/Create
        public ActionResult Create()
        {
            ViewBag.numAÑO_EN_CURSO = new SelectList(db.TBANIOS_CURSAR, "ID_CURSO", "STR_CURSO");
            return View();
        }

        // POST: ESTUDIANTE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estudiante,STR_NOMBRE,STR_APELLIDO,STR_DIRECCION,STR_CORREO,DT_FECHA_NAC,STR_DOMICILIO,STR_TELEFONO,STR_GENERO,CodESTUDIANTE,COD_MINED_ESTUDIANTE,STR_NOMBRE_PADRE,STR_NOMBRE_MADRE,numAÑO_EN_CURSO")] TBESTUDIANTE tBESTUDIANTE)
        {
            if (ModelState.IsValid)
            {
                db.TBESTUDIANTEs.Add(tBESTUDIANTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.numAÑO_EN_CURSO = new SelectList(db.TBANIOS_CURSAR, "ID_CURSO", "STR_CURSO", tBESTUDIANTE.numAÑO_EN_CURSO);
            return View(tBESTUDIANTE);
        }

        // GET: ESTUDIANTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBESTUDIANTE tBESTUDIANTE = db.TBESTUDIANTEs.Find(id);
            if (tBESTUDIANTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.numAÑO_EN_CURSO = new SelectList(db.TBANIOS_CURSAR, "ID_CURSO", "STR_CURSO", tBESTUDIANTE.numAÑO_EN_CURSO);
            return View(tBESTUDIANTE);
        }

        // POST: ESTUDIANTE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estudiante,STR_NOMBRE,STR_APELLIDO,STR_DIRECCION,STR_CORREO,DT_FECHA_NAC,STR_DOMICILIO,STR_TELEFONO,STR_GENERO,CodESTUDIANTE,COD_MINED_ESTUDIANTE,STR_NOMBRE_PADRE,STR_NOMBRE_MADRE,numAÑO_EN_CURSO")] TBESTUDIANTE tBESTUDIANTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBESTUDIANTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.numAÑO_EN_CURSO = new SelectList(db.TBANIOS_CURSAR, "ID_CURSO", "STR_CURSO", tBESTUDIANTE.numAÑO_EN_CURSO);
            return View(tBESTUDIANTE);
        }

        // GET: ESTUDIANTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBESTUDIANTE tBESTUDIANTE = db.TBESTUDIANTEs.Find(id);
            if (tBESTUDIANTE == null)
            {
                return HttpNotFound();
            }
            return View(tBESTUDIANTE);
        }

        // POST: ESTUDIANTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBESTUDIANTE tBESTUDIANTE = db.TBESTUDIANTEs.Find(id);
            db.TBESTUDIANTEs.Remove(tBESTUDIANTE);
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
