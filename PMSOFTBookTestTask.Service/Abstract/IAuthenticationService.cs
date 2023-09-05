using PMSOFTBookTestTask.Service.Dto;

namespace PMSOFTBookTestTask.Service.Abstract
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// GET
        /// Service method. Gets user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="UserEntity"/></returns>
        Task<UserDto?> GetByEmailAsync(string email);

        /// <summary>
        /// GET
        /// Service method. Takes user by refresh token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns><see cref="UserEntity"/></returns>
        Task<UserDto?> GetByRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// GET
        /// Service method. Gets user's refresh token. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string?> GetRefreshTokenByUserAsync(Guid userId);

        /// <summary>
        /// POST
        /// Service method. Creates accsess token.
        /// </summary>
        /// <param name="user"></param>
        /// <returns><see cref="string"/></returns>
        Task<string?> CreateAccessTokenAsync(UserDto user);

        /// <summary>
        /// PUT
        /// Service method. Updates refresh token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="userId"></param>
        /// <returns><see cref="string"/></returns>
        Task<string?> UpdateRefreshTokenAsync(Guid userId);

        /// <summary>
        /// Service method. Validates token.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accessToken"></param>
        /// <returns><see cref="TokenDto" /></returns>
        Task<TokenDto?> ValidateTokenAsync(UserDto user, string accessToken);
    }
}
