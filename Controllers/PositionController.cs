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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            this._positionService = positionService;
        }

        [HttpGet("{id}")]
        [Route("GetByDepartment")]
        public IActionResult GetByDepartment(int id)
        {
            Response resp = new Response();
            var positions = _positionService.GetPositionByDepartment(id);
            if(positions.Count > 0)
            {
                resp.Status = 200;
                resp.Message = "Ok";
                resp.Data = positions;
                return Ok(resp);
            }
            else
            {
                resp.Status = 404;
                resp.Message = "Not Foud";
                resp.Data = null;
                return NotFound(resp);
            }


        }

    }
}
