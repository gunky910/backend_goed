using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using angularAPI.Models;
using backend_herexamen.Helpers;
using backend_herexamen.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AngularAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly Context _databaseContext;

        public UserService(IOptions<AppSettings> appSettings, Context databaseContext)
        {
            _appSettings = appSettings.Value;
            _databaseContext = databaseContext;
        }

        public Gebruiker Authenticate(string email, string wachtwoord)
        {
            var user = _databaseContext.Gebruikers.SingleOrDefault(f => f.email == email && f.wachtwoord == wachtwoord);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication succesful so generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("gebruikerID", user.gebruikerID.ToString()),
                    new Claim("email", user.email.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.wachtwoord = null;

            return user;
        }
    }
}
