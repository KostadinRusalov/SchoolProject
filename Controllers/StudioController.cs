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
    public class StudioController : Controller
    {
        private readonly StudioContext context;

        public StudioController(AnimeCollectionDbContext context)
        {
            this.context = new(context);
        }

        // GET: Studio
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAll());
        }

        // GET: Studio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await context.Read((int)id);
            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // GET: Studio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Headquarters,Founded")] Studio studio)
        {
            if (ModelState.IsValid)
            {
                await context.Create(studio);
                return RedirectToAction(nameof(Index));
            }
            return View(studio);
        }

        // GET: Studio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await context.Read((int)id);
            if (studio == null)
            {
                return NotFound();
            }
            return View(studio);
        }

        // POST: Studio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Headquarters,Founded")] Studio studio)
        {
            if (id != studio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.Update(studio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await StudioExists(studio.Id))
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
            return View(studio);
        }

        // GET: Studio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await context.Read((int)id);
            if (studio == null)
            {
                return NotFound();
            }

            return View(studio);
        }

        // POST: Studio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudioExists(int id)
        {
            return (await context.ReadAll()).Any(e => e.Id == id);
        }
    }
}
