using AutoMapper;
using SpotiWish_back.Model;

namespace SpotiWish_back.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            //playlist
            CreateMap<PlayList, SimplePlayListDTO>().ReverseMap();
            CreateMap<PlayList, PlayListDTO>().ReverseMap();

            CreateMap<Music, SimpleMusicDTO>().ReverseMap();
            CreateMap<Music, MusicDTO>().ReverseMap();
            
            CreateMap<Artist, SimpleArtistDTO>().ReverseMap();
            CreateMap<Artist, ArtistDTO>().ReverseMap();
            
            CreateMap<Album, SimpleAlbumDTO>().ReverseMap();
            
            CreateMap<User, SimpleUserDTO>().ReverseMap();
        }
    }
}