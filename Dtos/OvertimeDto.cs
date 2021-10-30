using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Dtos
{
    public class OvertimeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Overtime { get; set; }
        public string AmountOfOvertime { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
