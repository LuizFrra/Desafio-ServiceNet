using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Repository.ClientR
{
    public interface IClientRepository<T>
    {
        T Add(T Client);

        T Update(T Client);

        T Delete(int ClientId);

        T GetById(int Id);

        ICollection<T> GetByName(string Name);

        ICollection<T> GetAll();
    }
}
