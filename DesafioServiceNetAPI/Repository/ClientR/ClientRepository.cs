using DesafioServiceNetAPI.Infra.Context;
using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Client> AddAsync(Client Client)
        {
            await desafioContext.AddAsync(Client);
            var result = await desafioContext.SaveChangesAsync();

            if (result == 1)
                return await Task.FromResult(Client);

            return await Task.FromResult<Client>(null);
        }

        public async Task<Client> DeleteAsync(int ClientId)
        {
            var client = await desafioContext.Clients.FirstOrDefaultAsync(c => c.ClientID == ClientId);

            if (client == null)
                return null;

            desafioContext.Clients.Remove(client);
            var result = await desafioContext.SaveChangesAsync();

            if (result == 1)
                return await Task.FromResult(client);

            return await Task.FromResult<Client>(client);
        }

        public async Task<ICollection<Client>> GetAllAsync()
        {
            var clients = await desafioContext.Clients.ToListAsync();

            return clients == null ? await Task.FromResult<ICollection<Client>>(null) : await Task.FromResult(clients);
        }

        public async Task<Client> GetByIdAsync(int Id)
        {
            var client = await desafioContext.Clients.FirstOrDefaultAsync(c => c.ClientID == Id);

            return client == null ? await Task.FromResult<Client>(null) : await Task.FromResult(client);
        }

        public async Task<ICollection<Client>> GetByNameAsync(string Name)
        {
            var client = await desafioContext.Clients.Where(c => c.Name == Name).ToListAsync();

            return client == null ? await Task.FromResult<ICollection<Client>>(null) : await Task.FromResult(client);
        }

        public async Task<Client> UpdateAsync(Client Client)
        {
            desafioContext.Update(Client);
            var result = await desafioContext.SaveChangesAsync();

            return result == 1 ? await Task.FromResult(Client) : await Task.FromResult<Client>(null);
        }
    }
}
