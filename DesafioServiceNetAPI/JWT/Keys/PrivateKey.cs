using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.JWT.Keys
{
    public class PrivateKey
    {
        private byte[] Modulus;

        private byte[] Exponent;

        private byte[] P;

        private byte[] Q;

        private byte[] DP;

        private byte[] DQ;

        private byte[] InverseQ;

        private byte[] D;

        private RSAParameters parameters;

        public PrivateKey(IConfiguration configuration)
        {
            Modulus = Convert.FromBase64String(configuration.GetSection("PrivateKey:Modulus").Value);
            Exponent = Convert.FromBase64String((configuration.GetSection("PrivateKey:Exponent").Value));
            P = Convert.FromBase64String((configuration.GetSection("PrivateKey:P").Value));
            Q = Convert.FromBase64String((configuration.GetSection("PrivateKey:Q").Value));
            DP = Convert.FromBase64String(configuration.GetSection("PrivateKey:DP").Value);
            DQ = Convert.FromBase64String((configuration.GetSection("PrivateKey:DQ").Value));
            InverseQ = Convert.FromBase64String((configuration.GetSection("PrivateKey:InverseQ").Value));
            D = Convert.FromBase64String((configuration.GetSection("PrivateKey:D").Value));
        }

        public RSAParameters GetParameters()
        {
            parameters = new RSAParameters();
            parameters.Modulus = Modulus;
            parameters.Exponent = Exponent;
            parameters.P = P;
            parameters.Q = Q;
            parameters.DP = DP;
            parameters.DQ = DQ;
            parameters.InverseQ = InverseQ;
            parameters.D = D;

            return parameters;
        }

        private byte[] StringToBytes(string Value)
        {
            return Encoding.ASCII.GetBytes(Value);
        }
    }
}
