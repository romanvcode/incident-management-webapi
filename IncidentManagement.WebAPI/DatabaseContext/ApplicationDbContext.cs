using IncidentManagement.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace IncidentManagement.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountName)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            //modelBuilder.Entity<Incident>()
            //    .Property(i => i.IncidentName)
            //    .ValueGeneratedOnAdd()
            //    .HasValueGenerator<GuidValueGenerator>();

            modelBuilder.Entity<Incident>()
                .HasMany(i => i.Accounts)
                .WithOne(i => i.Incident)
                .HasForeignKey(i => i.IncidentName);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Contacts)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountID);
        }
    }
}
