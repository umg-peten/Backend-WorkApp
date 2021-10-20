using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Models;

namespace WorkApp.Dtos
{
    public class AuthenticatedUserDto
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Username { get; set; }
        public string ImgProfile { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Position Position { get; set; }
        public Department Department { get; set; }
    }
}
