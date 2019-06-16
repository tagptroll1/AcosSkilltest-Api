using System.Collections.Generic;
using System.Threading.Tasks;
using Acos.ProgrammingTask.Models;
using Acos.ProgrammingTask.Utils;
using Microsoft.EntityFrameworkCore;

namespace Acos.ProgrammingTask.Services
{
    public interface IPostitService
    {
        Task<Postit> GetById(int id);
        Task<List<Postit>> GetAll();
        Task<Postit> Create(Postit postit);
        Task Delete(int id);
        Task Update(Postit postit);
    }

    public class PostitService :IPostitService
    {
        
        private DatabaseContext _ctx;

        public PostitService (DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Postit> Create(Postit postit)
        {
            await _ctx.Postits.AddAsync(postit);
            await _ctx.SaveChangesAsync();

            return postit;
        }

        public async Task Delete(int id)
        {
            var note = await GetById(id);
            if (note != null)
            {
                _ctx.Postits.Remove(note);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Postit>> GetAll() => 
            await _ctx.Postits
                .Include(x => x.Todo)
                .ToListAsync();

        public async Task<Postit> GetById(int id) => 
            await _ctx.Postits
                .Include(x => x.Todo)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task Update(Postit postit)
        {
            var note = await GetById(postit.Id);

            note.Todo = postit.Todo;
            note.Color = postit.Color;
            note.X = postit.X;
            note.Y = postit.Y;

            _ctx.Postits.Update(note);
            await _ctx.SaveChangesAsync();
        }
    }
}