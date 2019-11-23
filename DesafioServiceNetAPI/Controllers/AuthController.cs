using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioServiceNetAPI.Facebook;
using DesafioServiceNetAPI.JWT.Handler;
using DesafioServiceNetAPI.Models;
using DesafioServiceNetAPI.Repository.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioServiceNetAPI.Controller
{
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IAuthRepository<User> authRepository;

        private IJwtHandler jwtHandler;

        private FaceBookHandler faceBookHandler;

        public AuthController(IAuthRepository<User> authRepository, IJwtHandler jwtHandler, FaceBookHandler faceBookHandler)
        {
            this.faceBookHandler = faceBookHandler;
            this.jwtHandler = jwtHandler;
            this.authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegister user)
        {
            if(ModelState.IsValid)
            {
                var userRegistered = await authRepository.AddAsync(new User(user.Name, user.Email, user.Password));

                if (userRegistered == null)
                    return BadRequest(authRepository.ObterErros());

                return CreatedAtAction("Register", userRegistered);
            }

            return BadRequest(ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLogin user)
        {
            var userLogged = await authRepository.LoginAsync(user.Email, user.Password);

            if (userLogged == null)
                return Unauthorized(authRepository.ObterErros());

            var token = jwtHandler.Create(userLogged);

            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> FaceBookLogin([FromBody]string UserToken)
        {
            await faceBookHandler.ValidToken(UserToken);
            return Ok();
        }

        public string oi()
        {
            return "Oi";
        }
    }
}
