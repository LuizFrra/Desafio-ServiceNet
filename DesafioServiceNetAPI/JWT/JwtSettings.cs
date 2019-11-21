using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.JWT
{
    public class JwtSettings
    {
        public int expiryMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
