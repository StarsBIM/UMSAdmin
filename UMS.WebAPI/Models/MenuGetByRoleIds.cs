using System.ComponentModel.DataAnnotations;

namespace UMS.WebAPI.Models
{
    public class MenuGetByRoleIds
    {
        [Required]
        public long[] RoleIds { get; set; }
    }
}
