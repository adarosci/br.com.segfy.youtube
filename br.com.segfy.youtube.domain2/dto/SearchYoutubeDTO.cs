using System;
using System.Collections.Generic;
using System.Text;

namespace br.com.segfy.youtube.domain.dto
{
    public class SearchYoutubeDTO
    {
        public string Id
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        public string Filter { get; set; }
    }
}
