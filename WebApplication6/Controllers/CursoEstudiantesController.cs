using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class CursoEstudiantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static int idaux = 0;
        // GET: CursoEstudiantes
        public async Task<ActionResult> Index(int id=0)
        {
            if (id!=0)
            {
                idaux = id;
                var cursoEstudiantes = db.CursoEstudiantes.Include(c => c.CursoEscolar).Include(c => c.Estudiantes).Where(x => x.Id_Estudiante == id);
                return View(await cursoEstudiantes.ToListAsync());
            }
            return View();
        }
        public ActionResult IndexCurso(int IdCurso = 0)
        {
            var cursoEstudiantes = db.CursoEstudiantes.Include(c => c.CursoEscolar).Include(c => c.Estudiantes).Where(x=>x.Id_Curso==IdCurso);
            return PartialView("IndexCurso",  cursoEstudiantes.ToList());
        }
            // GET: CursoEstudiantes/Details/5
            public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoEstudiantes cursoEstudiantes = await db.CursoEstudiantes.FindAsync(id);
            if (cursoEstudiantes == null)
            {
                return HttpNotFound();
            }
            return View(cursoEstudiantes);
        }

        // GET: CursoEstudiantes/Create
        public ActionResult Create()
        {
            int idaño = db.tbestudiantes.Find(idaux).Id;
            var Cursos = db.tbCursoEscolar.Where(x => x.Id_Año == idaño).ToList();
    
            ViewBag.Id_Curso = new SelectList(Cursos, "Id_Curso", "NombredeCurso");
            if (Cursos.Count==0)
            {
                ViewBag.NoCurso = true;
            }
            return View();
        }

        // POST: CursoEstudiantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Id_Curso,Id_Estudiante")] CursoEstudiantes cursoEstudiantes)
        {
            if (ModelState.IsValid)
            {
                var aux = db.tbCursoEscolar.Find(cursoEstudiantes.Id_Curso);
                Secciones seccion= db.Secciones.Find(aux.Id_Seccion);
                seccion.EstudiantesAula += 1;
                if (seccion.EstudiantesAula<=db.OpcionesDeColegios.FirstOrDefault().MaximoEstudiantes)
                {
                    db.Entry(seccion).State = EntityState.Modified;
                    cursoEstudiantes.Id_Estudiante = idaux;
                    db.CursoEstudiantes.Add(cursoEstudiantes);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details", "Estudiantes", new { id = idaux });
                }
                else
                {

                    ViewBag.ErrorPorLLeno = true;
                }

            }

            ViewBag.Id_Curso = new SelectList(db.tbCursoEscolar, "Id_Curso", "Str_Modalidad", cursoEstudiantes.Id_Curso);
            return View(cursoEstudiantes);
        }

        // GET: CursoEstudiantes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoEstudiantes cursoEstudiantes = await db.CursoEstudiantes.FindAsync(id);
            if (cursoEstudiantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Curso = new SelectList(db.tbCursoEscolar, "Id_Curso", "Str_Modalidad", cursoEstudiantes.Id_Curso);
            ViewBag.Id_Estudiante = new SelectList(db.tbestudiantes, "Id_Estudiante", "Nombre", cursoEstudiantes.Id_Estudiante);
            return View(cursoEstudiantes);
        }

        // POST: CursoEstudiantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Id_Curso,Id_Estudiante")] CursoEstudiantes cursoEstudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursoEstudiantes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Curso = new SelectList(db.tbCursoEscolar, "Id_Curso", "Str_Modalidad", cursoEstudiantes.Id_Curso);
            ViewBag.Id_Estudiante = new SelectList(db.tbestudiantes, "Id_Estudiante", "Nombre", cursoEstudiantes.Id_Estudiante);
            return View(cursoEstudiantes);
        }

        // GET: CursoEstudiantes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoEstudiantes cursoEstudiantes = await db.CursoEstudiantes.FindAsync(id);
            if (cursoEstudiantes == null)
            {
                return HttpNotFound();
            }
            return View(cursoEstudiantes);
        }

        // POST: CursoEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CursoEstudiantes cursoEstudiantes = await db.CursoEstudiantes.FindAsync(id);
            var aux = db.tbCursoEscolar.Find(cursoEstudiantes.Id_Curso);
            Secciones seccion = db.Secciones.Find(aux.Id_Seccion);
            seccion.EstudiantesAula -= 1;
            db.Entry(seccion).State = EntityState.Modified;
            db.CursoEstudiantes.Remove(cursoEstudiantes);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Estudiantes", new { id = idaux });
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
