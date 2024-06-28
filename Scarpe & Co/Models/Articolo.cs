using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scarpe___Co.Models
{
    public class Articolo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        public string Descrizione { get; set; }

        public string? ImmagineCopertina { get; set; }
        public string? ImmagineAggiuntiva1 { get; set; }
        public string? ImmagineAggiuntiva2 { get; set; }

        [NotMapped]
        public IFormFile? FileImmagineCopertina { get; set; }

        [NotMapped]
        public IFormFile? FileImmagineAggiuntiva1 { get; set; }

        [NotMapped]
        public IFormFile? FileImmagineAggiuntiva2 { get; set; }
    }
}