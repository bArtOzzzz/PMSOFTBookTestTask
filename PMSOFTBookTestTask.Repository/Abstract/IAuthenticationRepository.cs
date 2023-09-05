using PMSOFTBookTestTask.Repository.Entities;

namespace PMSOFTBookTestTask.Repository.Abstract
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// GET
        /// Repository method. Gets user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="UserEntity"/></returns>
        Task<UserEntity?> GetByEmailAsync(string email);

        /// <summary>
        /// GET
        /// Repository method. Takes user by refresh token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns><see cref="UserEntity"/></returns>
        Task<UserEntity?> GetByRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// GET
        /// Repository method. Gets user's refresh token. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string?> GetRefreshTokenByUserAsync(Guid userId);

        /// <summary>
        /// PUT
        /// Repository method. Updates refresh token for user.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="userId"></param>
        /// <returns><see cref="string" /></returns>
        Task<string?> UpdateRefreshTokenAsync(string refreshToken, Guid userId);

        /// <summary>
        /// GET
        /// Repository method. Gets role by id.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns><see cref="RoleEntity"/></returns>
        Task<RoleEntity?> GetByIdAsync(Guid roleId);
    }
}
