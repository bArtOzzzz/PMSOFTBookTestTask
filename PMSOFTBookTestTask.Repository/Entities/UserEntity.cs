namespace PMSOFTBookTestTask.Repository.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public RoleEntity? Role { get; set; }
    }
}
