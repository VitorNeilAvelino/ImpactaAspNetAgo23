using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpoCenter.Mvc.Models
{
    public class EventoViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Required]
        //[DataType(DataType.Date)]
        public DateTime Data { get; set; }
        
        [Required]
        public string Local { get; set; }
        
        [Required]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }
    }
}
