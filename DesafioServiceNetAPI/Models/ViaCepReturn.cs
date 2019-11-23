using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class ViaCepReturn
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }

        public CEP getCEP()
        {
            if (!string.IsNullOrEmpty(cep))
            {
                return new CEP(localidade, uf, Convert.ToString(cep));
            }
            return null;
        }

    }
}
