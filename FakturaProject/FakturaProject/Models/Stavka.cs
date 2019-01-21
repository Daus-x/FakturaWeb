using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FakturaWeb.Models
{
    public class Stavka
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StavkaID { get; set; }

        [Required]
        public int RedniBroj { get; set; }

        
        [Required]
        public short Kolicina { get; set; }

        [Required]
        public decimal Cena { get; set; }

        
        public decimal Ukupno { get; set; }

        
        public int FakturaID { get; set; }
        
        public virtual Faktura Faktura { get; set; }
    }
}