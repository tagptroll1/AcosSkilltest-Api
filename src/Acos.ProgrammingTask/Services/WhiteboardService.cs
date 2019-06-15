using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acos.ProgrammingTask.Models;
using Acos.ProgrammingTask.Utils;
using Microsoft.EntityFrameworkCore;

namespace Acos.ProgrammingTask.Services
{
    public interface IWhiteboardService
    {
        Task<Whiteboard> GetById(int id);
        Task<IEnumerable<Whiteboard>> GetAll();
        Task<Whiteboard> Create(Whiteboard whiteboard);

        Task Update(Whiteboard whiteboard);
        Task Delete(int id);
        Task<List<Whiteboard>> GetAllFromUser(User user);

    }
    public class WhiteboardService : IWhiteboardService
    {
        private DatabaseContext _ctx;

        public WhiteboardService(DatabaseContext context)
        {
            _ctx = context;
        }

        public async Task<Whiteboard> Create(Whiteboard whiteboard)
        {
            if ( await _ctx.Whiteboards.AnyAsync(
                    w => w.Title == whiteboard.Title 
                    && w.Owner.Id == whiteboard.Owner.Id))
                throw new WhiteboardException("You already have a whiteboard with this title");

            await _ctx.Whiteboards.AddAsync(whiteboard);
            await _ctx.SaveChangesAsync();

            return whiteboard;
        }

        public async Task Delete(int id)
        {
            var board = await _ctx.Whiteboards.FindAsync(id);
            if (board != null)
            {
                _ctx.Whiteboards.Remove(board);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Whiteboard>> GetAll() =>
            await _ctx.Whiteboards.ToListAsync();

        public async Task<Whiteboard> GetById(int id) =>
            await _ctx.Whiteboards.FindAsync(id);

        public async Task Update(Whiteboard boardIn)
        {
            var board = await _ctx.Whiteboards.FindAsync(boardIn.Id);

            board.Title = boardIn.Title;

            _ctx.Whiteboards.Update(board);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Whiteboard>> GetAllFromUser(User user)
        {
            var boards = await _ctx.Whiteboards
                .Where(w => w.Owner.Id == user.Id)
                .ToListAsync();

            return boards;
        }
    }
}