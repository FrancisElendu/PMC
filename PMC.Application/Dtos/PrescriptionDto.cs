namespace PMC.Application.Dtos
{
    // Prescription DTO
    public class PrescriptionDto
    {
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public PatientDto Patient { get; set; }
        public DoctorDto Doctor { get; set; }
        public ICollection<PrescriptionItemDto> PrescriptionItems { get; set; }
    }
}
