using HandymanTools.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace HandymanTools.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public UserType UserType { get; set; }
    }
}