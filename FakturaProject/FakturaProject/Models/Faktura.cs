using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakturaWeb.Models
{
    public class Faktura
    {
        public int FakturaID { get; set; }
        public DateTime Datum { get; set; }
        public string BrojFakture { get; set; }
        public float Ukupno { get; set; }
        
        public virtual ICollection<Stavka> Stavkas { get; set; }

    }
}