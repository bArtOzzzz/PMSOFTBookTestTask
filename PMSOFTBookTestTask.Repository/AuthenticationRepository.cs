using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Entities;
using PMSOFTBookTestTask.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace PMSOFTBookTestTask.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;

        public AuthenticationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email!.Equals(email))
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync();
        }

        public async Task<UserEntity?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.Where(u => u.RefreshToken!.Equals(refreshToken))
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync();
        }

        public async Task<string?> GetRefreshTokenByUserAsync(Guid userId)
        {
            var currentUser = await _context.Users.Where(u => u.Id.Equals(userId))
                                                  .AsNoTracking()
                                                  .FirstOrDefaultAsync();

            return currentUser!.RefreshToken;
        }

        public async Task<string?> UpdateRefreshTokenAsync(string refreshToken, Guid userId)
        {
            var currentUser = await _context.Users.FindAsync(userId);

            currentUser!.RefreshToken = refreshToken;

            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();

            return currentUser.RefreshToken;
        }

        public async Task<RoleEntity?> GetByIdAsync(Guid roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }
    }
}
