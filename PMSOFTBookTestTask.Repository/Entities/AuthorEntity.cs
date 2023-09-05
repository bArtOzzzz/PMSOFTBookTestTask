namespace PMSOFTBookTestTask.Repository.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }
        public string? AuthorName { get; set; }
        public List<BookEntity>? Books { get; set; }
    }
}
