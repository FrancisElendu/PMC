namespace PMC.Application.Dtos
{
    // Notification DTO
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public UserDto User { get; set; }
    }
}
