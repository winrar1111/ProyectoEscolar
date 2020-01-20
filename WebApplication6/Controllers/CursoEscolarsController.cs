using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
using WebApplication6.viewModels;

namespace WebApplication6.Controllers
{
    public class CursoEscolarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CursoEscolars
        public ActionResult Index(int pagina = 1)
        {
            if (TempData["ErrorCreation"] != null)
            {
                ViewBag.ErrorCreation = true;
            }
            var cantidadRegistrosPorPagina = 8;
            var cursoEscolars = db.tbCursoEscolar.Include(x=>x.Secciones).Include(x => x.AniosACursar).OrderBy(x => x.Id_Año)
                      .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                      .Take(cantidadRegistrosPorPagina).ToList();

            var totalDeRegistros = db.tbAniosACursar.Count();
            var modelo = new IndexViewModel2();
            modelo.CursoEscolars = cursoEscolars;
            modelo.PaginaActual = pagina;
            modelo.TotalRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            return View(modelo);
        }
        public ActionResult IndexCursoAño(int IdAño)
        {
            var tbCursoEscolar = db.tbCursoEscolar.Include(c => c.AniosACursar).Include(c => c.Secciones).Where(x=>x.Id_Año==IdAño);
             
        
            try
            {
                ViewBag.Anio = tbCursoEscolar.First().AniosACursar.Str_Curso;
            }
            catch (Exception)
            {
                ViewBag.Anio = db.tbAniosACursar.Find(IdAño).Str_Curso;
                return PartialView(tbCursoEscolar.ToList());

            }
            return PartialView(tbCursoEscolar.ToList());
        }
        // GET: CursoEscolars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoEscolar cursoEscolar = db.tbCursoEscolar.Include(c => c.AniosACursar).Include(c => c.Secciones).Where(x=>x.Id_Curso==id).First();
            if (cursoEscolar == null)
            {
                return HttpNotFound();
            }
            return View(cursoEscolar);
        }

        // GET: CursoEscolars/Create
        public ActionResult Create()
        {
            var aulas = db.Secciones;
            ViewBag.Id_Año = new SelectList(db.tbAniosACursar, "Id", "Str_Curso");
            ViewBag.Id_Seccion = new SelectList(aulas, "Id_Seccion", "Str_Seccion");
            return PartialView();
        }

        // POST: CursoEscolars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Curso,Str_Modalidad,Bl_Estado,Id_Año,Id_Seccion")] CursoEscolar cursoEscolar)
        {
            var aux = db.tbCursoEscolar.Where(x => x.Str_Modalidad == cursoEscolar.Str_Modalidad).Where(x=>x.Id_Seccion==cursoEscolar.Id_Seccion);

            if (ModelState.IsValid&&aux.Count()==0)
            {
                string año = db.tbAniosACursar.Find(cursoEscolar.Id_Año).Str_Curso;
                string[] array = { "A", "B", "C", "D", "E","F" };
                var curso = db.tbCursoEscolar.Where(x => x.Id_Año == cursoEscolar.Id_Año).ToList();
                string letras = "A";
                switch (curso.Count+1)
                {
                    case 1:
                        letras = "A";
                        break;
                    case 2:
                        letras = "B";
                        break;
                    case 3:
                        letras = "C";
                        break;
                    case 4:
                        letras = "D";
                        break;
                    case 5:
                        letras = "E";
                        break;
                    case 6:
                        letras = "F";
                        break;
                    default:

                        break;
                }
                cursoEscolar.NombredeCurso = año + " " + letras +" "+cursoEscolar.Str_Modalidad +" "+ DateTime.Now.Year.ToString();
                db.tbCursoEscolar.Add(cursoEscolar);
                db.Secciones.Find(cursoEscolar.Id_Seccion).AulaLLena = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["ErrorCreation"] = true;
            return RedirectToAction("Index");
        }

        // GET: CursoEscolars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoEscolar cursoEscolar = db.tbCursoEscolar.Include(c => c.AniosACursar).Include(c => c.Secciones).Where(x => x.Id_Curso == id).First();
            if (cursoEscolar == null)
            {
                return HttpNotFound();
            }
            var aulas = db.Secciones.Where(x => x.AulaLLena==true);
            ViewBag.Id_Año = new SelectList(db.tbAniosACursar.Where(x => x.Id == cursoEscolar.Id_Año), "Id", "Str_Curso", cursoEscolar.Id_Año);
            ViewBag.Id_Seccion = new SelectList(aulas, "Id_Seccion", "Str_Seccion", cursoEscolar.Id_Seccion);
            return PartialView(cursoEscolar);
        }

        // POST: CursoEscolars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Curso,Str_Modalidad,Bl_Estado,Id_Año,Id_Seccion,NombredeCurso")] CursoEscolar cursoEscolar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursoEscolar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var aulas = db.Secciones.Where(x => x.EstudiantesAula <= db.OpcionesDeColegios.FirstOrDefault().MaximoEstudiantes);
            ViewBag.Id_Año = new SelectList(db.tbAniosACursar.Where(x => x.Id == cursoEscolar.Id_Año), "Id", "Str_Curso", cursoEscolar.Id_Año);
            ViewBag.Id_Seccion = new SelectList(aulas, "Id_Seccion", "Str_Seccion", cursoEscolar.Id_Seccion);
            return View(cursoEscolar);
        }

        // GET: CursoEscolars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoEscolar cursoEscolar = db.tbCursoEscolar.Include(c => c.AniosACursar).Include(c => c.Secciones).Where(x => x.Id_Curso == id).First();
            if (cursoEscolar == null)
            {
                return HttpNotFound();
            }
            return PartialView(cursoEscolar);
        }

        // POST: CursoEscolars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CursoEscolar cursoEscolar = db.tbCursoEscolar.Include(c => c.AniosACursar).Include(c => c.Secciones).Where(x => x.Id_Curso == id).First();
            db.Secciones.Find(cursoEscolar.Id_Seccion).AulaLLena = true;
            db.tbCursoEscolar.Remove(cursoEscolar);
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
