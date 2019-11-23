using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class Client : ClientRegister
    {
        public int ClientID { get; set; }

        public User User { get; set; }

        public int UserID { get; set; }

        public CEP Cep { get; set; }

        public Client(string name, string phoneNumber, string address, string numberAddress, string country, int cepId, int userId)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            NumberAddress = numberAddress;
            Country = country;
            UserID = userId;
            UserID = userId;
            CepId = cepId;
        }

        public Client()
        {

        }
    }
}
