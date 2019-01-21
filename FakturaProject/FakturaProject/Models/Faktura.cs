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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FakturaID { get; set; }

        
        public DateTime Datum { get; set; }

        [Required]
        [StringLength(10)]
        public string BrojFakture { get; set; }

        public decimal Ukupno { get; set; }
        
        public ICollection<Stavka> Stavkas { get; set; }

    }
}