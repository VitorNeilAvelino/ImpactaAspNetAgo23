using System.ComponentModel.DataAnnotations;

namespace ExpoCenter.Mvc.Models
{
    public class ParticipanteCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public List<EventoGridViewModel>? Eventos { get; set; }
    }
}