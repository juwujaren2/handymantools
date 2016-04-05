using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HandymanTools.Models
{
    public class CustomerCreateProfileViewModel
    {
        [Required]
        [Display(Name = "Email Address (Login)")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name", Prompt = "Enter a First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name", Prompt = "Enter a Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Home Phone")]
        [Phone]
        public string HomePhone { get; set; }

        [Required]
        public string HomeAreaCode { get; set; }

        [Required]
        [Display(Name = "Work Phone")]
        public string WorkPhone { get; set; }

        [Required]
        public string WorkAreaCode { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
    }
}