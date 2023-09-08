using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Entities;
using PMSOFTBookTestTask.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace PMSOFTBookTestTask.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;  
        }

        public async Task<List<GenreEntity>?> GetAsync()
        {
            return await _context.Genres.AsNoTracking()
                                        .ToListAsync();
        }
    }
}
