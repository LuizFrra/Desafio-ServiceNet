using DesafioServiceNetAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.JWT.Handler
{
    public interface IJwtHandler
    {
        TokenValidationParameters parameters { get; }

        JwtToken Create(User user);
    }
}
