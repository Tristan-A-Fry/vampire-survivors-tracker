using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using web_api.Models;
using web_api.Data;


namespace web_api.Services
{
    public class UserReposiotry : IUserRepository
    {
       private readonly AppDbContext _context;

       public UserReposiotry(AppDbContext context)
       {
            _context = context;
       }
        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new AuthenticationResult { IsSuccess = false };
            }

            // Assuming user roles are stored somewhere
            var roles = new List<string> { user.Role };

            return new AuthenticationResult
            {
                IsSuccess = true,
                UserId = user.Id, //this is how we are sending/keeping track of the userId, so each user ahas their own information
                Roles = roles
            };
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if(await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                throw new InvalidOperationException("A user with this username already exists.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; } = false;
        public int UserId{get; set;}
        public List<string> Roles { get; set; } = new List<string>();
    }

    public class Roles
    {
        public const string Admin = "Admin";
        public const string Regular = "Regular";
    }
}