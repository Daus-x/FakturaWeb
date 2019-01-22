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

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Broj Fakture")]
        public string BrojFakture { get; set; }

        [Display(Name = "Ukupno")]
        public decimal Ukupno { get; set; }
        
        public virtual ICollection<Stavka> Stavkas { get; set; }

    }
}