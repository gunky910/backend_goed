using Microsoft.EntityFrameworkCore;

namespace angularAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Lijst> Lijsten { get; set; }
        public DbSet<Stem> Stemmen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");


            modelBuilder.Entity<Item>().ToTable("Item");

            modelBuilder.Entity<Lijst>().ToTable("Lijst");
            
            modelBuilder.Entity<Stem>().ToTable("Stem");

        }

    }
}