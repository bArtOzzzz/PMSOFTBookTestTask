using PMSOFTBookTestTask.Repository.Entities;

namespace PMSOFTBookTestTask.Repository.Abstract
{
    public interface IGenreRepository
    {
        /// <summary>
        /// GET
        /// Repository method. Gets list of genres.
        /// </summary>
        /// <returns><see cref="List{GenreEntity}" /></returns>
        Task<List<GenreEntity>?> GetAsync();
    }
}
