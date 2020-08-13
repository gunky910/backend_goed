using angularAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_herexamen.Services
{
    public interface IUserService
    {
        Gebruiker Authenticate(string email, string wachtwoord);
    }
}
