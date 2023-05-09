using AutoMapper;
using MusicMarket.Api.DTO;
using MusicMarket.Core.Entities;

namespace MusicMarket.Api.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Music, MusicDto>();
            CreateMap<MusicDto, Music>();

            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistDto, Artist>();

            CreateMap<SaveMusicDto, Music>();
            CreateMap<Music,SaveMusicDto>();

            CreateMap<Artist,SaveArtistDto>();
            CreateMap<SaveArtistDto,Artist>();
        }
    }
}
