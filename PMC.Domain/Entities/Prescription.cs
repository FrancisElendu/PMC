namespace PMC.Domain.Entities
{
    // Prescription entity
    public class Prescription
    {
        public int PrescriptionId { get; set; } // PK
        public int PatientId { get; set; } // FK to Patient
        public int DoctorId { get; set; } // FK to Doctor
        public DateTime DateIssued { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Status { get; set; } // Active, Expired, Refilled, Cancelled
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<PrescriptionItem>? PrescriptionItems { get; set; } // 1 Prescription -> N PrescriptionItems
    }
}
