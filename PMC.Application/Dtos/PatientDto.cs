namespace PMC.Application.Dtos
{
    // Patient DTO
    public class PatientDto
    {
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalHistory { get; set; }

        // Navigation properties
        public UserDto User { get; set; }
        public ICollection<PrescriptionDto> Prescriptions { get; set; }
    }
}
