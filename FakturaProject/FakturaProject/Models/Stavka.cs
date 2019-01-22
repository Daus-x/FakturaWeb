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
        [Display(Name = "Redni broj")]
        public int RedniBroj { get; set; }

        [Required]
        [Display(Name = "Kolicina")]
        public short Kolicina { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public decimal Cena { get; set; }

        [Display(Name = "Ukupno")]
        public decimal Ukupno { get; set; }

        
        public int FakturaID { get; set; }
        
        public virtual Faktura Faktura { get; set; }
    }
}