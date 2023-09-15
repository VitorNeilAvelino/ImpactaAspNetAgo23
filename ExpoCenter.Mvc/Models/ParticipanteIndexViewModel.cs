using System.ComponentModel.DataAnnotations;

namespace ExpoCenter.Mvc.Models
{
    public class ParticipanteIndexViewModel
    {
        private string cpf;

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        [Display(Name = "CPF")]
        public string Cpf 
        { 
            get => long.Parse(cpf).ToString(@"000\.000\.000-00") ; 
            set => cpf = value; 
        }
        
        [Display(Name = "Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
