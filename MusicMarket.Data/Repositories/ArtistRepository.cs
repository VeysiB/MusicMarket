using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MusicMarket.Core.Entities;
using MusicMarket.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private readonly MusicMarketDbContext _context;
        public ArtistRepository(MusicMarketDbContext context):base(context)
        {
            _context= context;
        }
        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            return await _context.Artists.Include(a => a.Musics).ToListAsync();
        }

        public Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return _context.Artists.Include(c=>c.Musics).SingleOrDefaultAsync(a=>a.Id == id);
        }
    }
}
