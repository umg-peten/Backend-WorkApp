using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Overtime
    {
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int IdEmployee { get; set; }
        [Required]
        public int IdUser { get; set; }

    }
}
