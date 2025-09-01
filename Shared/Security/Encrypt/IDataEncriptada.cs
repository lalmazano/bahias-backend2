using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security.Encrypt
{
    public interface IDataEncriptada
    {
        public string EncriptarData(string data);
        public string DesencriptarData(string data);
        bool VerifyPassword(string plainPassword, string encryptedPassword);
    }
}
