using Microsoft.EntityFrameworkCore;
using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Context;
using PMSOFTBookTestTask.Repository.Entities;

namespace PMSOFTBookTestTask.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorEntity>?> GetAsync()
        {
            return await _context.Authors.AsNoTracking()
                                         .ToListAsync();
        }
    }
}
