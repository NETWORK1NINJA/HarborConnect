using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarborConnect.Models
{
    public class PINVerification
    {
        [Key]
        public int PINVerificationId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        [StringLength(255)]
        public string PINHash { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public bool IsVerified { get; set; }

        // Navigation property
        public virtual Booking Booking { get; set; }
    }
}