using Aplicacion.DTO.SCode;
using Aplicacion.Servicio.SCode.IServicios;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode.ServiciosImpl
{
    public class TokenServiceImpl : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenServiceImpl(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string GenerateToken(UserDto user,int Id)
        {
            var handler = new JwtSecurityTokenHandler();
            var t = _jwtSettings.Key;
            var key = Encoding.ASCII.GetBytes(t);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("Id",Id.ToString()),
                    new Claim("Email",user.Email)
                }),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiryInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var t = _jwtSettings.Key;
            //var key = Encoding.UTF8.GetBytes(t);
            //var credentials = new SigningCredentials(
            //  new SymmetricSecurityKey(key),
            //  SecurityAlgorithms.HmacSha256);
            //var claimsUser = new[]
            //{
            //        new Claim(ClaimTypes.Name, user.Username),
            //        new Claim("Id",Id.ToString()),
            //        new Claim("Email",user.Email)
            //};
            //var jwtconfig = new JwtSecurityToken(
            //        claims: claimsUser,
            //        expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiryInDays),
            //        signingCredentials: credentials
            //    );
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Username),
            //        new Claim("Id",Id.ToString()),
            //        new Claim("Email",user.Email)
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiryInDays),
            //    SigningCredentials = credentials
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return new JwtSecurityTokenHandler().WriteToken(jwtconfig);
        }
    }
}
