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
    public class StavkasController : Controller
    {
        private FakturaDBContext db = new FakturaDBContext();

        // GET: Stavkas
        public ActionResult Index()
        {
            var stavkas = db.Stavkas.Include(s => s.Faktura);
            return View(stavkas.ToList());
        }

        // GET: Stavkas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavkas.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // GET: Stavkas/Create
        public ActionResult Create()
        {
            ViewBag.FakturaID = new SelectList(db.Fakturas, "FakturaID", "BrojFakture");
            return View();
        }

        // POST: Stavkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StavkaID,RedniBroj,Kolicina,Cena,Ukupno,FakturaID")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Stavkas.Add(stavka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FakturaID = new SelectList(db.Fakturas, "FakturaID", "BrojFakture", stavka.FakturaID);
            return View(stavka);
        }

        // GET: Stavkas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavkas.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            ViewBag.FakturaID = new SelectList(db.Fakturas, "FakturaID", "BrojFakture", stavka.FakturaID);
            return View(stavka);
        }

        // POST: Stavkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StavkaID,RedniBroj,Kolicina,Cena,Ukupno,FakturaID")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stavka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FakturaID = new SelectList(db.Fakturas, "FakturaID", "BrojFakture", stavka.FakturaID);
            return View(stavka);
        }

        // GET: Stavkas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavkas.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // POST: Stavkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stavka stavka = db.Stavkas.Find(id);
            db.Stavkas.Remove(stavka);
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
