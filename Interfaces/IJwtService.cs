using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Interfaces
{
    public interface IJwtService
    {
        string GetUserIdFromToken(string jwtToken);

    }
}