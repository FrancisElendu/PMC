namespace PMC.Domain.Entities
{
    // PrescriptionItem entity
    public class PrescriptionItem
    {
        public int PrescriptionItemId { get; set; } // PK
        public int PrescriptionId { get; set; } // FK to Prescription
        public int DrugId { get; set; } // FK to Drug
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public string? Duration { get; set; }
        public string? Instructions { get; set; }

        // Navigation property
        public virtual Prescription? Prescription { get; set; }
        public virtual Drug? Drug { get; set; }
    }
}
