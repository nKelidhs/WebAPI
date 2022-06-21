using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Transaction
    {
        [Key]
        public Int32 TransactionId { get; set; }

        [Required]
        public Int32 CustomerId { get; set; }

        [Required]
        public Decimal Price { get; set; }

        [MaxLength(4000)]
        public String Description { get; set; }

        public Customer Customer { get; set; }
    }
}
