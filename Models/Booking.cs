using System;
using System.ComponentModel.DataAnnotations;

namespace HarborConnect.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        // Customer
        [Required]
        public string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }


        // Selected Boat
        [Required]
        public int BoatId { get; set; }

        public virtual Boat Boat { get; set; }


        // Selected Boat Category
        [Required]
        public int CategoryId { get; set; }

        public virtual BoatCategory Category { get; set; }


        // Date booking was created
        [Required]
        public DateTime BookingDate { get; set; }


        // Date of the actual trip
        [Required]
        public DateTime TripDate { get; set; }


        // Trip starting and ending time
        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }


        // Number of passengers
        [Required]
        [Range(1, 100)]
        public int NumberOfPassengers { get; set; }


        // Price stored at time of booking
        [Required]
        public decimal TotalAmount { get; set; }


        // Booking status
        [Required]
        [StringLength(30)]
        public string BookingStatus { get; set; }


        // Payment status
        [Required]
        [StringLength(30)]
        public string PaymentStatus { get; set; }


        // Optional customer request
        [StringLength(500)]
        public string SpecialRequest { get; set; }


        // Date/time record was created
        [Required]
        public DateTime CreatedDate { get; set; }


        // Payment relationship
        public virtual Payment Payment { get; set; }


        // PIN relationship
        public virtual PINVerification PINVerification { get; set; }


        // Trip relationship
        public virtual Trip Trip { get; set; }
    }
}