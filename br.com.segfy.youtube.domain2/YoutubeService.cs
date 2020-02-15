using br.com.segfy.youtube.domain.dto;
using br.com.segfy.youtube.domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace br.com.segfy.youtube.domain
{
    public class YoutubeService : IYoutubeService
    {
        private readonly IYoutubeApi _api;
        private readonly IRepository _repository;

        const string MOVIES = "movies";
        const string SEARCH = "search";

        public YoutubeService(IYoutubeApi api, IRepository repository)
        {
            _api = api;
            _repository = repository;
        }

        public IEnumerable<YoutubeDTO> LoadAll()
        {
            return _repository.Load<YoutubeDTO>(MOVIES);
        }

        public IEnumerable<YoutubeDTO> Search(string filter)
        {
            var search = _repository.Load<SearchYoutubeDTO>(x => x.Filter == filter, SEARCH);
            if (search.Any())
            {
                return _repository.Load<YoutubeDTO>(x => x.Filter == filter, MOVIES);
            }
            var movies = _api.Search(filter).ToList();
            Task.Run(() => SaveSearch(filter, movies));
            return movies;
        }

        private void SaveSearch(string filter, IEnumerable<YoutubeDTO> movies)
        {
            _repository.Save(new SearchYoutubeDTO { Filter = filter }, SEARCH);
            _repository.Save(movies, MOVIES);
        }
    }
}
