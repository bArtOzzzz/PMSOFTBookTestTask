using PMSOFTBookTestTask.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using PMSOFTBookTestTask.Models.Response;
using PMSOFTBookTestTask.Models.Request;
using PMSOFTBookTestTask.Service.Dto;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using AutoMapper;

namespace PMSOFTBookTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        private readonly IValidator<BookModel> _bookModelValidator;

        public BookController(IBookService bookService,
                              IMapper mapper,
                              ILogger<BookController> logger,
                              IValidator<BookModel> bookModelValidator)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
            _bookModelValidator = bookModelValidator;
        }

        [HttpGet("GetListOfBooks")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("ERROR 400 [BookController (GetAsync)]: [ModelStateError] An error occurred in model state.");
                return BadRequest("An error occurred in model state.");
            }

            List<BookDto>? result = await _bookService.GetAsync();

            List<BookResponse>? booksMap = _mapper.Map<List<BookResponse>>(result);

            if (booksMap is null)
            {
                _logger.LogError("ERROR 400 [BookController (GetAsync)]: [ServiceError] An error occurred while getting list of books.");
                return BadRequest("An error occurred while getting list of books.");
            }

            return Ok(booksMap);
        }

        [HttpPut("UpdateBook/{bookId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> UpdateAsync(BookModel model, Guid bookId)
        {
            ValidationResult validationResult = _bookModelValidator.Validate(model);

            if (bookId.Equals(Guid.Empty) || model is null || !validationResult.IsValid)
            {
                _logger.LogError("ERROR 400 [BookController (UpdateAsync)]: [ModelStateError] Invalid model or model state.");
                return BadRequest("Invalid model or model state.");
            }

            BookDto? bookMap = _mapper.Map<BookDto>(model);

            if (bookMap is null)
            {
                _logger.LogError("ERROR 400 [BookController (UpdateAsync)]: [ServiceError] An error occurred while mapping book.");
                return BadRequest("An error occurred while mapping book.");
            }

            Guid id = await _bookService.UpdateAsync(bookMap, bookId);

            if (id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 400 [BookController (UpdateAsync)]: [ServiceError] An error occurred while updating book.");
                return BadRequest("An error occurred while updating book.");
            }

            return Ok(id);
        }

        [HttpPost("CreateBook")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> CreateAsync(BookModel model)
        {
            ValidationResult validationResult = _bookModelValidator.Validate(model);

            if (model is null || string.IsNullOrEmpty(model.Name) || !validationResult.IsValid)
            {
                _logger.LogError("ERROR 400 [BookController (CreateAsync)]: [ModelStateError] Invalid model.");
                return NotFound("Invalid model.");
            }

            BookDto? bookMap = _mapper.Map<BookDto>(model);

            if (bookMap is null)
            {
                _logger.LogError("ERROR 400 [BookController (CreateAsync)]: [MapperError] An error occurred while book mapping.");
                return BadRequest("An error occurred while book mapping.");
            }

            Guid bookId = await _bookService.CreateAsync(bookMap);

            if (bookId.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [BookController (CreateAsync)]: [RepositoryError] Incorrect data.");
                return NotFound("Incorrect file path occured.");
            }

            return Ok(bookId);
        }

        [HttpDelete("DeleteBook/{bookId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteAsync(Guid bookId)
        {
            if (bookId.Equals(Guid.Empty) || !ModelState.IsValid)
            {
                _logger.LogError("ERROR 404 [BookController (DeleteAsync)]: [ModelStateError] Book not found or invalid model state.");
                return NotFound("Book not found or invalid model state.");
            }

            bool isDeleted = await _bookService.DeleteAsync(bookId);

            if (!isDeleted)
            {
                _logger.LogError("ERROR 400 [BookController (DeleteAsync)]: [ModelStateError] Book can not be deleted.");
                return BadRequest("Book can not be deleted.");
            }

            return NoContent();
        }
    }
}
