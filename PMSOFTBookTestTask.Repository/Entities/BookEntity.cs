namespace PMSOFTBookTestTask.Repository.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public GenreEntity? Genre { get; set; }
        public AuthorEntity? Author { get; set; }
    }
}
