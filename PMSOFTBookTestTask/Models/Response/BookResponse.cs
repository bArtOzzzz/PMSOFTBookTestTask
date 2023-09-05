using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Models.Response
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public GenreDto? Genre { get; set; }
        public AuthorDto? Author { get; set; }
    }
}
