namespace PMC.Application.Dtos
{
    // Pharmacist DTO
    public class PharmacistDto
    {
        public int PharmacistId { get; set; }
        public int UserId { get; set; }
        public string? LicenseNumber { get; set; }
        public string? PharmacyAddress { get; set; }

        // Navigation property
        public UserDto? User { get; set; }
    }
}
