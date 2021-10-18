using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class LogsWSModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Section { get; set; }
        public bool Success { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }

        public LogsWSModel()
        {
            this.DateBegin = DateTime.Now;
        }
    }
}
