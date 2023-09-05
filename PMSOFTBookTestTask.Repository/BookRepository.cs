using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Entities;
using PMSOFTBookTestTask.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace PMSOFTBookTestTask.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<BookEntity>?> GetAsync()
        {
            return await _context.Books.Include(e => e.Author)
                                       .Include(e => e.Genre)
                                       .AsNoTracking()
                                       .ToListAsync();
        }

        public async Task<Guid> UpdateAsync(BookEntity book, Guid bookId)
        {
            var currentBook = await _context.Books.FindAsync(bookId);

            currentBook!.GenreId = book.GenreId;
            currentBook!.AuthorId = book.AuthorId;
            currentBook!.Name = book.Name;
            currentBook!.Year = book.Year;

            _context.Books.Update(currentBook);
            await _context.SaveChangesAsync();

            return bookId;
        }

        public async Task<Guid> CreateAsync(BookEntity book)
        {
            BookEntity bookEntity = new()
            {
                Id = Guid.NewGuid(),
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                Name = book.Name,
                Year = book.Year,
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<bool> DeleteAsync(Guid bookId)
        {
            var currentBook = await _context.Books.FindAsync(bookId);

            _context.Books.Remove(currentBook!);
            var saved = _context.SaveChangesAsync();

            return await saved > 0;
        }
    }
}
