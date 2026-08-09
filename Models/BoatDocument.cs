using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarborConnect.Models
{
    public class BoatDocument
    {
        [Key]
        public int BoatDocumentId { get; set; }

        [Required]
        public int BoatId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; }

        [Required]
        [StringLength(300)]
        public string DocumentPath { get; set; }

        public DateTime UploadedDate { get; set; }

        // Navigation property
        public virtual Boat Boat { get; set; }
    }
}