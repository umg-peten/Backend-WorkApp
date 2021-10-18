﻿using Microsoft.AspNetCore.Http;
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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        [HttpPost]
        [Route("AddDepartment")]
        public IActionResult AddDepartment(Department department)
        {
            Response resp = new Response();
           
            if (_departmentService.AddDepartment(department))
            {
                resp.Status = 200;
                resp.Message = "Se ha creado exitosamente el departamento";
                resp.Data = department;
                return Ok(resp);
            }
            else
            {
                resp.Status = 500;
                resp.Message = "Ha ocurrido un error, si el problema persiste, contacta con el administrador del sistema";
                return StatusCode(500, resp);
            }
        }

    }
}
