using System.ComponentModel.DataAnnotations;

namespace UMS.WebAPI.Models
{
    public class AdminUserLoginPost
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
