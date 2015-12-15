using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogFelipeWeb.Models.Administracao
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("ID Usuario")]
        [Required(ErrorMessage = "Campo Obrigatório")]        
        public int iId { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "A quantidade de caracteres no campo Login deve ser entre {2} e {1}.")]
        public string sLogin { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é Obrigatório.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "A quantidade de caracteres no campo Nome deve ser entre {2} e {1}")]
        public string sNome { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é Obrigatório")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "A quantidade de caracteres no campo Senha deve ser entre {2} e {1}")]
        public string sSenha { get; set; }
    }
}