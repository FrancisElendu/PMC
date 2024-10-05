namespace PMC.Application.Dtos
{
    // Doctor DTO
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string LicenseNumber { get; set; }
        public string Specialization { get; set; }
        public string ClinicAddress { get; set; }

        // Navigation properties
        public UserDto User { get; set; }
        public ICollection<PrescriptionDto> Prescriptions { get; set; }
    }
}
