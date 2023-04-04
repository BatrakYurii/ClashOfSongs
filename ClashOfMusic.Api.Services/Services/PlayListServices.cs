using AutoMapper;
using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Services
{
    public class PlayListServices : IPlayListServices
    {

        private readonly IMapper _mapper;
        private readonly IPlayListRepository _playListRepository;

        public PlayListServices(IMapper mapper, IPlayListRepository playListRepository)
        {
            _mapper = mapper;
            _playListRepository = playListRepository;
        }

        public async Task<PlayListModel> Create(PlayListModel model)
        {
            var playListEntity = _mapper.Map<PlayList>(model);
            if (model.Songs.Count % 8 != 0)
                throw new Exception("Songs count must be 16,32,64,128 or 256");

            //var songsEntity = model.Songs.Select(x => _mapper.Map<Song>(x)).ToList();
            var playList = await _playListRepository.CreateAsync(playListEntity);
            return _mapper.Map<PlayListModel>(playList);
        }

        public async Task DeletePlayList(int id)
        {
            await _playListRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PlayListModel>> GetAllByUserId(string userId)
        {
            var playlists = await _playListRepository.GetAllByUserIdAsync(userId);
            var playListModels = playlists.Select(x => _mapper.Map<PlayListModel>(x)).ToList();
            return playListModels;
                
        }

        public async Task<PlayListModel> GetById(int id)
        {
            var model = await _playListRepository.GetByIdAsync(id);
            return _mapper.Map<PlayListModel>(model);
        }

        public async Task<IEnumerable<PlayListModel>> GetPlayLists()
        {
            var playListModels = await _playListRepository.GetAsync();
            return playListModels.Select(x => _mapper.Map<PlayListModel>(x)).ToList();
        }

        public Task<PlayListModel> Update(PlayListModel model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
