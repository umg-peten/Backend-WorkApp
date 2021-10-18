using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Activity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string creationDate { get; set; }
        public string expirationDate { get; set; }
        public int idUser { get; set; }
    }
}
