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
            var songsEntity = model.Songs.Where(x => model.Songs.FirstOrDefault(x) == null).Select(x => _mapper.Map<Song>(x)).ToList();
            await _playListRepository.CreateAsync(playListEntity, songsEntity);
            model.Id = playListEntity.Id;
            return model;
        }

        public async Task DeletePlayList(int id)
        {
            await _playListRepository.DeleteAsync(id);
        }

        public async Task<PlayListModel> GetById(int id)
        {
            var model = await _playListRepository.GetByIdAsync(id);
            return _mapper.Map<PlayListModel>(model);
        }

        public Task<IEnumerable<PlayListModel>> GetPlayLists()
        {
            throw new NotImplementedException();
        }

        public Task<PlayListModel> Update(PlayListModel model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
