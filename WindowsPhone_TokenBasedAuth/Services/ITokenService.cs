using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhone_TokenBasedAuth.Models;

namespace WindowsPhone_TokenBasedAuth.Services
{
    public interface ITokenService
    {
        bool IsTokenExist();
        Token GetToken();
        bool IsValidToken(Token token);
        bool SaveToken(Token token);
        bool DeleteToken();
    }
}
