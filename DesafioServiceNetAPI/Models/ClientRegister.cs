using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class ClientRegister
    {
        [Required(ErrorMessage = "Atributo Name é Obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Atributo PhoneNumber é Obrigatório")]
        [RegularExpression("^(\\+[0-9]{2,3})?((\\([1-9]{2}\\))|([1-9]{2}))( ?9?[0-9]{4}-?[0-9]{4})$", ErrorMessage = "Telefone Inválido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Atributo Address é Obrigatório")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Atributo NumberAddress é Obrigatório")]
        public string NumberAddress { get; set; }

        [Required(ErrorMessage = "Atributo Country é Obrigatório")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Atributo CEP é Obrigatório")]
        [RegularExpression("^[0-9]{5}-?[\\d]{3}$", ErrorMessage = "CEP Inválido")]
        public int CepId { get; set; }

        public Client getClient(int UserId)
        {
            return new Client(Name, PhoneNumber, Address, NumberAddress, Country, CepId, UserId);
        }
    }
}
