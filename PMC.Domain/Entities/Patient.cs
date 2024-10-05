namespace PMC.Domain.Entities
{
    // Patient entity
    public class Patient
    {
        public int PatientId { get; set; } // PK
        public int UserId { get; set; } // FK to User
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalHistory { get; set; }

        // Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; } // 1 Patient -> N Prescriptions
    }
}
