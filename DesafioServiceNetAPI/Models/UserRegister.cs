using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Nome do usuario não pode ser vazio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email do usuario não pode ser vazio.")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha do usuario não pode ser vazia.")]
        public string Password { get; set; }
    }
}
