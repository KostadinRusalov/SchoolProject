using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AnimeContext : IDbContext<Anime, int>
    {
        private readonly AnimeCollectionDbContext context;

        public AnimeContext()
        {
            context = new AnimeCollectionDbContext();
        }

        public AnimeContext(AnimeCollectionDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Anime anime)
        {
            try
            {
                Studio _studio = await context.Studios.FindAsync(anime.StudioId);
                if (_studio != null)
                {
                    anime.Studio = _studio;
                }

                List<Character> characters = new();
                foreach (Character character in anime.Characters)
                {
                    Character _character = await context.Characters.FindAsync(character.Id);
                    if (_character == null)
                    {
                        characters.Add(character);
                    }
                    else
                    {
                        characters.Add(_character);
                    }
                }
                anime.Characters = characters;

                context.Animes.Add(anime);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Anime> Read(int id)
        {
            try
            {
                Anime anime = await context.Animes
                    .Include(a => a.Studio)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (anime == null)
                {
                    throw new ArgumentNullException("There is no anime with that id!");
                }

                return anime;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Anime>> ReadAll()
        {
            try
            {
                return await context.Animes
                    .Include(a => a.Studio)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Anime anime)
        {
            try
            {
                Anime _anime = await Read(anime.Id);
                context.Entry(_anime).CurrentValues.SetValues(anime);
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
                Anime _anime = await Read(id);
                context.Animes.Remove(_anime);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
