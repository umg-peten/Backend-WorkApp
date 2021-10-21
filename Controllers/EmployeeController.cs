using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var employee = _employeeService.GetAllEmployees();
            if (employee.Count > 0)
            {
                Response resp = new Response
                {
                    Status = 200,
                    Message = "Ok",
                    Data = employee
                };
                return Ok(resp);
            }
            else
            {
                Response resp = new Response
                {
                    Status = 404,
                    Message = "Not Found",
                    Data = employee
                };
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee.Id != 0)
            {
                Response resp = new Response
                {
                    Status = 200,
                    Message = "Ok",
                    Data = employee
                };
                return Ok(resp);
            }
            else
            {
                Response resp = new Response
                {
                    Status = 404,
                    Message = "Not Found",
                    Data = employee
                };
                return NotFound();
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [Route("Add")]
        public IActionResult AddEmployee(AddEmployeeDto employe)
        {
            Response resp = new Response();
            if (_employeeService.ExistDpi(employe.Dpi))
            {
                resp.Status = 400;
                resp.Message = "El dpi ya existe en la base de datos";
                return BadRequest(resp);
            }


            int idEmployee = _employeeService.AddEmployee(employe);

            if (idEmployee > 0) return Ok(new Response { Status = 200, Message = "Usuario creado exitosamente", Data = _employeeService.GetEmployeeById(idEmployee) });

            return StatusCode(500, new Response { Status = 500, Message = "Ocurrió un error al crear el usuario, si el problema persiste contacta al administrador del sistema" });

        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateEmployee(UpdateEmployeeDto employe)
        {
            Response resp = new Response();
            if (!_employeeService.ExistEmployeeById(employe.Id))
            {
                resp.Status = 404;
                resp.Message = "Usuario no encontrado";
                return NotFound(resp);
            }

            int rowsAffected = _employeeService.UpdateEmployee(employe);

            if (rowsAffected > 0)
            {
                resp.Status = 200;
                resp.Message = "Usuario Actualizado satisfactoriamente";
                resp.Data = _employeeService.GetEmployeeById(employe.Id);
                return Ok(resp);
            }

            return StatusCode(500, new Response { Status = 500, Message = "Ocurrió un error al Actualizar el usuario, si el problema persiste contacta al administrador del sistema" });

        }

    }
}
