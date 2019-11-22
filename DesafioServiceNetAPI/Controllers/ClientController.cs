using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosaicoSolutions.ViaCep;

namespace DesafioServiceNetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private IViaCepService viaCepService;

        public ClientController(IViaCepService viaCepService)
        {
            this.viaCepService = viaCepService;
        }

        [HttpPost]
        public async Task<IActionResult> ObterEndereco([FromBody] string CEP)
        {
            if (string.IsNullOrEmpty(CEP))
                return BadRequest("Atributo CEP é obrigatório.");

            Regex rgx = new Regex("^[0-9]{5}-?[\\d]{3}$");
            if (!rgx.IsMatch(CEP))
                return BadRequest("CEP Inválido");

            string jsonEndereco = null;
            try
            {
                jsonEndereco = await viaCepService.ObterEnderecoComoJsonAsync(CEP.Replace("-", ""));
            }
            catch (CepInexistenteException e)
            {
                jsonEndereco = e.Message;
                return BadRequest(jsonEndereco);
            }

            return Ok(jsonEndereco);
        }
    }
}