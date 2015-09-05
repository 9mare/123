using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string encryptedPassword { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password fields did not match")]
        public string ConfirmedPassword { get; set; }

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "This email address is not valid.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool LoggedIn { get; set; }
    }
}