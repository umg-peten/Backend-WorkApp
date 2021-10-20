using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Dtos
{
    public class UpdateEmployeeDto : AddEmployeeDto
    {
        [Required]
        public int Id { get; set; }
    }
}
