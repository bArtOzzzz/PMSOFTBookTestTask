using AutoMapper;
using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, 
                             IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<List<AuthorDto>?> GetAsync()
        {
            var authors = await _authorRepository.GetAsync();
            var authorsMap = _mapper.Map<List<AuthorDto>>(authors);

            return authorsMap;
        }
    }
}
