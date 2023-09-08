using PMSOFTBookTestTask.Repository.Entities;

namespace PMSOFTBookTestTask.Repository.Abstract
{
    public interface IAuthorRepository
    {
        /// <summary>
        /// GET
        /// Repository method. Gets list of authors.
        /// </summary>
        /// <returns><see cref="List{AuthorEntity}" /></returns>
        Task<List<AuthorEntity>?> GetAsync();
    }
}
