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

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Person>().HasMany(p => p.Photos).WithMany(s => s.Persons).Map(m =>
        //    {
        //        m.ToTable("Person2Photo");
        //        m.MapLeftKey("PersonId");
        //        m.MapRightKey("PhotoId");
        //    });
        //}

        public DbSet<Person> Person { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoPrice> PhotoPrice { get; set; }
    }
}