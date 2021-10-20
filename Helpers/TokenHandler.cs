using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkApp.Dtos;
using WorkApp.IServices;

namespace WorkApp.Helpers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly JWT _jwt;
        public TokenHandler(IOptions<JWT> jwt)
        {
            this._jwt = jwt.Value;
        }

        public string GenerateToken(AuthenticatedUserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(

                    new Claim[]
                    {
                        new Claim("Id", user.IdUser.ToString()),
                        new Claim("Fullname", user.Name + user.LastName),
                        new Claim("Username", user.Username),
                        new Claim("Rol", "Admin"),
                        //new Claim("IdBranch", employee.Branch.IdBranch.ToString())
                        //new Claim(ClaimTypes.Email, usuario.email.ToString()),
                        //new Claim("rol", usuario.tipoUsuario.idTipoUsuario.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
