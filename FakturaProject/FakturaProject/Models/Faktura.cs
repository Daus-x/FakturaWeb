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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        [Required]
        [StringLength(10)]
        public string BrojFakture { get; set; }

        public decimal Ukupno { get; set; }
        
        public ICollection<Stavka> Stavkas { get; set; }

    }
}