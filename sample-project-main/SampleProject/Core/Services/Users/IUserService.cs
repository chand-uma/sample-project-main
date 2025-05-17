using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Users
{
    public interface IUserService
    {
        Task<User> CreateAsync(Guid id, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags);
        Task DeleteAsync(User user);
        Task DeleteAllAsync();
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync(UserTypes? userType = null, string name = null, string email = null);
    }
}
