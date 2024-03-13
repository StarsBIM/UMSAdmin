using System.ComponentModel.DataAnnotations;

namespace UMS.WebAPI.Models
{
    public class RoleAddPost
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        public long[] MenuIds { get; set; }
    }
}
