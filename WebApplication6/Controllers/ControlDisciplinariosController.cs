using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WebApplication6.Models;
using WebApplication6.viewModels;


namespace WebApplication6.Controllers
{
    public class ControlDisciplinariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static int auxid = 0;

        // GET: ControlDisciplinarios
        public ActionResult Index(string MesFiltro, string AnioFiltro,int id, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 8;
            auxid = id;
            ViewBag.id = id;
            var usuario = db.Users.Find(User.Identity.GetUserId());
            bool respues = Validador.PuedeEntrar(usuario.Id,"Ver Historial Diciplinario");

            if (respues == true)
            {
                if (TempData["SuccessDelete"] != null)
                {
                    ViewBag.SuccessDelete = true;
                }
                else if (TempData["SuccessCreate"] != null)
                {
                    ViewBag.SuccessCreate = true;
                }
                else if (TempData["SuccessEdit"] != null)
                {
                    ViewBag.SuccessEdit = true;
                }

                if (ViewBag.ErrorFiltro == false || ViewBag.ErrorFiltro == null)
                {
                    int mes = 0;
                    if (!string.IsNullOrEmpty(MesFiltro))
                    {
                        mes = Convert.ToInt32(MesFiltro);
                    }

                    var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).WhereIf(id != 0, x => x.Id_Estudiantes == id)
                      .WhereIf(mes!=0,x=>x.Fecha_Emision.Month==mes)
                      .WhereIf(!string.IsNullOrEmpty(AnioFiltro),x=>x.Fecha_Emision.Year.ToString()==AnioFiltro)
                     .OrderByDescending(x => x.Fecha_Emision)
                     .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                     .Take(cantidadRegistrosPorPagina).ToList();

                    var totalDeRegistros = db.tbcontrolDisciplinario.WhereIf(!string.IsNullOrEmpty(id.ToString()), x => x.Id_Estudiantes == id)
                      .WhereIf(!string.IsNullOrEmpty(MesFiltro), x => x.Fecha_Emision.Month == mes)
                      .WhereIf(!string.IsNullOrEmpty(AnioFiltro), x => x.Fecha_Emision.Year.ToString() == AnioFiltro).Count();
                    var modelo = new IndexViewModel2();
                    modelo.controlDisciplinarios = ListaDisciplinaria;
                    modelo.PaginaActual = pagina;
                    modelo.TotalRegistros = totalDeRegistros;
                    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                    if (!string.IsNullOrEmpty(id.ToString())&&id!=0)
                    {
                        ViewBag.ModeloEstudiante = true;
                        ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                    }

                    return View(modelo);
                    /*
                    //FILTROS CON ESTUDIANTES
                    if (id != 0)
                    {
                        //FILTRO DE SOLO ESTUDIANTE
                        if (AnioFiltro == null && MesFiltro == null)
                        {
                            //filtro de solo estudiante!
                            var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).WhereIf(!string.IsNullOrEmpty(id.ToString()),x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                            .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                             .Take(cantidadRegistrosPorPagina).ToList();

                            var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Id_Estudiantes == id).Count();
                            var modelo = new IndexViewModel2();
                            modelo.controlDisciplinarios = ListaDisciplinaria;
                            modelo.PaginaActual = pagina;
                            modelo.TotalRegistros = totalDeRegistros;
                            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                            ViewBag.ModeloEstudiante = true;
                            ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                            return View(modelo);
                        }
                        //FILTROS PARA AÑOS
                        else if (AnioFiltro != "-- Años --")
                        {
                            if (MesFiltro != "0")
                            {
                                //filtros con año y mes y estudiante
                                var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro).
                                Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                .Take(cantidadRegistrosPorPagina).ToList();
                                //si falla la busquea reseatea los datos
                                if (ListaDisciplinaria.Count == 0)
                                {
                                    ViewBag.ErrorFiltro = true;
                                    var ListaDisciplinaria2 = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado)
                                  .Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                                  .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                  .Take(cantidadRegistrosPorPagina).ToList();

                                    var totalDeRegistros2 = db.tbcontrolDisciplinario
                                    .Where(x => x.Id_Estudiantes == id).Count();
                                    var modelo2 = new IndexViewModel2();
                                    modelo2.controlDisciplinarios = ListaDisciplinaria;
                                    modelo2.PaginaActual = pagina;
                                    modelo2.TotalRegistros = totalDeRegistros2;
                                    modelo2.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                    ViewBag.ModeloEstudiante = true;
                                    ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                                    return View(modelo2);
                                }
                                var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro)
                                .Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).Where(x => x.Id_Estudiantes == id).Count();
                                var modelo = new IndexViewModel2();
                                modelo.controlDisciplinarios = ListaDisciplinaria;
                                modelo.PaginaActual = pagina;
                                modelo.TotalRegistros = totalDeRegistros;
                                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                ViewBag.ModeloEstudiante = true;
                                ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                                return View(modelo);
                            }
                            else
                            {
                                //solo filtros de años y estudiante
                                var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro)
                                .Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                .Take(cantidadRegistrosPorPagina).ToList();
                                // SI FALLA
                                if (ListaDisciplinaria.Count == 0)
                                {
                                    ViewBag.ErrorFiltro = true;
                                    var ListaDisciplinaria2 = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado)
                                  .Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                                  .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                  .Take(cantidadRegistrosPorPagina).ToList();

                                    var totalDeRegistros2 = db.tbcontrolDisciplinario
                                    .Where(x => x.Id_Estudiantes == id).Count();
                                    var modelo2 = new IndexViewModel2();
                                    modelo2.controlDisciplinarios = ListaDisciplinaria;
                                    modelo2.PaginaActual = pagina;
                                    modelo2.TotalRegistros = totalDeRegistros2;
                                    modelo2.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                    ViewBag.ModeloEstudiante = true;
                                    ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                                    return View(modelo2);
                                }
                                var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro)
                                    .Where(x => x.Id_Estudiantes == id).Count();
                                var modelo = new IndexViewModel2();
                                modelo.controlDisciplinarios = ListaDisciplinaria;
                                modelo.PaginaActual = pagina;
                                modelo.TotalRegistros = totalDeRegistros;
                                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                ViewBag.ModeloEstudiante = true;
                                ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                                return View(modelo);
                            }

                        }
                        //FILTROS PARA MES
                        else if (MesFiltro != "0")
                        {
                            //FILTRO DE MES Y ESTUDIANTE
                            var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).
                                    Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                    .Take(cantidadRegistrosPorPagina).ToList();
                            //si falla la busquea reseatea los datos
                            if (ListaDisciplinaria.Count == 0)
                            {
                                ViewBag.ErrorFiltro = true;
                                var ListaDisciplinaria2 = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado)
                                  .Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                                  .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                  .Take(cantidadRegistrosPorPagina).ToList();

                                var totalDeRegistros2 = db.tbcontrolDisciplinario
                                .Where(x => x.Id_Estudiantes == id).Count();
                                var modelo2 = new IndexViewModel2();
                                modelo2.controlDisciplinarios = ListaDisciplinaria;
                                modelo2.PaginaActual = pagina;
                                modelo2.TotalRegistros = totalDeRegistros2;
                                modelo2.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                ViewBag.ModeloEstudiante = true;
                                ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                                return View(modelo2);
                            }
                            var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro)
                            .Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).Where(x => x.Id_Estudiantes == id).Count();
                            var modelo = new IndexViewModel2();
                            modelo.controlDisciplinarios = ListaDisciplinaria;
                            modelo.PaginaActual = pagina;
                            modelo.TotalRegistros = totalDeRegistros;
                            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                            ViewBag.ModeloEstudiante = true;
                            ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                            return View(modelo);
                        }
                        //ULTIMO FILTRO SI TODO FALLA
                        else
                        {
                            //filtro de solo estudiante
                            var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).Where(x => x.Id_Estudiantes == id).OrderByDescending(x => x.Fecha_Emision)
                            .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                             .Take(cantidadRegistrosPorPagina).ToList();

                            var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Id_Estudiantes == id).Count();
                            var modelo = new IndexViewModel2();
                            modelo.controlDisciplinarios = ListaDisciplinaria;
                            modelo.PaginaActual = pagina;
                            modelo.TotalRegistros = totalDeRegistros;
                            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                            ViewBag.ModeloEstudiante = true;
                            ViewBag.name = db.tbestudiantes.Find(id).Nombre + " " + db.tbestudiantes.Find(id).Apellido;
                            return View(modelo);
                        }
                    }
                    //SIN FILTRO
                    else if (id == 0 && AnioFiltro == null || AnioFiltro == "-- Años --" && MesFiltro == "0")
                    {
                        //Obtiene todos
                        var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).OrderByDescending(x => x.Fecha_Emision)
                        .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                        .Take(cantidadRegistrosPorPagina).ToList();

                        var totalDeRegistros = db.tbcontrolDisciplinario.Count();
                        var modelo = new IndexViewModel2();
                        modelo.controlDisciplinarios = ListaDisciplinaria;
                        modelo.PaginaActual = pagina;
                        modelo.TotalRegistros = totalDeRegistros;
                        modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                        return View(modelo);
                    }
                    //FILTROS CON AÑOS
                    else if (AnioFiltro != "-- Años --")
                    {
                        //obtiene filtro de años
                        if (MesFiltro != "0")
                        {
                            //filtros con año y mes

                            var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro).Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).OrderByDescending(x => x.Fecha_Emision)
                            .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                            .Take(cantidadRegistrosPorPagina).ToList();
                            //si falla la busquea reseatea los datos
                            if (ListaDisciplinaria.Count == 0)
                            {
                                ViewBag.ErrorFiltro = true;
                                var ListaDisciplinaria2 = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).OrderByDescending(x => x.Fecha_Emision)
                                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                .Take(cantidadRegistrosPorPagina).ToList();

                                var totalDeRegistros2 = db.tbcontrolDisciplinario.Count();
                                var modelo2 = new IndexViewModel2();
                                modelo2.controlDisciplinarios = ListaDisciplinaria2;
                                modelo2.PaginaActual = pagina;
                                modelo2.TotalRegistros = totalDeRegistros2;
                                modelo2.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                return View(modelo2);
                            }
                            var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro).Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).Count();
                            var modelo = new IndexViewModel2();
                            modelo.controlDisciplinarios = ListaDisciplinaria;
                            modelo.PaginaActual = pagina;
                            modelo.TotalRegistros = totalDeRegistros;
                            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                            return View(modelo);
                        }
                        else
                        {
                            //solo filtros de años
                            var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro).OrderByDescending(x => x.Fecha_Emision)
                            .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                            .Take(cantidadRegistrosPorPagina).ToList();
                            //si falla la busquea reseatea los datos
                            if (ListaDisciplinaria.Count == 0)
                            {
                                ViewBag.ErrorFiltro = true;
                                var ListaDisciplinaria2 = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).OrderByDescending(x => x.Fecha_Emision)
                                .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                                .Take(cantidadRegistrosPorPagina).ToList();

                                var totalDeRegistros2 = db.tbcontrolDisciplinario.Count();
                                var modelo2 = new IndexViewModel2();
                                modelo2.controlDisciplinarios = ListaDisciplinaria2;
                                modelo2.PaginaActual = pagina;
                                modelo2.TotalRegistros = totalDeRegistros2;
                                modelo2.RegistrosPorPagina = cantidadRegistrosPorPagina;
                                return View(modelo2);
                            }
                            var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Fecha_Emision.Year.ToString() == AnioFiltro).Count();
                            var modelo = new IndexViewModel2();
                            modelo.controlDisciplinarios = ListaDisciplinaria;
                            modelo.PaginaActual = pagina;
                            modelo.TotalRegistros = totalDeRegistros;
                            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                            return View(modelo);
                        }

                    }
                    //FILTRO DE MES
                    else if (MesFiltro != "0")
                    {
                        var ListaDisciplinaria = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).OrderByDescending(x => x.Fecha_Emision)
                       .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                       .Take(cantidadRegistrosPorPagina).ToList();
                        //si falla la busquea reseatea los datos
                        if (ListaDisciplinaria.Count == 0)
                        {
                            ViewBag.ErrorFiltro = true;
                            var ListaDisciplinaria2 = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Include(x => x.IdentityUser.Empleado).OrderByDescending(x => x.Fecha_Emision)
                            .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                            .Take(cantidadRegistrosPorPagina).ToList();

                            var totalDeRegistros2 = db.tbcontrolDisciplinario.Count();
                            var modelo2 = new IndexViewModel2();
                            modelo2.controlDisciplinarios = ListaDisciplinaria2;
                            modelo2.PaginaActual = pagina;
                            modelo2.TotalRegistros = totalDeRegistros2;
                            modelo2.RegistrosPorPagina = cantidadRegistrosPorPagina;
                            return View(modelo2);
                        }
                        var totalDeRegistros = db.tbcontrolDisciplinario.Where(x => x.Fecha_Emision.Month.ToString() == MesFiltro.ToString()).Count();
                        var modelo = new IndexViewModel2();
                        modelo.controlDisciplinarios = ListaDisciplinaria;
                        modelo.PaginaActual = pagina;
                        modelo.TotalRegistros = totalDeRegistros;
                        modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                        return View(modelo);
                    }*/
                }
            }
            else
            {
                return RedirectToRoute("EjemploHome");
            }



            return View();
        }



        // GET: ControlDisciplinarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlDisciplinario controlDisciplinario = db.tbcontrolDisciplinario.Find(id);
            if (controlDisciplinario == null)
            {
                return HttpNotFound();
            }
            return View(controlDisciplinario);
        }

        // GET: ControlDisciplinarios/Create
        public ActionResult Create()
        {
            var usuario = db.Users.Find(User.Identity.GetUserId());
            bool respues = Validador.PuedeEntrar(usuario.Id, "Crear Falta Disciplinaria");
            if (respues == true)
            {
                ViewBag.id = auxid;
                return PartialView();
            }
            else
            {
                return RedirectToRoute("EjemploHome");
            }

        }

        // POST: ControlDisciplinarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Control,Fecha_Emision,Descripcion")] ControlDisciplinario controlDisciplinario)
        {
            if (ModelState.IsValid)
            {
                controlDisciplinario.Id = User.Identity.GetUserId();
                controlDisciplinario.Id_Estudiantes = Convert.ToInt32(auxid);
                if (controlDisciplinario.Id_Estudiantes > 0)
                {
                    db.tbcontrolDisciplinario.Add(controlDisciplinario);
                    db.SaveChanges();
                    TempData["SuccessCreate"] = true;
                    return RedirectToAction("Index", new { id = controlDisciplinario.Id_Estudiantes });
                }
            }

            return View(controlDisciplinario);
        }

        // GET: ControlDisciplinarios/Edit/5
        public ActionResult Edit(int? id)
        {

            var usuario = db.Users.Find(User.Identity.GetUserId());
            bool respues = Validador.PuedeEntrar(usuario.Id, "Editar Falta Disciplinaria");
            if (respues == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ControlDisciplinario controlDisciplinario = db.tbcontrolDisciplinario.Find(id);
                if (controlDisciplinario == null)
                {
                    return HttpNotFound();
                }
                return PartialView(controlDisciplinario);
            }
            else
            {
                return RedirectToRoute("EjemploHome");
            }

        }

        // POST: ControlDisciplinarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Control,Fecha_Emision,Descripcion")] ControlDisciplinario controlDisciplinario)
        {
            if (ModelState.IsValid)
            {
                controlDisciplinario.Id = User.Identity.GetUserId();
                controlDisciplinario.Id_Estudiantes = Convert.ToInt32(auxid);
                if (controlDisciplinario.Id_Estudiantes > 0)
                {
                    db.Entry(controlDisciplinario).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessEdit"] = true;
                    return RedirectToAction("Index", new { id = controlDisciplinario.Id_Estudiantes });
                }
            }
            return View(controlDisciplinario);
        }

        // GET: ControlDisciplinarios/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuario = db.Users.Find(User.Identity.GetUserId());
            bool respues = Validador.PuedeEntrar(usuario.Id, "Borrar Falta Diciplinario");
            if (respues == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ControlDisciplinario controlDisciplinario = db.tbcontrolDisciplinario.Include(x => x.Estudiantes).Where(x => x.Id_Control == id).First();

                if (controlDisciplinario == null)
                {
                    return HttpNotFound();
                }
                return PartialView(controlDisciplinario);
            }
            else
            {
                return RedirectToRoute("EjemploHome");
            }

        }

        // POST: ControlDisciplinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControlDisciplinario controlDisciplinario = db.tbcontrolDisciplinario.Find(id);
            db.tbcontrolDisciplinario.Remove(controlDisciplinario);
            db.SaveChanges();
            TempData["SuccessDelete"] = true;
            if (auxid==0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", new { id = controlDisciplinario.Id_Estudiantes });
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
