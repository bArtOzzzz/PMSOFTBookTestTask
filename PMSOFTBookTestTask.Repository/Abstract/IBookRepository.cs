using PMSOFTBookTestTask.Repository.Entities;

namespace PMSOFTBookTestTask.Repository.Abstract
{
    public interface IBookRepository
    {
        /// <summary>
        /// GET
        /// Repository method. Gets list of books.
        /// </summary>
        /// <returns><see cref="List{BookEntity}" /></returns>
        Task<List<BookEntity>?> GetAsync();

        /// <summary>
        /// PUT
        /// Repository method. Updates book details.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns><see cref="Guid"/></returns>
        Task<Guid> UpdateAsync(BookEntity book, Guid bookId);

        /// <summary>
        /// POST
        /// Repository method. Creates new book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns><see cref="Guid"/></returns>
        Task<Guid> CreateAsync(BookEntity book);

        /// <summary>
        /// DELETE
        /// Repository method. Deletes book.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns><see cref="Guid"/></returns>
        Task<bool> DeleteAsync(Guid bookId);
    }
}
