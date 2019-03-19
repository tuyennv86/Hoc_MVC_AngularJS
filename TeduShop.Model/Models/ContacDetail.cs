using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("ContacDetails")]
    public class ContacDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string Website { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        public int Order { get; set; }             
        public string LatMap { get; set; }       
        public string LngMap { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
