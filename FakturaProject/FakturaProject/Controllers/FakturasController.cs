using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakturaProject.DAL;
using FakturaWeb.Models;

namespace FakturaProject.Controllers
{
    public class FakturasController : Controller
    {
        private FakturaDBContext db = new FakturaDBContext();

        // GET: Fakturas
        public ActionResult Index()
        {
            return View(db.Fakturas.ToList());
        }

        // GET: Fakturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakturas.Find(id);
            List<Stavka> listaStavkas = db.Stavkas.Where(s => s.FakturaID == faktura.FakturaID).OrderBy(s => s.RedniBroj).ToList();

            if (listaStavkas == null || faktura == null)
            {
                return HttpNotFound();
            }

            ViewData["listStavkas"] = listaStavkas;
            return View(faktura);
        }

        // GET: Fakturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fakturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Datum,BrojFakture,Ukupno")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Fakturas.Add(faktura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faktura);
        }

        // GET: Fakturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakturas.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Datum,BrojFakture,Ukupno")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faktura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faktura);
        }

        // GET: Fakturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakturas.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faktura faktura = db.Fakturas.Find(id);
            db.Fakturas.Remove(faktura);
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
