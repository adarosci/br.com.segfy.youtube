using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using br.com.segfy.youtube.domain.dto;
using br.com.segfy.youtube.domain.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace br.com.segfy.youtube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IYoutubeService _service;

        public SearchController(IYoutubeService service)
        {
            _service = service;
        }
        [HttpGet()]
        public ActionResult<List<YoutubeDTO>> Get([FromQuery(Name = "filter")] string filter)
        {
            return Ok(_service.Search(filter));
        }

        [HttpGet("all")]
        public ActionResult<List<YoutubeDTO>> Get()
        {
            return Ok(_service.LoadAll());
        }
    }
}