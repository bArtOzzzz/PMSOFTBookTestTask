namespace PMSOFTBookTestTask.Repository.Entities
{
    public class GenreEntity
    {
        public Guid Id { get; set; }
        public string? GenreName { get; set; }
        public List<BookEntity>? Books { get; set; }
    }
}
