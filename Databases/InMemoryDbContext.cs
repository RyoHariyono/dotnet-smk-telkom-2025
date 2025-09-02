using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Databases
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Atur tipe data untuk Price (opsional)
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            // Title harus diisi
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired();

            // Name customer harus diisi
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}