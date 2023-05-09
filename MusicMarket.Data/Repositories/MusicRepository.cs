using Microsoft.EntityFrameworkCore;
using MusicMarket.Core.Entities;
using MusicMarket.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Data.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        private readonly MusicMarketDbContext _context;

        public MusicRepository(MusicMarketDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await _context.Musics.Include(m=>m.Artist).ToListAsync(); ;
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int id)
        {
            return await _context.Musics.Include(m=>m.Artist).Where(m=>m.ArtistId== id).ToListAsync(); ;
        }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await _context.Musics.Include(m => m.Artist).SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
