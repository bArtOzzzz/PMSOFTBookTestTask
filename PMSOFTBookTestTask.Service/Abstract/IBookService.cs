using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Service.Abstract
{
    public interface IBookService
    {
        /// <summary>
        /// GET
        /// Service method. Gets list of books.
        /// </summary>
        /// <returns><see cref="List{BookDto}" /></returns>
        Task<List<BookDto>?> GetAsync();

        /// <summary>
        /// PUT
        /// Service method. Updates book details.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns><see cref="Guid"/></returns>
        Task<Guid> UpdateAsync(BookDto book, Guid bookId);

        /// <summary>
        /// POST
        /// Service method. Creates new book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns><see cref="Guid"/></returns>
        Task<Guid> CreateAsync(BookDto book);

        /// <summary>
        /// DELETE
        /// Service method. Deletes book.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns><see cref="Guid"/></returns>
        Task<bool> DeleteAsync(Guid bookId);
    }
}
