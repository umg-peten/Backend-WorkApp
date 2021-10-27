using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _host;

        public ImagesController(IWebHostEnvironment host)
        {
            this._host = host;
        }
        [HttpGet]
        public IActionResult GetImage()
        {
            string rutaPrincipal = _host.WebRootPath;
            string subida = Path.Combine(rutaPrincipal, @"image-profile");
            string rutaFinal = Path.Combine(subida, "profile.jpg");

            return Ok(rutaFinal);
        }
    }
}
