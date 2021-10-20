using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;

namespace WorkApp.IServices
{
    public interface IAuthenticationService
    {
        public AuthenticatedUserDto Authentication(AuthDto auth);
    }
}
