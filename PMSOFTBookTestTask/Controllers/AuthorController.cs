using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorService authorService,
                               IMapper mapper,
                               ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetListOfAuthors")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> GetAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("ERROR 400 [AuthorController (GetAsync)]: [ModelStateError] An error occurred in model state.");
                return BadRequest("An error occurred in model state.");
            }

            List<AuthorDto>? result = await _authorService.GetAsync();

            List<AuthorDto>? authorMap = _mapper.Map<List<AuthorDto>>(result);

            if (authorMap is null)
            {
                _logger.LogError("ERROR 400 [AuthorController (GetAsync)]: [ServiceError] An error occurred while getting list of authors.");
                return BadRequest("An error occurred while getting list of authors.");
            }

            return Ok(authorMap);
        }
    }
}
