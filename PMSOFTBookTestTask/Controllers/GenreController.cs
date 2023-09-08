using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreService genreService, 
                               IMapper mapper,
                               ILogger<GenreController> logger)
        {
            _genreService = genreService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetListOfGenres")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> GetAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("ERROR 400 [GenreController (GetAsync)]: [ModelStateError] An error occurred in model state.");
                return BadRequest("An error occurred in model state.");
            }

            List<GenreDto>? result = await _genreService.GetAsync();

            List<GenreDto>? genreMap = _mapper.Map<List<GenreDto>>(result);

            if (genreMap is null)
            {
                _logger.LogError("ERROR 400 [GenreController (GetAsync)]: [ServiceError] An error occurred while getting list of genres.");
                return BadRequest("An error occurred while getting list of genres.");
            }

            return Ok(genreMap);
        }
    }
}
