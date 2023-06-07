using Core.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey     _symmetricSecurityKey;
        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["token:key"]));
        }
        public string GetToken(AppUser user)
        {
            var claim = new List<Claim>{
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName,user.address.FirstName+ " "+user.address.LastName),
                
            };

            var cred = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var tokenDiscription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = cred,
                Issuer = _configuration["token:Issuer"],


            };
            var tokenHandeller = new JwtSecurityTokenHandler();
            var token = tokenHandeller.CreateToken(tokenDiscription);
            return tokenHandeller.WriteToken(token);


        }
    }
}
