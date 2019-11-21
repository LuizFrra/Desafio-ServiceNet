using DesafioServiceNetAPI.Infra.Context;
using DesafioServiceNetAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Repository.Auth
{
    public class AuthRepository : IAuthRepository<User>
    {
        private DesafioContext desafioContext;

        private IList<string> erros;

        public AuthRepository(DesafioContext desafioContext)
        {
            erros = new List<string>();
            this.desafioContext = desafioContext;
        }

        public async Task<User> AddAsync(User user)
        {
            bool userAlreadyExist = await UserExistAsync(user.Email);
            
            if(userAlreadyExist)
            {
                erros.Add("Este Email já está cadastrado.");
                return await Task.FromResult<User>(null);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await desafioContext.Users.AddAsync(user);
            int result = await desafioContext.SaveChangesAsync();

            user.Password = null;

            if (result == 1)
                return await Task.FromResult(user);

            erros.Add("Ocorreu Algum Erro Ao Cadastrar Usuario");

            return await Task.FromResult<User>(null);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await desafioContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            var IsPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(IsPasswordCorrect)
            {
                user.Password = null;
                return await Task.FromResult(user);
            }

            erros.Add("Usuario Ou Senha Incorreto.");
            return await Task.FromResult<User>(null);
        }

        public IList<string> ObterErros()
        {
            return erros;
        }

        public async Task<bool> UserExistAsync(string email)
        {
            var user = await desafioContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                erros.Add("Usuario Não Encontrado.");
                return false;
            }

            return true ;
        }
    }
}
