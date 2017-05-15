using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HubFintech.Models;

namespace HubFintech.Controllers
{
    public class transferenciasController : Controller
    {
        private HubFintech6Entities db = new HubFintech6Entities();

        // GET: transferencias
        public ActionResult Index()
        {
            var transferencias = db.transferencias.Include(t => t.Conta).Include(t => t.Conta1);
            TempData["mensagemErro"] = "";
            return View(transferencias.ToList());
        }

        // GET: transferencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transferencia transferencia = db.transferencias.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // GET: transferencias/Create
        public ActionResult Create()
        {
            ViewBag.IDContaOrigem = new SelectList(db.Contas, "IDConta", "IDConta");
            ViewBag.IDContaDestino = new SelectList(db.Contas, "IDConta", "IDConta");
            return View();
        }

        // POST: transferencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTransferencia,IDContaOrigem,IDContaDestino,ValorTransferencia")] transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                db.transferencias.Add(transferencia);
                db.SaveChanges();

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(transferencia).State = EntityState.Modified;
                        Conta contaOrigem = db.Contas.Find(transferencia.IDContaOrigem);
                        Conta contaDestino = db.Contas.Find(transferencia.IDContaDestino);
                        if (contaOrigem.SituacaoConta == "Ativa" && contaDestino.SituacaoConta == "Ativa")
                        {
                            if (!(contaOrigem.TipoConta == "Filial") && !(contaDestino.TipoConta == "Matriz"))
                            {
                                contaOrigem.ValorContav -= transferencia.ValorTransferencia;
                                contaDestino.ValorContav += transferencia.ValorTransferencia;
                                db.SaveChanges();

                                return RedirectToAction("Index");
                            }
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = "Erro!";
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }

            }

            ViewBag.IDContaOrigem = new SelectList(db.Contas, "IDConta", "IDConta", transferencia.IDContaOrigem);
            ViewBag.IDContaDestino = new SelectList(db.Contas, "IDConta", "IDConta", transferencia.IDContaDestino);
            return View(transferencia);
        }

        // GET: transferencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transferencia transferencia = db.transferencias.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDContaOrigem = new SelectList(db.Contas, "IDConta", "IDConta", transferencia.IDContaOrigem);
            ViewBag.IDContaDestino = new SelectList(db.Contas, "IDConta", "IDConta", transferencia.IDContaDestino);
            return View(transferencia);
        }

        // POST: transferencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTransferencia,IDContaOrigem,IDContaDestino,ValorTransferencia")] transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transferencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDContaOrigem = new SelectList(db.Contas, "IDConta", "TipoConta", transferencia.IDContaOrigem);
            ViewBag.IDContaDestino = new SelectList(db.Contas, "IDConta", "TipoConta", transferencia.IDContaDestino);
            return View(transferencia);
        }

        // GET: transferencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transferencia transferencia = db.transferencias.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // POST: transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transferencia transferencia = db.transferencias.Find(id);
            db.transferencias.Remove(transferencia);
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
