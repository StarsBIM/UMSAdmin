namespace UMS.Core.DB.Entities
{
    public class AdminLogEntity : BaseEntity
    {
        public long AdminUserId { get; set; }
        public virtual AdminUserEntity AdminUser { get; set; }
        public string Message { get; set; }
    }
}
