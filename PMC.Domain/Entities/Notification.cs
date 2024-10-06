namespace PMC.Domain.Entities
{
    // Notification entity
    public class Notification
    {
        public int NotificationId { get; set; } // PK
        public int UserId { get; set; } // FK to User
        public string? Type { get; set; } // Refill, Expiration, Interaction
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
}
