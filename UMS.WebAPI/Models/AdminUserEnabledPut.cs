using System.ComponentModel.DataAnnotations;

namespace UMS.WebAPI.Models
{
    public class AdminUserEnabledPut
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
    }
}
