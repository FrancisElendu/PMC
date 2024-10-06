namespace PMC.Domain.Entities
{
    // Doctor entity
    public class Doctor
    {
        public int DoctorId { get; set; } // PK
        public int UserId { get; set; } // FK to User
        public string? LicenseNumber { get; set; }
        public string? Specialization { get; set; }
        public string? ClinicAddress { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
        public virtual ICollection<Prescription>? Prescriptions { get; set; } // 1 Doctor -> N Prescriptions
    }
}
