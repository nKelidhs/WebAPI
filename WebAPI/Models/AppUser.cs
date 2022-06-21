using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AppUser
    {

        [Key]
        public Int32 UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public String Username { get; set; }

        [Required]
        [MaxLength(250)]
        public String Password { get; set; }

        [MaxLength(250)]
        public String Email { get; set; }

        [MaxLength(250)]
        public String FirstName { get; set; }

        [MaxLength(250)]
        public String LastName { get; set; }

        [MaxLength(250)]
        public String Mobile { get; set; }
    }
}
