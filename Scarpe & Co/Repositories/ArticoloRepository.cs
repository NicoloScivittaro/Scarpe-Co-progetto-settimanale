using Scarpe___Co.Models;
using System.Collections.Generic;
using System.Linq;

namespace Scarpe___Co.Repositories
{
    public static class ArticoloRepository
    {
        private static List<Articolo> articoli = new List<Articolo>
        {
            new Articolo
            {
                Id = 1,
                Nome = "Scarpe Sportive",
                Prezzo = 79.99m,
                Descrizione = "Scarpe sportive comode e leggere.",
                ImmagineCopertina = "scarpe_sportive.jpg",
                ImmagineAggiuntiva1 = "scarpe_sportive_1.jpg",
                ImmagineAggiuntiva2 = "scarpe_sportive_2.jpg"
            },
            new Articolo
            {
                Id = 2,
                Nome = "Scarpe Running",
                Prezzo = 89.99m,
                Descrizione = "Scarpe da running per lunghe distanze.",
                ImmagineCopertina = "scarpe_running.jpg",
                ImmagineAggiuntiva1 = "scarpe_running_1.jpg",
                ImmagineAggiuntiva2 = "scarpe_running_2.jpg"
            },
            new Articolo
            {
                Id = 3,
                Nome = "Scarpe Casual",
                Prezzo = 59.99m,
                Descrizione = "Scarpe casual per tutti i giorni.",
                ImmagineCopertina = "scarpe_casual.jpg",
                ImmagineAggiuntiva1 = "scarpe_casual_1.jpg",
                ImmagineAggiuntiva2 = "scarpe_casual_2.jpg"
            },
            
        };

        public static List<Articolo> GetAll() => articoli;
        public static Articolo GetById(int id) => articoli.FirstOrDefault(a => a.Id == id);

        public static void AggiungiArticolo(Articolo articolo)
        {
            articolo.Id = articoli.Count + 1;
            articoli.Add(articolo);
        }
    }
}