using Businesslogic.Models;
using Businesslogic.UrlService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortURL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }


        [HttpPost]
        [Route("add-url")]
        public ActionResult<string> AddUrl(URLDto url)
        {
            if (url.LongUrl != null)
            {
                url = _urlService.AddUrl(url);
                return url.ShortUrl;               
            }
            return BadRequest("Dont try to add an invalid data !!!");
        }
    }
}
