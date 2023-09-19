using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ExpoCenter.Mvc.Models
{
    public class EventoGridViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        public DateTime Data { get; set; }

        public string? Local { get; set; }

        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public bool Selecionado { get; set; }
    }
}