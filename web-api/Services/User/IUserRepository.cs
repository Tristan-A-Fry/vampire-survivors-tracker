using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using web_api.Models;

namespace web_api.Services
{
    public interface IUserRepository
    {
        Task<AuthenticationResult> AuthenticateAsync(string userName, string password);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id); 
    }
}