using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarborConnect.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public string DriverId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(200)]
        public string StartLocation { get; set; }

        [StringLength(200)]
        public string EndLocation { get; set; }

        [Required]
        [StringLength(30)]
        public string TripStatus { get; set; }

        // Navigation properties
        public virtual Booking Booking { get; set; }

        public virtual ApplicationUser Driver { get; set; }
    }
}