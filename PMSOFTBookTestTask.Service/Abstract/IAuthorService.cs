using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Service.Abstract
{
    public interface IAuthorService
    {
        /// <summary>
        /// GET
        /// Service method. Gets list of authors.
        /// </summary>
        /// <returns><see cref="List{AuthorDto}" /></returns>
        Task<List<AuthorDto>?> GetAsync();
    }
}
