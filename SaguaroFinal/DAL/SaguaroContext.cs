using SaguaroFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SaguaroFinal.DAL
{
    public class SaguaroContext : DbContext
    {
        public SaguaroContext() : base("DefaultConnection")
        {

            Database.SetInitializer<SaguaroContext>(new DropCreateDatabaseIfModelChanges<SaguaroContext>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired<Category>(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany<Product>(c => c.Products)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
