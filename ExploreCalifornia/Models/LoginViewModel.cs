using System.ComponentModel.DataAnnotations;

namespace ExploreCalifornia.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress, MaxLength(100)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}