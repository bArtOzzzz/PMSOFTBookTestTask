namespace PMSOFTBookTestTask.Models.Request
{
    public class TokensModel
    {
        public Guid Id { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
