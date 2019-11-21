using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.JWT
{
    public class JwtToken
    {
        public string Token { get; set; }

        public long Expires { get; set; }

        public string TokenType { get; set; }
    }
}
