using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarborConnect.Models
{
    public class BoatCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Boat> Boats { get; set; }

        public BoatCategory()
        {
            Boats = new HashSet<Boat>();
        }
    }
}