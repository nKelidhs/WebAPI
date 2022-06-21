using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    /// <summary>
    /// Customer Entity
    /// </summary>
    public class Customer
    {
        [Key]
        public Int32 CustomerId { get; set; }

        [Required]
        [MaxLength(250)]
        public String FullName { get; set; }

        [Required]
        [MaxLength(250)]
        public String Mobile { get; set; }

        [Required]
        [MaxLength(250)]
        public String Email{ get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
