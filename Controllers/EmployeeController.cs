using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this._hostEnviroment = hostEnvironment;
            this._employeeService = employeeService;
            this._httpContextAccessor = httpContextAccessor;
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

        [HttpGet]
        [Route("GetPDF")]
        public IActionResult GetPdf()
        {
            var rutaPrincipal = _hostEnviroment.WebRootPath;
            string rutaImage = Path.Combine(rutaPrincipal, "image-profile", "profile.jpg");
            EmployeeDto employee = new EmployeeDto
            {
                Name = "Axel",
                LastName = "Aguilar",
                Dpi = "324486121801",
                Position = new Position
                {
                    name = "Analista Programador",
                    Department = new Department
                    {
                        name = "Gerencia de Sistemas"
                    }
                }
            };

            string fileName = Guid.NewGuid().ToString() + ".pdf";

            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(130,206);
            Document doc = new Document(rectangle, 0, 0,0,0);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.Combine(rutaPrincipal, "public", fileName), FileMode.Create));
            doc.AddTitle("Primer PDF");
            doc.Open();
            Image img = Image.GetInstance(rutaImage);
            img.Border = 40;
            img.BorderWidth = 0;
            img.BorderWidthTop = 1;
            img.Alignment = Element.ALIGN_CENTER;
            img.ScalePercent(0.1f * 100);
            doc.Add(img);
            



            BarcodeQRCode barcode = new BarcodeQRCode(employee.Dpi, 65,65,null);
            Image codeQrImage = barcode.GetImage();
            //doc.Add(codeQrImage);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            Paragraph frase = new Paragraph();
            frase.SpacingAfter = 0;
            frase.Alignment = Element.ALIGN_CENTER;
            frase.Add(employee.Name + " " + employee.LastName);
            frase.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLUE);

            doc.Add(frase);
            doc.Add(Chunk.NEWLINE);
            frase.Clear();
            frase.Add(employee.Position.name);
            doc.Add(frase);


            codeQrImage.Alignment = Element.ALIGN_CENTER;
            doc.Add(codeQrImage);
            doc.Close();
            writer.Close();

            //var subida = Path.Combine("image-profile", "profile.jpg");

            string urlActual = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            string rutaFinal = Path.Combine(urlActual, "public", fileName).Replace("\\", "/");
            return Ok(rutaFinal);

              

        }


        [HttpDelete("{id:int}", Name = "Delete")]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            Response resp = new Response();
            if (_employeeService.ExistEmployeeById(id))
            {
                bool sucess = _employeeService.DeleteEmployee(id);
                if (sucess)
                {
                    resp.Status = 200;
                    resp.Message = "Empleado eliminado exitosamente";
                    return Ok(resp);
                }
                else
                {
                    resp.Status = 500;
                    resp.Message = "Ocurrio un error al eliminar";
                    return Ok(resp);
                }
            }
            else
            {
                resp.Status = -1;
                resp.Message = "No existe algun empleado con el id {"+id+"}";
                return NotFound(resp);
            }

        }

    }
}
