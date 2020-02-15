using br.com.segfy.youtube.domain.dto;
using br.com.segfy.youtube.domain.interfaces;
using br.com.segfy.youtube.infra;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace br.com.segfy.youtube.api
{
    public class YoutubeApi : IYoutubeApi
    {
        private readonly YouTubeService _youtube;

        public YoutubeApi(IOptions<AppSettings> options)
        {
            _youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = options.Value.YoutubeKey
            });
        }
        public IEnumerable<YoutubeDTO> Search(string filter)
        {
            try
            {
                return search(filter);   
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IEnumerable<YoutubeDTO> search(string filter)
        {
            SearchResource.ListRequest listRequest = _youtube.Search.List("snippet");
            listRequest.Q = filter;
            listRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;

            SearchListResponse searchResponse = listRequest.Execute();

            foreach (SearchResult searchResult in searchResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    yield return new YoutubeDTO
                    {
                        Filter = filter,
                        Title = searchResult.Snippet.Title,
                        Description = searchResult.Snippet.Description,
                        Url = searchResult.Snippet.Thumbnails.Default__.Url,
                        Id = searchResult.Id.VideoId,
                    };
                }
            }
        }
    }
}