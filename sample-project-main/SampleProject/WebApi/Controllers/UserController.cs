using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Users;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(IUserService userService, IUpdateUserService updateUserService)
        {
            _userService = userService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateUser(Guid userId, [FromBody] UserModel model)
        {
            var existingUser = await _userService.GetUserAsync(userId);
            if (existingUser != null)
            {
                // User already exists, return a conflict or appropriate response
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, "User already exists.");
            }

            var user = await _userService.CreateAsync(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
            return Found(new UserData(user));
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            _updateUserService.Update(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
            return Found(new UserData(user));
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteUser(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            await _userService.DeleteAsync(user);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUser(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUsers(UserTypes? type = null, string name = null, string email = null)
        {
            var users = (await _userService.GetUsersAsync(type, name, email))
                .Select(q => new UserData(q))
                .ToList();
            return Found(users);
        }

        [Route("clear")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAllUsers()
        {
            await _userService.DeleteAllAsync();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUsersByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new { message = "Tag is required." });
            }

            var users = (await _userService.GetUsersAsync())
                .Where(u => u.Tags != null && u.Tags.Any(t => string.Equals(t, tag, StringComparison.OrdinalIgnoreCase)))
                .Select(u => new UserData(u))
                .ToList();

            return Found(users);
        }
    }
}