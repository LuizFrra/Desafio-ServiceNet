using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.JWT.Keys
{
    public class PublicKey
    {
        private byte[] Modulus;

        private byte[] Exponent;

        public PublicKey(IConfiguration configuration)
        {
            Modulus = Convert.FromBase64String(configuration.GetSection("PublicKey:Modulus").Value);
            var test = configuration.GetSection("PublicKey:Exponent").Value;
            Exponent = Convert.FromBase64String(configuration.GetSection("PublicKey:Exponent").Value);
        }

        public RSAParameters GetParameters()
        {
            RSAParameters parameters = new RSAParameters();
            parameters.Modulus = Modulus;
            parameters.Exponent = Exponent;

            return parameters;
        }
    }
}
