using Microsoft.EntityFrameworkCore;
using PetTracking.Models;

namespace PetTracking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<PetVaccine> PetVaccines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-Many: Owner - Pets
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade); //deleting an Owner will delete all their Pets.           

            // Many-to-Many: Pet - Vaccine
            modelBuilder.Entity<PetVaccine>()
                .HasKey(pv => new { pv.PetId, pv.VaccineId }); // Composite key

            modelBuilder.Entity<PetVaccine>()
                .HasOne(pv => pv.Pet)
                .WithMany(p => p.PetVaccines)
                .HasForeignKey(pv => pv.PetId);

            modelBuilder.Entity<PetVaccine>()
                .HasOne(pv => pv.Vaccine)
                .WithMany(v => v.PetVaccines)
                .HasForeignKey(pv => pv.VaccineId);
        }
    }
}
