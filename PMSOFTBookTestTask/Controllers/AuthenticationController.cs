using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Models.Response;
using Microsoft.AspNetCore.Authorization;
using PMSOFTBookTestTask.Models.Request;
using PMSOFTBookTestTask.Service.Dto;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using FluentValidation;
using AutoMapper;

namespace PMSOFTBookTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IValidator<LoginModel> _loginValidator;
        private readonly IValidator<TokensModel> _tokensValidator;

        public AuthenticationController(IAuthenticationService authenticationService,
                                        IMapper mapper,
                                        ILogger<AuthenticationController> logger,
                                        IValidator<LoginModel> loginValidator,
                                        IValidator<TokensModel> tokensValidator)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _logger = logger;
            _loginValidator = loginValidator;
            _tokensValidator = tokensValidator;
        }

        [HttpGet("GetRefreshTokenByUser")]
        [AllowAnonymous]
        public async Task<ActionResult> GetRefreshTokenByUserAsync(Guid userId)
        {
            if (userId.Equals(Guid.Empty) || !ModelState.IsValid)
            {
                _logger.LogError("ERROR 400 [AuthenticationController (GetRefreshTokenByUserAsync)]: [ModelStateError] An error occurred while getting refresh token by user id.");
                return BadRequest("An error occurred while getting refresh token by user id.");
            }

            string? refreshToken = await _authenticationService.GetRefreshTokenByUserAsync(userId);

            if (string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogError("ERROR 404 [AuthenticationController (GetRefreshTokenByUserAsync)]: [ModelStateError] Refresh token or user not found.");
                return NotFound("Refresh token or user not found");
            }

            return Ok(refreshToken);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync(LoginModel userLogin)
        {
            ValidationResult validationResult = _loginValidator.Validate(userLogin);

            if (string.IsNullOrEmpty(userLogin.Email) ||
                string.IsNullOrEmpty(userLogin.Password) ||
                !validationResult.IsValid)
            {
                _logger.LogError("ERROR 404 [AuthenticationController (LoginAsync)]: [ModelStateError] An error occurred while login.");
                return NotFound("An error occurred while login.");
            }

            UserDto? loggedInUser = await _authenticationService.GetByEmailAsync(userLogin.Email);

            if (loggedInUser is null)
            {
                _logger.LogError("ERROR 404 [AuthenticationController (LoginAsync)]: [ServiceError] An error occurred while authentication.");
                return NotFound("An error occurred while authentication.");
            }
            else if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, loggedInUser!.Password))
            {
                _logger.LogError("ERROR 401 [AuthenticationController (LoginAsync)]: [ServiceError] An error occurred while authentication. Username or password is incorrect.");
                return Unauthorized("An error occurred while authentication. Username or password is incorrect.");
            }

            string? accessToken = await _authenticationService.CreateAccessTokenAsync(loggedInUser);
            string? refreshToken = await _authenticationService.UpdateRefreshTokenAsync(loggedInUser.Id);

            if (accessToken is null || refreshToken is null)
            {
                _logger.LogError("ERROR 401 [AuthenticationController (LoginAsync)]: [ServiceError] An error occurred while authentication. Access denied!. Refresh or Access tokens is null.");
                return Unauthorized("An error occurred while authentication. Access denied!. Refresh or Access tokens is null.");
            }

            var tokens = new TokensResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return Ok(tokens);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshTokenAsync(TokensModel model)
        {
            ValidationResult validationResult = _tokensValidator.Validate(model);

            if (model is null ||
                string.IsNullOrEmpty(model.AccessToken) ||
                string.IsNullOrEmpty(model.RefreshToken) ||
                !validationResult.IsValid)
            {
                _logger.LogError("ERROR 400 [AuthenticationController (RefreshTokenAsync)]: [ModelStateError] An error occurred while obtain refresh token.");
                return BadRequest("An error occurred while obtain refresh token.");
            }

            UserDto? currentUser = await _authenticationService.GetByRefreshTokenAsync(model.RefreshToken!);

            if (currentUser is null)
            {
                _logger.LogError("ERROR 400 [AuthenticationController (RefreshTokenAsync)]: [ServiceError] An error occurred while authentication.");
                return BadRequest("An error occurred while authentication.");
            }

            TokensResponse? response = _mapper.Map<TokensResponse>(await _authenticationService.ValidateTokenAsync(currentUser, model.AccessToken!));

            if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
            {
                _logger.LogError("ERROR 400 [AuthenticationController (RefreshTokenAsync)]: [ServiceError] An error occurred while mapping token models.");
                return BadRequest("An error occurred while mapping token models.");
            }

            return Ok(response.AccessToken);
        }
    }
}
