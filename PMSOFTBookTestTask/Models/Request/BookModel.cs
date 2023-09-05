namespace PMSOFTBookTestTask.Models.Request
{
    public class BookModel
    {
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
    }
}
