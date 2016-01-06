using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class BookContext : DbContext
    {
        public BookContext()
            : base("name=BookContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoPrice>().HasRequired(p => p.Photo).WithRequiredPrincipal(s => s.PhotoPrice).Map(m => m.MapKey("wocao"));
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoPrice> PhotoPrice { get; set; }
    }
}