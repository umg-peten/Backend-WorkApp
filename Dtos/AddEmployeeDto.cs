using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Dtos
{
    public class AddEmployeeDto
    {
        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Se admiten mínimo 3 caracteres y un máximo de 64")]
        public string Name { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Se admiten mínimo 3 caracteres y un máximo de 64")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{8})$", ErrorMessage = "Solo admiten 8 valores númericos")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{13})$", ErrorMessage = "Ingrese un numero de dpi valido")]
        public string Dpi { get; set; }
        [Required]
        [RegularExpression(@"^([0-1]{1})$", ErrorMessage = "Ingrese un Id Valido: (0-Femenino),(1-Masculino)")]
        public int Sex { get; set; }
        [Required]
        public int IdPosition { get; set; }
        [Required]
        public double Salary { get; set; }
    }
}
