using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FakturaWeb.Models;

namespace FakturaProject.DAL
{
    public class FakturaInitializer : DropCreateDatabaseAlways<FakturaDBContext>
    {
        protected override void Seed(FakturaDBContext context)
        {
            var stav = new List<Stavka>
            {
                new Stavka{RedniBroj=1,Cena=100,Kolicina=5,Ukupno=500,StavkaID=1,FakturaID=1},
                new Stavka{RedniBroj=2,Cena=200,Kolicina=5,Ukupno=1000,StavkaID=2,FakturaID=1},
                new Stavka{RedniBroj=3,Cena=300,Kolicina=5,Ukupno=1500,StavkaID=3,FakturaID=1},
                new Stavka{RedniBroj=1,Cena=100,Kolicina=5,Ukupno=500,StavkaID=4,FakturaID=2},
                new Stavka{RedniBroj=2,Cena=150,Kolicina=5,Ukupno=750,StavkaID=5,FakturaID=2},
                new Stavka{RedniBroj=3,Cena=200,Kolicina=5,Ukupno=1000,StavkaID=6,FakturaID=2},
            };

            stav.ForEach(s => context.Stavkas.Add(s));
            context.SaveChanges();

            var fakt = new List<Faktura>
            {
                new Faktura{FakturaID=1, BrojFakture="123sff99", Datum=DateTime.Parse("2005-09-01"), Ukupno=3000},
                new Faktura{FakturaID=2, BrojFakture="12sfsdfs", Datum=DateTime.Parse("2005-10-02"), Ukupno=2250}
            };

            fakt.ForEach(s => context.Fakturas.Add(s));
            context.SaveChanges();

        }
    }
    
}