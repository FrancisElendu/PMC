namespace PMC.Domain.Entities
{
    // User entity
    public class User
    {
        public int UserId { get; set; } // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } // Unique
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin, Doctor, Pharmacist, Patient
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Pharmacist Pharmacist { get; set; }

        // New: Collection of notifications
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
