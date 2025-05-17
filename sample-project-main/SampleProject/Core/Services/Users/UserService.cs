using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Users
{
    [AutoRegister]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdObjectFactory<User> _userFactory;
        private readonly IUpdateUserService _updateUserService;

        public UserService(IIdObjectFactory<User> userFactory, IUserRepository userRepository, IUpdateUserService updateUserService)
        {
            _userFactory = userFactory;
            _userRepository = userRepository;
            _updateUserService = updateUserService;
        }

        public async Task<User> CreateAsync(Guid id, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            var user = _userFactory.Create(id);
            _updateUserService.Update(user, name, email, type, annualSalary, tags);
            await _userRepository.SaveAsync(user);
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
        }

        public async Task DeleteAllAsync()
        {
            await _userRepository.DeleteAllAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(UserTypes? userType = null, string name = null, string email = null)
        {
            return await _userRepository.GetAsync(userType, name, email);
        }
    }
}
