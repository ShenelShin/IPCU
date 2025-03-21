using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbantibiotics")]
    public class Antibiotic
    {
        [Key]
        public int AntibioticId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime DateAdded { get; set; }
    }
}