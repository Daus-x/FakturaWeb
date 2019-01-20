using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakturaWeb.Models
{
    public class Stavka
    {
        public int StavkaID { get; set; }
        public int RedniBroj { get; set; }
        public int Kolicina { get; set; }
        public float Cena { get; set; }
        public float Ukupno { get; set; }
        public int FakturaID { get; set; }
        
        public virtual Faktura Faktura { get; set; }
    }
}