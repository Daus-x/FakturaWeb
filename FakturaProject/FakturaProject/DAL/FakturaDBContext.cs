using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FakturaWeb.Models;

namespace FakturaProject.DAL
{
    public class FakturaDBContext : DbContext
    {
       public FakturaDBContext() : base("name=dbconString")
        {

        }

        public DbSet<Faktura> Fakturas { get; set; } 
        public DbSet<Stavka> Stavkas { get; set; } 
    }
}