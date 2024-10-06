namespace PMC.Domain.Entities
{
    // Pharmacist entity
    public class Pharmacist
    {
        public int PharmacistId { get; set; } // PK
        public int UserId { get; set; } // FK to User
        public string? LicenseNumber { get; set; }
        public string? PharmacyAddress { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
}
