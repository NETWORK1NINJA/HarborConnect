using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarborConnect.Models
{
    public class TripTracking
    {
        [Key]
        public int TrackingId { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public double? Speed { get; set; }

        [Required]
        public DateTime RecordedAt { get; set; }

        // Navigation property
        public virtual Trip Trip { get; set; }
    }
}