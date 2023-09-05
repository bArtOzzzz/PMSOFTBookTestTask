using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Entities;
using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Service.Dto;
using AutoMapper;

namespace PMSOFTBookTestTask.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>?> GetAsync()
        {
            var books = await _bookRepository.GetAsync();
            var bookMap = _mapper.Map<List<BookDto>>(books);

            return bookMap;
        }

        public async Task<Guid> UpdateAsync(BookDto book, Guid bookId)
        {
            var bookMap = _mapper.Map<BookEntity>(book);
            var result = await _bookRepository.UpdateAsync(bookMap, bookId);

            return result;
        }

        public async Task<Guid> CreateAsync(BookDto book)
        {
            var bookMap = _mapper.Map<BookEntity>(book);
            var result = await _bookRepository.CreateAsync(bookMap);

            return result;
        }

        public async Task<bool> DeleteAsync(Guid bookId)
        {
            var result = await _bookRepository.DeleteAsync(bookId);

            return result;
        }
    }
}
