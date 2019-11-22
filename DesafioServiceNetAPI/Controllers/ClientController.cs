using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesafioServiceNetAPI.Infra.Context;
using DesafioServiceNetAPI.Models;
using DesafioServiceNetAPI.Repository.ClientR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosaicoSolutions.ViaCep;
using Newtonsoft.Json;

namespace DesafioServiceNetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private IViaCepService viaCepService;
        private IClientRepository<Client> clientRepository;

        public ClientController(IViaCepService viaCepService, IClientRepository<Client> clientRepository)
        {
            this.viaCepService = viaCepService;
            this.clientRepository = clientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ObterEndereco([FromBody] string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return BadRequest("Atributo CEP é obrigatório.");

            Regex rgx = new Regex("^[0-9]{5}-?[\\d]{3}$");
            if (!rgx.IsMatch(cep))
                return BadRequest("CEP Inválido");

            string jsonEndereco = null;
            try
            {
                jsonEndereco = await viaCepService.ObterEnderecoComoJsonAsync(cep.Replace("-", ""));
                
                var obj = JsonConvert.DeserializeObject<ViaCepReturn>(jsonEndereco);
                if (obj != null)
                {
                    var cepobj = obj.getCEP();
                    
                    if (cepobj != null)
                        await clientRepository.AddCep(cepobj);
                }
            }
            catch (CepInexistenteException e)
            {
                jsonEndereco = e.Message;
                return BadRequest(jsonEndereco);
            }

            return Ok(jsonEndereco);
        }

        public async Task<IActionResult> Add([FromBody]ClientRegister client)
        {
            if(ModelState.IsValid)
            {
                int UserId = -1;
                
                if (int.TryParse(HttpContext.User.FindFirst("user_id").Value, out UserId))
                {
                    var clientObj = client.getClient(UserId);
                    if(clientObj != null)
                    {
                        var result = await clientRepository.AddAsync(clientObj);
                        if(result != null)
                        {
                            return CreatedAtAction("Add", result);
                        }
                    }
                }
                return BadRequest("Error");
            }
            return BadRequest(ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
        }
    }
}