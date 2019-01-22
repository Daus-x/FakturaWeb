using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakturaWeb.Models;

namespace FakturaProject.ViewModel
{
    public class FakturaViewModel
    {
        public Faktura Faktura { get; set; }

        public List<Stavka> Stavkas { get; set; }
    }
}