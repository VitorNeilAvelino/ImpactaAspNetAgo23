using System.ComponentModel.DataAnnotations;

namespace ExpoCenter.Mvc.Models
{
    public class ParticipanteGridViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }

        [Display(Name = "CPF")]
        [DisplayFormat(DataFormatString = @"{0:000\.000\.000-00}")]
        public long? Cpf { get; set; }

        public bool Selecionado { get; set; }
    }
}