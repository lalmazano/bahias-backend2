using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security.JWT.Interface
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
