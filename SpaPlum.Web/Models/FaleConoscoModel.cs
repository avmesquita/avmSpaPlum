using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaPlum.Web.Models
{
    public class FaleConoscoModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O tamanho máximo são 100 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "E-Mail é obrigatório")]
        [StringLength(100, ErrorMessage = "O tamanho máximo são 100 caracteres")]
        [EmailAddress(ErrorMessage = "E-Mail inválido")]
        public string Email { get; set; }

        [Display(Name = "Mensagem")]
        [Required(ErrorMessage = "Mensagem é obrigatória")]
        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }
    }
}