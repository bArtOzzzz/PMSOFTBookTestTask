using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Entities;
using PMSOFTBookTestTask.Service.Extensions;
using PMSOFTBookTestTask.Service.Abstract;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PMSOFTBookTestTask.Service.Dto;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace PMSOFTBookTestTask.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationService(IAuthenticationRepository authenticationRepository,
                                     IConfiguration configuration,
                                     IMapper mapper)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _authenticationRepository.GetByEmailAsync(email);
            var userMap = _mapper.Map<UserDto>(user);

            return userMap;
        }

        public async Task<UserDto?> GetByRefreshTokenAsync(string refreshToken)
        {
            var user = await _authenticationRepository.GetByRefreshTokenAsync(refreshToken);
            var userMap = _mapper.Map<UserDto>(user);

            return userMap;
        }

        public async Task<string?> GetRefreshTokenByUserAsync(Guid userId)
        {
            return await _authenticationRepository.GetRefreshTokenByUserAsync(userId);
        }

        public async Task<string?> CreateAccessTokenAsync(UserDto user)
        {
            if (user != null)
            {
                RoleEntity? currentRole = await _authenticationRepository.GetByIdAsync(user.RoleId);

                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.GivenName, user.Username!),
                    new Claim(ClaimTypes.Role, currentRole!.Role!),
                    new Claim(ClaimTypes.Dns, await GetIPAdress.GetIPAsync()),
                    new Claim("macAdress", await MacAddressReader.GetMACAsync())
                };

                JwtSecurityToken token = new(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:LifeTime"]!)),
                    signingCredentials: new SigningCredentials(
                                        new SymmetricSecurityKey(
                                            Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!)),
                                            SecurityAlgorithms.HmacSha512Signature));

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        public async Task<string?> UpdateRefreshTokenAsync(Guid userId)
        {
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] randomNumber = new byte[32];
            rng.GetBytes(randomNumber);
            string refreshToken = Convert.ToBase64String(randomNumber);

            if (string.IsNullOrEmpty(refreshToken))
            {
                return null;
            }

            var result = await _authenticationRepository.UpdateRefreshTokenAsync(refreshToken, userId);

            return result;
        }

        public async Task<TokenDto?> ValidateTokenAsync(UserDto user, string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!)),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!)),
                ClockSkew = TimeSpan.Zero,
                NameClaimType = ClaimTypes.Email,
                RoleClaimType = ClaimTypes.Role,
                RequireSignedTokens = true
            };

            var principal = tokenHandler.ValidateToken(accessToken, validationParameters, out _);
            var credentials = new SigningCredentials(
                                    new SymmetricSecurityKey(
                                        Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!)),
                                        SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: principal.Claims.ToArray(),
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:LifeTime"]!)),
                signingCredentials: credentials);

            var dnsClaim = principal.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Dns))?.Value;
            var macAddressClaim = principal.Claims.FirstOrDefault(c => c.Type == "macAdress")?.Value;

            if (dnsClaim != await GetIPAdress.GetIPAsync() ||
                macAddressClaim != await MacAddressReader.GetMACAsync())
            {
                return null;
            }

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new TokenDto
            {
                AccessToken = jwtToken,
                RefreshToken = await UpdateRefreshTokenAsync(user.Id)
            };

            return response;
        }
    }
}
