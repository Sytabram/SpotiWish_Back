using System;
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
            model.Users = await GetUserById(newPLayList.UserId);
            model.Musics = await GetMusicById(newPLayList.MusicId);
            _context.PlayLists.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<User>> GetUserById(List<int> idList)
        {
            return await _context.Users
                .Include(x=>x.Playlists)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
               
                
        }
        public async Task<List<Music>> GetMusicById(List<int> idList)
        {
            return await _context.Musics
                .Include(x=>x.Albums)
                .Include(x=>x.Playlists)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
            
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
                .Include(x => x.Users)
                .ToListAsync();
        }

        public async Task<PlayList> GetSinglePlayList(int id)
        {
            var playlist = await _context.PlayLists
                .Include(x=>x.Users)
                .Include(x => x.Musics)
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return playlist;
        }

        public async Task<PlayList> UpdatePlayList(int id, CRUDPlayListDTO PlaylistToEdit)
        {
            var userListModel = new List<User>();
            var musicListModel = new List<Music>();
            
            if (PlaylistToEdit.UserId != null)
            {
                var userList = PlaylistToEdit.UserId.ToList();
                userListModel = await _context.Users.Where(x => userList.Contains(x.Id)).ToListAsync();
            }
            if (PlaylistToEdit.MusicId != null)
            {
                var musicList = PlaylistToEdit.MusicId.ToList();
                musicListModel = await _context.Musics.Where(x => musicList.Contains(x.Id)).ToListAsync();
            }
            
            var playList = await _context.PlayLists
                .Include(x=> x.Users)
                .Include(x=>x.Musics)
                .FirstAsync(x=>x.Id==id);
            playList.Descrition = PlaylistToEdit.Descrition;
            playList.Name = PlaylistToEdit.Name;

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