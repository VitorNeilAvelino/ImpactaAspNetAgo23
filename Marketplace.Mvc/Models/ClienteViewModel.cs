using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marketplace.Mvc.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "O campo Nome pode ter no máximo 60 caracteres.", MinimumLength = 10)]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}.\d{3}.\d{3}-\d{2}$")]
        public string Documento { get; set; }

        [Required]
        //[Phone]
        [StringLength(20, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres.")] // {0}: nome do campo - {1}: máximo - {2}: mínimo
        public string Telefone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}