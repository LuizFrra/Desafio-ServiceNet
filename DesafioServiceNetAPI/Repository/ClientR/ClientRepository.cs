using DesafioServiceNetAPI.Infra.Context;
using DesafioServiceNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Repository.ClientR
{
    public class ClientRepository : IClientRepository<Client>
    {
        private DesafioContext desafioContext;

        public ClientRepository(DesafioContext desafioContext)
        {
            this.desafioContext = desafioContext;
        }

        public Client Add(Client Client)
        {
            throw new NotImplementedException();
        }

        public Client Delete(int ClientId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Client> GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Client Update(Client Client)
        {
            throw new NotImplementedException();
        }
    }
}
