using DesafioServiceNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Repository.ClientR
{
    public interface IClientRepository<T>
    {
        Task<T> AddAsync(T Client);

        Task<T> UpdateAsync(int UserId, T Client);

        Task<T> DeleteAsync(int UserId, int ClientId);

        Task<T> GetByIdAsync(int UserId, int ClientId);

        Task<ICollection<T>> GetByNameAsync(string Name);

        Task<ICollection<ClientCard>> GetAllAsync(int UserId);

        Task<CEP> AddCepAsync(CEP Cep);
    }
}
