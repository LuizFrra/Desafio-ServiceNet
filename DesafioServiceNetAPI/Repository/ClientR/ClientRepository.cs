using DesafioServiceNetAPI.Infra.Context;
using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
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
            var CepExist = desafioContext.CEP.Any(c => c.CepID == Client.CepId);

            if (!CepExist) return null;

            await desafioContext.AddAsync(Client);

            var result = await desafioContext.SaveChangesAsync();

            if (result == 1)
                return await Task.FromResult(Client);

            return await Task.FromResult<Client>(null);
        }

        public async Task<Client> DeleteAsync(int UserId, int ClientId)
        {
            var client = await desafioContext.Clients.FirstOrDefaultAsync(c => c.ClientID == ClientId && c.UserID == UserId);

            if (client == null)
                return null;

            desafioContext.Clients.Remove(client);
            var result = await desafioContext.SaveChangesAsync();

            if (result == 1)
                return await Task.FromResult(client);

            return await Task.FromResult<Client>(client);
        }

        public async Task<ICollection<ClientCard>> GetAllAsync(int UserId)
        {
            var clients = await desafioContext.Clients.Where(u => u.UserID == UserId).Select(x => new ClientCard()
            {
                ClientID = x.ClientID,
                Name = x.Name, 
                Address = x.Address + " " + x.NumberAddress, 
                PhoneNumber = x.PhoneNumber
            }).ToListAsync();

            if(clients != null)
            {
                return await Task.FromResult(clients);
            }

            return await Task.FromResult<ICollection<ClientCard>>(null);
        }

        public async Task<Client> GetByIdAsync(int UserId, int ClientId)
        { 
            var client = await desafioContext.Clients.Include(c => c.Cep).FirstOrDefaultAsync(c => c.ClientID == ClientId && c.UserID == UserId);

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

        public async Task<CEP> AddCep(CEP Cep)
        {
            var exist = await desafioContext.CEP.AnyAsync(c => c.CepID == Cep.CepID);

            if (exist) return await Task.FromResult(Cep);

            await desafioContext.AddAsync(Cep);
            await desafioContext.SaveChangesAsync();

            return await Task.FromResult(Cep);
        }
    }
}
