using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OvertimeController : ControllerBase
    {
        private readonly IOvertimeService _overtime;
        public OvertimeController(IOvertimeService overtime)
        {
            this._overtime = overtime;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Overtime overtime)
        {
            int idUser = Int32.Parse(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
            overtime.IdUser = idUser;
            Response resp = new Response();
            if (_overtime.AddOvertime(overtime) > 0)
            {
                resp.Status = 200;
                resp.Message = "Tiempo extra agregado correctamente";
                return Ok(resp);
            }
            else
            {
                resp.Status = -1;
                resp.Message = "Ocurrió un error, si el problema persiste contacta al administrador";
                return Ok(resp);
            }

            
        }
        [HttpGet]
        [Route("get")]
        public IActionResult GetOvertime([FromQuery] int type, int? user, string? datebegin, string? dateend)
        {
           var lista =  _overtime.GetOvertime(type, user, datebegin, dateend);
           
            return Ok(lista);
        }
    }
}
