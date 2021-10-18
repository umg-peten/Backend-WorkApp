using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Configuration
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string companyTriburaryId { get; set; }
        public int CompanyCountry { get; set; }
        public int CompanyTimeZone { get; set; }
        public int CompanyCurrency { get; set; }
        
    }
}
