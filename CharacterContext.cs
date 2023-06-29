using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CharacterContext : IDbContext<Character, int>
    {
        private readonly AnimeCollectionDbContext context;

        public CharacterContext()
        {
            context = new AnimeCollectionDbContext();
        }

        public CharacterContext(AnimeCollectionDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Character character)
        {
            try
            {
                Anime _anime = await context.Animes.FindAsync(character.AnimeId);
                if (_anime != null)
                {
                    character.Anime = _anime;
                }

                context.Characters.Add(character);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Character> Read(int id)
        {
            try
            {
                Character character = await context.Characters
                    .Include(c => c.Anime)
                    .ThenInclude(a => a.Studio)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (character == null)
                {
                    throw new ArgumentNullException("There is no character with that id!");
                }

                return character;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Character>> ReadAll()
        {
            try
            {
                return await context.Characters
                    .Include(c => c.Anime)
                    .ThenInclude(a => a.Studio)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Character character)
        {
            try
            {
                Character _character = await Read(character.Id);
                context.Entry(_character).CurrentValues.SetValues(character);
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
                Character _character = await Read(id);
                context.Characters.Remove(_character);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
