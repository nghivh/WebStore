using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMessage { get; set; }

        [Required]
        [MaxLength(250)]
        public string PaymentMethod { get; set; }

        [Required]
        [MaxLength(250)]
        public string PaymentStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        [MaxLength(250)]
        public string CreatedBy { get; set; }

        public bool Status { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(250)]
        public string CustomerID { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
