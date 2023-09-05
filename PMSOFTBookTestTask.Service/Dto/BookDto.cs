namespace PMSOFTBookTestTask.Service.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public GenreDto? Genre { get; set; }
        public AuthorDto? Author { get; set; }
    }
}
