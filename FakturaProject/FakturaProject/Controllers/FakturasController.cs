using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakturaProject.DAL;
using FakturaProject.ViewModel;
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
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // GET: Fakturas/Create
        public ActionResult Create(int? number)
        {
            if(number == null || number == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Stavka> stavkas = new List<Stavka>();
                for (int i = 0; i < number; i++)
                {
                    stavkas.Add(new Stavka());
                }

                FakturaViewModel viewModel = new FakturaViewModel
                {
                    Faktura = new Faktura(),
                    Stavkas = stavkas,
                };
                return View(viewModel);
            }
        }

    

        // POST: Fakturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FakturaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Faktura faktura = viewModel.Faktura;
                db.Fakturas.Add(faktura);
                db.SaveChanges();

                decimal ukupno = 0;
                ukupno = SaveStavkasAndCalculateUkupno(viewModel, faktura, ukupno);

                faktura.Ukupno = ukupno;
                db.Entry(faktura).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        private decimal SaveStavkasAndCalculateUkupno(FakturaViewModel viewModel, Faktura faktura, decimal ukupno)
        {
            for (int i = 0; i < viewModel.Stavkas.Count(); i++)
            {
                Stavka stavka = viewModel.Stavkas[i];
                stavka.Ukupno = stavka.Cena * stavka.Kolicina;
                stavka.FakturaID = faktura.FakturaID;
                ukupno += stavka.Ukupno;

                db.Stavkas.Add(stavka);
                db.SaveChanges();
            }

            return ukupno;
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
            FakturaViewModel viewModel = CreateViewModelFromFaktura(faktura);

            return View(viewModel);
        }


        // POST: Fakturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FakturaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Faktura != null && viewModel.Stavkas != null)
                {
                    decimal ukupnoF = UpdateChangesInStavkasAndCalculateUkupno(viewModel);

                    UpdateFaktura(viewModel, ukupnoF);

                    return RedirectToAction("Index");
                }

            }
            return View(viewModel);
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

        private void UpdateFaktura(FakturaViewModel viewModel, decimal ukupnoF)
        {
            Faktura faktura = db.Fakturas.Find(viewModel.Faktura.FakturaID);
            faktura.Ukupno = ukupnoF;

            db.Entry(faktura).State = EntityState.Modified;
            db.SaveChanges();
        }

        private static FakturaViewModel CreateViewModelFromFaktura(Faktura faktura)
        {
            FakturaViewModel viewModel = new FakturaViewModel
            {
                Faktura = faktura,
                Stavkas = faktura.Stavkas.ToList()
            };
            return viewModel;
        }

        private decimal UpdateChangesInStavkasAndCalculateUkupno(FakturaViewModel viewModel)
        {
            decimal ukupno = 0;
            for (int i = 0; i < viewModel.Stavkas.Count(); i++)
            {
                Stavka stavka = db.Stavkas.Find(viewModel.Stavkas[i].StavkaID);

                stavka.Kolicina = viewModel.Stavkas[i].Kolicina;
                stavka.Cena = viewModel.Stavkas[i].Cena;
                stavka.Ukupno = stavka.Cena * stavka.Kolicina;
                ukupno += stavka.Ukupno;

                db.Entry(stavka).State = EntityState.Modified;
                db.SaveChanges();
            }

            return ukupno;
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
