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
    public class CharacterController : Controller
    {
        private readonly CharacterContext context;

        private readonly AnimeContext animeContext;
        public CharacterController(AnimeCollectionDbContext context)
        {
            this.context = new(context);
            animeContext = new();
        }

        // GET: Character
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAll());
        }

        // GET: Character/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Read((int)id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            LoadAnimes();
            return View();
        }

        // POST: Character/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,AnimeId")] Character character)
        {
            if (ModelState.IsValid)
            {
                await context.Create(character);
                return RedirectToAction(nameof(Index));
            }
            LoadAnimes();
            return View(character);
        }

        // GET: Character/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Read((int)id);
            if (character == null)
            {
                return NotFound();
            }
            LoadAnimes();
            return View(character);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,AnimeId")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await context.Update(character);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CharacterExists(character.Id))
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
            LoadAnimes();
            return View(character);
        }

        // GET: Character/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Read((int)id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CharacterExists(int id)
        {
            return (await context.ReadAll()).Any(e => e.Id == id);
        }

        private async void LoadAnimes() => ViewData["Animes"] = await animeContext.ReadAll();
    }
}
