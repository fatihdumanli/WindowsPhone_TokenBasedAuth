using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhone_TokenBasedAuth.Models;

namespace WindowsPhone_TokenBasedAuth.Services
{
    interface IAuthService
    {
        Task<Token> GetAccessToken(string username, string password);
        Task<bool> Logout(Token token);
    }
}
