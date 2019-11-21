using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Repository.Auth
{
    public interface IAuthRepository<T> where T : class
    {
        Task<T> AddAsync(T user);

        Task<bool> UserExistAsync(string email);

        Task<T> LoginAsync(string email, string password);

        IList<string> ObterErros();
    }
}
