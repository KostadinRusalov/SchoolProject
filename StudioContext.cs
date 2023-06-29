using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class StudioContext : IDbContext<Studio, int>
    {
        private readonly AnimeCollectionDbContext context;

        public StudioContext()
        {
            context = new AnimeCollectionDbContext();
        }

        public StudioContext(AnimeCollectionDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Studio studio)
        {
            try
            {
                List<Anime> animes = new();
                foreach (Anime anime in studio.Animes)
                {
                    Anime _anime = await context.Animes.FindAsync(anime.Id);
                    if (_anime == null)
                    {
                        animes.Add(anime);
                    }
                    else
                    {
                        animes.Add(_anime);
                    }
                }
                studio.Animes = animes;

                context.Studios.Add(studio);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Studio> Read(int id)
        {
            try
            {
                Studio studio = await context.Studios
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (studio == null)
                {
                    throw new ArgumentNullException("There is no studio with that id!");
                }

                return studio;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Studio>> ReadAll()
        {
            try
            {
                return await context.Studios.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Studio studio)
        {
            try
            {
                Studio _studio = await Read(studio.Id);
                context.Entry(_studio).CurrentValues.SetValues(studio);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Studio _studio = await Read(id);
                context.Studios.Remove(_studio);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
