namespace PMC.Application.Dtos
{
    // PrescriptionItem DTO
    public class PrescriptionItemDto
    {
        public int PrescriptionItemId { get; set; }
        public int PrescriptionId { get; set; }
        public int DrugId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Instructions { get; set; }

        // Navigation properties
        public PrescriptionDto Prescription { get; set; }
        public DrugDto Drug { get; set; }
    }
}
