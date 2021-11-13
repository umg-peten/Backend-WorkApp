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
    public class ClockingController : ControllerBase
    {
        private readonly IClockingService clockingService;
        private readonly IEmployeeService employeeService;

        public ClockingController(IClockingService clockingService, IEmployeeService employeeService)
        {
            this.clockingService = clockingService;
            this.employeeService = employeeService;
        }

        [HttpPost]
        [Route("AddClocking")]
        public IActionResult AddClocking(ClockingModel clocking)
        {
            Response response = new Response();
            if (!employeeService.ExistEmployeeById(clocking.IdEmployee))
            {
                response.Status = -1;
                response.Message = "El usuario enviado no existe";
                return NotFound(response); ;
            }

            if (!clockingService.AddClocking(clocking.IdEmployee))
            {
                response.Status = -1;
                response.Message = "Ocurrió un error, si el problema persiste contacta al admin";
                return StatusCode(500, response); ;
            }

            response.Status = 1;
            response.Message = "Se ha registrado  el ingreso o salida correctamente";
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Response response = new Response();
            Object data = clockingService.GetClockingsById(id);

            if(data == null)
            {
                response.Status = 404;
                response.Message = "Usuario No encontrado";
                return NotFound(response);
            }
            else
            {
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
                return Ok(response);
            }


        }
    }
}
