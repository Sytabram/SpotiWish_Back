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
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<int> DeletePlaylist(int id)
        {
            _context.PlayLists.Remove(await _context.PlayLists.FindAsync(id));
            await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();
        }

        public async Task<List<PlayList>> GetAllPlayList()
        {
            return await _context.PlayLists
                .Include(x => x.Musics)
                .ToListAsync();
        }

        public async Task<PlayList> GetSinglePlayList(int id)
        {
            var playlist = await _context.PlayLists
                .Include(x => x.Musics)
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return playlist;
        }

        public async Task<PlayList> UpdatePlayList(int id, CRUDPlayListDTO PlaylistToEdit)
        {
            var model = await _context.PlayLists.FindAsync(id);
            model.Descrition = PlaylistToEdit.Descrition;
            model.Name = PlaylistToEdit.Name;

            await _context.SaveChangesAsync();
            return await GetSinglePlayList(id);
        }

        public async Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail)
        {
            var SpotiDB = _context.PlayLists.Find(id);
            SpotiDB.Thumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetThumbnailPlayList(int id)
        {
            return (await _context.PlayLists.FindAsync(id)).Thumbnail;
        }

        public Task<bool> ExistById(int id)
        {
            return _context.PlayLists.AnyAsync(u => u.Id == id);
        }
    }
}