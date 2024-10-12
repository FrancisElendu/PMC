using Microsoft.EntityFrameworkCore;
using PMC.Domain.Entities;
using PMC.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Infrastructure.Seeder
{
    internal class DatabaseSeeder(PrescriptionManagementDbContext context) : IDatabaseSeeder
    {
        private readonly PrescriptionManagementDbContext _context = context;

        public async Task SeedAsync()
        {
            // Apply pending migrations if any
            if (_context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }

            // Seed each entity
            await SeedUsersAsync();
            await SeedPatientsAsync();
            await SeedDoctorsAsync();
            await SeedPharmacistsAsync();
            await SeedDrugsAsync();
            await SeedPrescriptionsAsync();
            await SeedPrescriptionItemsAsync();
            await SeedNotificationsAsync();
        }

        private async Task SeedUsersAsync()
        {
            if (!_context.Users.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users ON;");
                var users = new List<User>
            {
                new User
                {
                    //UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PasswordHash = "hashedpassword1", // Use a proper hash in production
                    Role = "Patient",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    //UserId = 2,
                    FirstName = "Anna",
                    LastName = "Smith",
                    Email = "dr.smith@example.com",
                    PasswordHash = "hashedpassword2",
                    Role = "Doctor",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    //UserId = 3,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "pharmacist.jane@example.com",
                    PasswordHash = "hashedpassword3",
                    Role = "Pharmacist",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
                _context.Users.AddRange(users);
                await _context.SaveChangesAsync();
                // Turn off IDENTITY_INSERT for Patients table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users OFF;");
            }
        }

        private async Task SeedPatientsAsync()
        {
            if (!_context.Patients.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Patients ON;");
                var patients = new List<Patient>
            {
                new Patient
                {
                    //PatientId = 1,
                    UserId = 1, // FK to User (John Doe)
                    DateOfBirth = new DateTime(1985, 5, 12),
                    Address = "123 Health St.",
                    PhoneNumber = "555-1234",
                    MedicalHistory = "None"
                },
                new Patient
                {
                    //PatientId = 2,
                    UserId = 2,
                    DateOfBirth = new DateTime(1990, 6, 22),
                    Address = "456 Care Ave.",
                    PhoneNumber = "555-5678",
                    MedicalHistory = "Allergic to penicillin"
                }
            };
                _context.Patients.AddRange(patients);
                await _context.SaveChangesAsync();

                // Turn off IDENTITY_INSERT for Patients table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Patients OFF;");
            }
        }

        private async Task SeedDoctorsAsync()
        {
            if (!_context.Doctors.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Doctors ON;");
                var doctors = new List<Doctor>
            {
                new Doctor
                {
                    //DoctorId = 1,
                    UserId = 2, // FK to User (Anna Smith)
                    LicenseNumber = "DOC56789",
                    Specialization = "Cardiology",
                    ClinicAddress = "45 Heartway Blvd."
                }
            };
                _context.Doctors.AddRange(doctors);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Doctors OFF;");
            }
        }

        private async Task SeedPharmacistsAsync()
        {
            if (!_context.Pharmacists.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Pharmacists ON;");
                var pharmacists = new List<Pharmacist>
            {
                new Pharmacist
                {
                    //PharmacistId = 1,
                    UserId = 3, // FK to User (Jane Doe)
                    LicenseNumber = "PHARM98765",
                    PharmacyAddress = "789 Medicine Ave."
                }
            };
                _context.Pharmacists.AddRange(pharmacists);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Pharmacists OFF;");
            }
        }

        private async Task SeedDrugsAsync()
        {
            if (!_context.Drugs.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Drugs ON;");
                var drugs = new List<Drug>
            {
                new Drug
                {
                    //DrugId = 1,
                    Name = "Aspirin",
                    Description = "Pain reliever",
                    SideEffects = "Nausea, dizziness",
                    Interactions = "Avoid alcohol",
                    StockQuantity = 200
                },
                new Drug
                {
                    //DrugId = 2,
                    Name = "Ibuprofen",
                    Description = "Anti-inflammatory",
                    SideEffects = "Stomach ache, headache",
                    Interactions = "Avoid with aspirin",
                    StockQuantity = 150
                }
            };
                _context.Drugs.AddRange(drugs);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Drugs OFF;");
            }
        }

        private async Task SeedPrescriptionsAsync()
        {
            if (!_context.Prescriptions.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Prescriptions ON;");
                var prescriptions = new List<Prescription>
            {
                new Prescription
                {
                    //PrescriptionId = 1,
                    PatientId = 1, // FK to Patient (John Doe)
                    DoctorId = 1, // FK to Doctor (Anna Smith)
                    DateIssued = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddMonths(1),
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
                _context.Prescriptions.AddRange(prescriptions);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Prescriptions OFF;");
            }
        }

        private async Task SeedPrescriptionItemsAsync()
        {
            if (!_context.PrescriptionItems.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PrescriptionItems ON;");
                var prescriptionItems = new List<PrescriptionItem>
            {
                new PrescriptionItem
                {
                    //PrescriptionItemId = 1,
                    PrescriptionId = 1, // FK to Prescription
                    DrugId = 1, // FK to Drug (Aspirin)
                    Dosage = "500mg",
                    Frequency = "Twice a day",
                    Duration = "10 days",
                    Instructions = "Take after meal"
                },
                new PrescriptionItem
                {
                    //PrescriptionItemId = 2,
                    PrescriptionId = 1, // FK to Prescription
                    DrugId = 2, // FK to Drug (Ibuprofen)
                    Dosage = "200mg",
                    Frequency = "Once a day",
                    Duration = "5 days",
                    Instructions = "Take with water"
                }
            };
                _context.PrescriptionItems.AddRange(prescriptionItems);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PrescriptionItems OFF;");
            }
        }

        private async Task SeedNotificationsAsync()
        {
            if (!_context.Notifications.Any())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Notifications ON;");
                var notifications = new List<Notification>
            {
                new Notification
                {
                    //NotificationId = 1,
                    UserId = 1, // FK to User (John Doe)
                    Type = "Refill",
                    Message = "Your prescription needs a refill.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Notification
                {
                    //NotificationId = 2,
                    UserId = 2, // FK to User (Anna Smith)
                    Type = "Expiration",
                    Message = "Your patient's prescription is about to expire.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                }
            };
                _context.Notifications.AddRange(notifications);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Notifications OFF;");
            }
        }
    }


}
