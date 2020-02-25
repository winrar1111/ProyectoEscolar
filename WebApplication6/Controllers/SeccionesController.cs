using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class SeccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Secciones
        public ActionResult Index()
        {
            ViewBag.SeccionMax =0+ db.OpcionesDeColegios.FirstOrDefault().MaximoEstudiantes;
            return View(db.Secciones.ToList());
        }

        // GET: Secciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = db.Secciones.Find(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }

        // GET: Secciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Seccion,Str_Seccion,Bl_Estado")] Secciones secciones)
        {
            if (ModelState.IsValid)
            {
                secciones.EstudiantesAula = 0;
                secciones.AulaLLena = true;
                db.Secciones.Add(secciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secciones);
        }

        // GET: Secciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = db.Secciones.Find(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }

        // POST: Secciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Seccion,Str_Seccion,Bl_Estado")] Secciones secciones)
        {
            if (ModelState.IsValid)
            {
                secciones.AulaLLena = db.Secciones.Find(secciones.Id_Seccion).AulaLLena;
                secciones.EstudiantesAula = db.Secciones.Find(secciones.Id_Seccion).EstudiantesAula;
                db.Entry(secciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secciones);
        }

        // GET: Secciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = db.Secciones.Find(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }

        // POST: Secciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Secciones secciones = db.Secciones.Find(id);
            db.Secciones.Remove(secciones);
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
