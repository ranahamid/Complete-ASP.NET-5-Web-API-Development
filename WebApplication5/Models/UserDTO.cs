﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage ="Your password is limited to {2} to {1} characters.", MinimumLength =6)]
        public string Password { get; set; }
    }
}
