using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.Username).HasMaxLength(250);
                entity.Property(e => e.Password).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.FirstName).HasMaxLength(250);
                entity.Property(e => e.LastName).HasMaxLength(250);
                entity.Property(e => e.Mobile).HasMaxLength(250);

            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.FullName).HasMaxLength(250);
                entity.Property(e => e.Mobile).HasMaxLength(250);

            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.TransactionId);
                entity.Property(e => e.CustomerId);
                entity.Property(e => e.Price);
                entity.Property(e => e.Description).HasMaxLength(4000);

            });

            //modelBuilder.Entity<Customer>().Navigation(f => f.Transactions).AutoInclude();

        }
    }
}
