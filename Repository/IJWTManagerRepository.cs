using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer;

namespace JWTWebAuthentication.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(User users);
    }

}