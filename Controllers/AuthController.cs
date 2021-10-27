using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;
using WorkApp.IServices;
using WorkApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkApp.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService)
        {
            this._authService = authService;
        }
        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Login(AuthDto auth)
        {
            Response resp = new Response();
            var user = _authService.Authentication(auth);

            if(user.Token != null)
            {
                resp.Status = 200;
                resp.Message = "Authenticacion Exitosa";
                resp.Data = user;
                return Ok(resp);

            }

            resp.Status = 400;
            resp.Message = "Usuario o contraseña incorrecta";
            return Unauthorized(resp);
        }

    }
}
