using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataAccessLayer;

namespace WebApplication.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AnimeContext context;

        private readonly StudioContext studioContext;
        public AnimeController(AnimeCollectionDbContext context)
        {
            this.context = new(context);
            studioContext = new();
        }

        // GET: Anime
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAll());
        }

        // GET: Anime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await context.Read((int)id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Anime/Create
        public IActionResult Create()
        {
            LoadStudios();
            return View();
        }

        // POST: Anime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Genre,StudioId")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                await context.Create(anime);
                return RedirectToAction(nameof(Index));
            }
            LoadStudios();
            return View(anime);
        }

        // GET: Anime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await context.Read((int)id);
            if (anime == null)
            {
                return NotFound();
            }
            LoadStudios();
            return View(anime);
        }

        // POST: Anime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,StudioId")] Anime anime)
        {
            if (id != anime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.Update(anime);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AnimeExists(anime.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            LoadStudios();
            return View(anime);
        }

        // GET: Anime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await context.Read((int)id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // POST: Anime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AnimeExists(int id)
        {
            return (await context.ReadAll()).Any(e => e.Id == id);
        }

        private async void LoadStudios() => ViewData["Studios"] = await studioContext.ReadAll();
    }
}
