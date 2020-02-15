using br.com.segfy.youtube.domain.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace br.com.segfy.youtube.domain.interfaces
{
    public interface IYoutubeService
    {
        IEnumerable<YoutubeDTO> Search(string filter);
        IEnumerable<YoutubeDTO> LoadAll();
    }
}
