using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class Client : ClientRegister
    {
        public int ClientID { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
