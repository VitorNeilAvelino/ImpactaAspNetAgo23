using System;

namespace AspNet.Capitulo02.Razor.Models
{
    public class ComentarioViewModel
    {
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public string Comentario { get; set; }
    }
}