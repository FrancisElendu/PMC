using Microsoft.EntityFrameworkCore;
using PMC.Domain.Entities;

namespace PMC.Infrastructure.Persistence
{
    //public class PrescriptionManagementDbContext : DbContext
    internal class PrescriptionManagementDbContext : DbContext
    {
        public PrescriptionManagementDbContext(DbContextOptions<PrescriptionManagementDbContext> options)
            : base(options)
        {
        }

        // DbSets for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User Entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure Patient Entity
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PatientId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete here

            // Configure Doctor Entity
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete here

            // Configure Pharmacist Entity
            modelBuilder.Entity<Pharmacist>()
                .HasKey(ph => ph.PharmacistId);

            modelBuilder.Entity<Pharmacist>()
                .HasOne(ph => ph.User)
                .WithOne(u => u.Pharmacist)
                .HasForeignKey<Pharmacist>(ph => ph.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete here

            // Configure Prescription Entity
            modelBuilder.Entity<Prescription>()
                .HasKey(pr => pr.PrescriptionId);

            modelBuilder.Entity<Prescription>()
                .HasOne(pr => pr.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(pr => pr.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for prescriptions

            modelBuilder.Entity<Prescription>()
                .HasOne(pr => pr.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(pr => pr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for prescriptions

            // Configure PrescriptionItem Entity
            modelBuilder.Entity<PrescriptionItem>()
                .HasKey(pi => pi.PrescriptionItemId);

            modelBuilder.Entity<PrescriptionItem>()
                .HasOne(pi => pi.Prescription)
                .WithMany(pr => pr.PrescriptionItems)
                .HasForeignKey(pi => pi.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for prescription items

            modelBuilder.Entity<PrescriptionItem>()
                .HasOne(pi => pi.Drug)
                .WithMany(d => d.PrescriptionItems)
                .HasForeignKey(pi => pi.DrugId)
                .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete for drugs

            // Configure Drug Entity
            modelBuilder.Entity<Drug>()
                .HasKey(d => d.DrugId);

            // Configure Notification Entity
            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete here
        }
    }
}
