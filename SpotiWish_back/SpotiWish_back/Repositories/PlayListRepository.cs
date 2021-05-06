using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Data;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Repositories
{
    public class PlayListRepository : IPlayListRepository
    {
        private readonly SpotiWishDataContext _context;

        public PlayListRepository(SpotiWishDataContext context)
        {
            _context = context;
        }
        public async Task<PlayList> AddPlaylist(CRUDPlayListDTO newPLayList)
        {
            var model = new PlayList();
            model.Name = newPLayList.Name;
            model.Descrition = newPLayList.Descrition;
            model.CreatDate = newPLayList.CreatDate;
            _context.PlayLists.Add(model);
            
            return model;
        }
        public Task<int> DeletePlaylist(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<PlayList>> GetAllPlayList()
        {
            return await _context.PlayLists
                .Include(x => x.Musics)
                .Include(x => x.Artists)
                .ToListAsync();
        }

        public Task<PlayList> GetSinglePlayList(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PlayListDTO> UpdatePlayList(int id, PlayListDTO model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail)
        {
            throw new System.NotImplementedException();
        }
    }
}