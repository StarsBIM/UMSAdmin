using System.ComponentModel.DataAnnotations;

namespace UMS.WebAPI.Models
{
    public class AdminUserAddPost
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        public string[] City { get; set; }
        [Required]
        public long[] RoleIds { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
    }
}
