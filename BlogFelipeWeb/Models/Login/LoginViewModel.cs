using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogFelipeWeb.Models.Login
{
    public class LoginViewModel
    {
        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo Login é obrigatório.")]        
        public string sLogin { get; set; }
        
        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é Obrigatório")]
        [DataType(DataType.Password)]        
        public string sSenha { get; set; }

        [Display(Name = "Lembrar?")]
        public bool Lembrar { get; set; }
    }
}