using MusicMarket.Core;
using MusicMarket.Core.Entities;
using MusicMarket.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Services.Concrete
{
    public class MusicService : IMusicService
    {
        private readonly IUnitofWork _unitofWork;
        public MusicService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public async Task<Music> CreateMusic(Music newMusic)
        {
            await _unitofWork.Musics.AddAsync(newMusic);
            await _unitofWork.CommitAsync();
            return newMusic;
        }

        public async Task DeleteMusic(Music music)
        {
            _unitofWork.Musics.Remove(music);
            await _unitofWork.CommitAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtist()
        {
            return await _unitofWork.Musics.GetAllWithArtistAsync();
        }

        public async Task<IEnumerable<Music>> GetMusicByArtistId(int artistId)
        {
           return await _unitofWork.Musics.GetAllWithArtistByArtistIdAsync(artistId);
        }

        public async Task<Music> GetMusicById(int id)
        {
            return await _unitofWork.Musics.GetWithArtistByIdAsync(id);
        }

        public async Task UpdateMusic(Music musicToBeUpdated, Music music)
        {
            musicToBeUpdated.Name=music.Name;
            musicToBeUpdated.ArtistId=music.ArtistId;

            await _unitofWork.CommitAsync();
        }
    }
}
