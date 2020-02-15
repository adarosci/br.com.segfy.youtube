using br.com.segfy.youtube.domain.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace br.com.segfy.youtube.domain.interfaces
{
    public interface IYoutubeApi
    {
        IEnumerable<YoutubeDTO> Search(string filter);
    }
}
