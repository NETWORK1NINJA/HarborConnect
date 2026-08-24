using System;
using System.ComponentModel.DataAnnotations;

namespace HarborConnect.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ApplicationUser User { get; set; }
    }
}
