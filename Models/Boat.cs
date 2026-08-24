using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HarborConnect.Models
{
    public class Boat
    {
        [Required]
        [Key]
        public int Registration_Num { get; set; }

        [Required]
        [StringLength(50)]
        public string BoatName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual BoatCategory Category { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public decimal PricePerTrip { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Required]
        [StringLength(30)]
        public string ApprovalStatus { get; set; }

        [StringLength(500)]
        public string AdminComment { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        // Boat Owner
        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        // Related records
        public virtual ICollection<BoatImage> BoatImages { get; set; }

        public virtual ICollection<BoatDocument> BoatDocuments { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public Boat()
        {
            BoatImages = new HashSet<BoatImage>();
            BoatDocuments = new HashSet<BoatDocument>();
            Bookings = new HashSet<Booking>();
        }
    }
}
