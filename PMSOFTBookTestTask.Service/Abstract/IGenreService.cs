using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Service.Abstract
{
    public interface IGenreService
    {
        /// <summary>
        /// GET
        /// Service method. Gets list of genres.
        /// </summary>
        /// <returns><see cref="List{GenreDto}" /></returns>
        Task<List<GenreDto>?> GetAsync();
    }
}
