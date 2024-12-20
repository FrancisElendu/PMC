﻿using System.Text.Json.Serialization;

namespace PMC.Application.Dtos
{
    // User DTO
    public class UserDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [JsonIgnore]
        public PatientDto? Patient { get; set; }
        [JsonIgnore]
        public DoctorDto? Doctor { get; set; }
        [JsonIgnore]
        public PharmacistDto? Pharmacist { get; set; }

        // Collection of notifications
        [JsonIgnore]
        public ICollection<NotificationDto>? Notifications { get; set; }
    }
}
