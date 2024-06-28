using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Scarpe___Co.Models;
using Scarpe___Co.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Scarpe___Co.Controllers
{
    public class ArticoliController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticoliController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var articoli = ArticoloRepository.GetAll();
            return View(articoli);
        }

        public IActionResult Dettagli(int id)
        {
            var articolo = ArticoloRepository.GetById(id);
            if (articolo == null)
            {
                return NotFound();
            }
            return View(articolo);
        }

        public IActionResult Nuovo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuovo(Articolo articolo)
        {
            if (ModelState.IsValid)
            {
                // Salva le immagini caricate nel file system solo se sono state fornite
                if (articolo.FileImmagineCopertina != null)
                    articolo.ImmagineCopertina = await SalvaImmagine(articolo.FileImmagineCopertina);

                if (articolo.FileImmagineAggiuntiva1 != null)
                    articolo.ImmagineAggiuntiva1 = await SalvaImmagine(articolo.FileImmagineAggiuntiva1);

                if (articolo.FileImmagineAggiuntiva2 != null)
                    articolo.ImmagineAggiuntiva2 = await SalvaImmagine(articolo.FileImmagineAggiuntiva2);

                // Aggiungi l'articolo al repository
                ArticoloRepository.AggiungiArticolo(articolo);

                return RedirectToAction(nameof(Index));
            }

            return View(articolo);
        }

        private async Task<string> SalvaImmagine(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Salva il file nell'upload folder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + uniqueFileName; 
        }
    }
}