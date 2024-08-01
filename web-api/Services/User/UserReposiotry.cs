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
}