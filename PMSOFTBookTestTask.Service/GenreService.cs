using AutoMapper;
using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Service
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, 
                            IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<List<GenreDto>?> GetAsync()
        {
            var genres = await _genreRepository.GetAsync();
            var genresMap = _mapper.Map<List<GenreDto>>(genres);

            return genresMap;
        }
    }
}
