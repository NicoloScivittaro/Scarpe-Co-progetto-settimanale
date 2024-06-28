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
                if (articolo.FileImmagineCopertina != null)
                {
                    articolo.ImmagineCopertina = await SalvaImmagine(articolo.FileImmagineCopertina);
                    if (articolo.ImmagineCopertina == null)
                    {
                        ModelState.AddModelError(string.Empty, "Errore nel salvataggio dell'immagine di copertina.");
                    }
                }

                if (articolo.FileImmagineAggiuntiva1 != null)
                {
                    articolo.ImmagineAggiuntiva1 = await SalvaImmagine(articolo.FileImmagineAggiuntiva1);
                    if (articolo.ImmagineAggiuntiva1 == null)
                    {
                        ModelState.AddModelError(string.Empty, "Errore nel salvataggio dell'immagine aggiuntiva 1.");
                    }
                }

                if (articolo.FileImmagineAggiuntiva2 != null)
                {
                    articolo.ImmagineAggiuntiva2 = await SalvaImmagine(articolo.FileImmagineAggiuntiva2);
                    if (articolo.ImmagineAggiuntiva2 == null)
                    {
                        ModelState.AddModelError(string.Empty, "Errore nel salvataggio dell'immagine aggiuntiva 2.");
                    }
                }

                if (ModelState.ErrorCount == 0)
                {
                    ArticoloRepository.AggiungiArticolo(articolo);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(articolo);
        }

        private async Task<string> SalvaImmagine(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            try
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return "/uploads/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}