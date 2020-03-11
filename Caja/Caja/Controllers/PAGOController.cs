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
    public class PAGOController : Controller
    {
        private PRUEBAEntities db = new PRUEBAEntities();

        // GET: PAGO
        public ActionResult Index()
        {
            var pAGOes = db.PAGOes.Include(p => p.CAJA).Include(p => p.CAJERO).Include(p => p.CONCEPTOSPAGO).Include(p => p.ESTADO1).Include(p => p.MONEDA1).Include(p => p.TBESTUDIANTE).Include(p => p.TIPODEPAGO);
            return View(pAGOes.ToList());
        }

        // GET: PAGO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO pAGO = db.PAGOes.Find(id);
            if (pAGO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO);
        }

        // GET: PAGO/Create
        public ActionResult Create()
        {
            ViewBag.idCaja = new SelectList(db.CAJAs, "idCaja", "numCaja");
            ViewBag.idCajero = new SelectList(db.CAJEROes, "idCajero", "nombre");
            ViewBag.conceptoPago = new SelectList(db.CONCEPTOSPAGOes, "idConcepto", "ano");
            ViewBag.estado = new SelectList(db.ESTADOes, "idEstado", "nombre");
            ViewBag.moneda = new SelectList(db.MONEDAs, "idMoneda", "nombre");
            ViewBag.idEstudiante = new SelectList(db.TBESTUDIANTEs, "id_estudiante", "STR_NOMBRE");
            ViewBag.tipoPago = new SelectList(db.TIPODEPAGOes, "idTipo", "nombre");
            return View();
        }

        // POST: PAGO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPago,fechaTransaccion,idCajero,idCaja,idEstudiante,nombreEstudiante,apellidoEstudiante,conceptoPago,tipoPago,moneda,pagoAbonado,equivalencia,balance,estado,mora")] PAGO pAGO)
        {
            if (ModelState.IsValid)
            {
                db.PAGOes.Add(pAGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCaja = new SelectList(db.CAJAs, "idCaja", "numCaja", pAGO.idCaja);
            ViewBag.idCajero = new SelectList(db.CAJEROes, "idCajero", "nombre", pAGO.idCajero);
            ViewBag.conceptoPago = new SelectList(db.CONCEPTOSPAGOes, "idConcepto", "ano", pAGO.conceptoPago);
            ViewBag.estado = new SelectList(db.ESTADOes, "idEstado", "nombre", pAGO.estado);
            ViewBag.moneda = new SelectList(db.MONEDAs, "idMoneda", "nombre", pAGO.moneda);
            ViewBag.idEstudiante = new SelectList(db.TBESTUDIANTEs, "id_estudiante", "STR_NOMBRE", pAGO.idEstudiante);
            ViewBag.tipoPago = new SelectList(db.TIPODEPAGOes, "idTipo", "nombre", pAGO.tipoPago);
            return View(pAGO);
        }

        // GET: PAGO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO pAGO = db.PAGOes.Find(id);
            if (pAGO == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCaja = new SelectList(db.CAJAs, "idCaja", "numCaja", pAGO.idCaja);
            ViewBag.idCajero = new SelectList(db.CAJEROes, "idCajero", "nombre", pAGO.idCajero);
            ViewBag.conceptoPago = new SelectList(db.CONCEPTOSPAGOes, "idConcepto", "ano", pAGO.conceptoPago);
            ViewBag.estado = new SelectList(db.ESTADOes, "idEstado", "nombre", pAGO.estado);
            ViewBag.moneda = new SelectList(db.MONEDAs, "idMoneda", "nombre", pAGO.moneda);
            ViewBag.idEstudiante = new SelectList(db.TBESTUDIANTEs, "id_estudiante", "STR_NOMBRE", pAGO.idEstudiante);
            ViewBag.tipoPago = new SelectList(db.TIPODEPAGOes, "idTipo", "nombre", pAGO.tipoPago);
            return View(pAGO);
        }

        // POST: PAGO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPago,fechaTransaccion,idCajero,idCaja,idEstudiante,nombreEstudiante,apellidoEstudiante,conceptoPago,tipoPago,moneda,pagoAbonado,equivalencia,balance,estado,mora")] PAGO pAGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCaja = new SelectList(db.CAJAs, "idCaja", "numCaja", pAGO.idCaja);
            ViewBag.idCajero = new SelectList(db.CAJEROes, "idCajero", "nombre", pAGO.idCajero);
            ViewBag.conceptoPago = new SelectList(db.CONCEPTOSPAGOes, "idConcepto", "ano", pAGO.conceptoPago);
            ViewBag.estado = new SelectList(db.ESTADOes, "idEstado", "nombre", pAGO.estado);
            ViewBag.moneda = new SelectList(db.MONEDAs, "idMoneda", "nombre", pAGO.moneda);
            ViewBag.idEstudiante = new SelectList(db.TBESTUDIANTEs, "id_estudiante", "STR_NOMBRE", pAGO.idEstudiante);
            ViewBag.tipoPago = new SelectList(db.TIPODEPAGOes, "idTipo", "nombre", pAGO.tipoPago);
            return View(pAGO);
        }

        // GET: PAGO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO pAGO = db.PAGOes.Find(id);
            if (pAGO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO);
        }

        // POST: PAGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAGO pAGO = db.PAGOes.Find(id);
            db.PAGOes.Remove(pAGO);
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
