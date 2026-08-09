using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarborConnect.Models
{
    public class BoatImage
    {
        [Key]
        public int BoatImageId { get; set; }

        [Required]
        public int BoatId { get; set; }

        [Required]
        [StringLength(300)]
        public string ImagePath { get; set; }

        public DateTime UploadedDate { get; set; }

        // Navigation property
        public virtual Boat Boat { get; set; }
    }
}