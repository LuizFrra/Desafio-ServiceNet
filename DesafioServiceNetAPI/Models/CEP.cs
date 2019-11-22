using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class CEP
    {
        public CEP(string city, string state, int cepID)
        {
            City = city;
            State = state;
            CepID = cepID;
        }

        [Required(ErrorMessage = "Atributo City é Obrigatório.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Atributo State é Obrigatório")]
        public string State { get; set; }

        [Required(ErrorMessage = "Atributo CEP é Obrigatório")]
        [RegularExpression("^[0-9]{5}-?[\\d]{3}$", ErrorMessage = "CEP Inválido")]
        public int CepID { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
