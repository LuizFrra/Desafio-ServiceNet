using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace desafioAPI.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Propriedade Nome Deve Ser Preenchida.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Propriedade Email Deve Ser Preenchida.")]
        [EmailAddress(ErrorMessage = "Este Não É Um Email Válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Propriedade Senha Deve Ser Preenchida.")]
        [MinLength(8, ErrorMessage = "Sua Senha Deve Conter No Mínimo 8 Caractéres.")]
        public string Password { get; set; }
    }
}
