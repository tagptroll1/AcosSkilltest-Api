using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acos.ProgrammingTask.Models;
using Acos.ProgrammingTask.Utils;
using Microsoft.EntityFrameworkCore;

namespace Acos.ProgrammingTask.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> Create(User user, string password);
        Task Update(User user, string password = null);
        Task Delete(int id);
        Task<User> JustFindHim(string query);
    }

    public class UserService : IUserService
    {
        private DatabaseContext _ctx;

        public UserService(DatabaseContext context)
        {
            _ctx = context;
        }

        public async Task<User> JustFindHim(string query)
        {
            User user;

            try {
                var id = Int32.Parse(query);
                user = await _ctx.Users.FindAsync(id);

                if (user != null)
                    return user;
            }
            catch (Exception){}

            user =  await _ctx.Users
                .Where(u => u.Username == query || u.Email == query)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _ctx.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PassHash, user.PassSalt))
                return null;

            return user;
        }

        public async Task<User> GetById(int id) => 
            await _ctx.Users.FindAsync(id);
        

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new UserException("Password is required");

            if (await _ctx.Users.AnyAsync(u => u.Username == user.Username))
                throw new UserException($"Username \"{user.Username}\" is already taken!");

            byte[] passHash;
            byte[] passSalt;
            CreatePasswordHash(password, out passHash, out passSalt);

            user.PassHash = passHash;
            user.PassSalt = passSalt;

            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            var user = await _ctx.Users.FindAsync(id);
            if (user != null)
            {
                _ctx.Users.Remove(user);
                await _ctx.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<User>> GetAll() =>
            await _ctx.Users.ToListAsync();

        public async Task Update(User userIn, string password)
        {
            var user = await _ctx.Users.FindAsync(userIn.Id);

            if (user == null)
                throw new UserException("User not found");

            if (userIn.Username != user.Username)
            {
                // Username has changed
                if (await _ctx.Users.AnyAsync(u => u.Username == userIn.Username))
                    throw new UserException($"Username \"{userIn.Username}\" is already taken");
            }

            user.Username = userIn.Username;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passHash;
                byte[] passSalt;
                CreatePasswordHash(password, out passHash, out passSalt);

                user.PassHash = passHash;
                user.PassSalt = passSalt;
            }

            _ctx.Users.Update(user);
            await _ctx.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length for password hash", "storedHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length for password salt", "storedSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}