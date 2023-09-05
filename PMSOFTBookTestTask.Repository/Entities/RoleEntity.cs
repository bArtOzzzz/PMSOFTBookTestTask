namespace PMSOFTBookTestTask.Repository.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string? Role { get; set; }
        public List<UserEntity>? Users { get; set; }
    }
}
