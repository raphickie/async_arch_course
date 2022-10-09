using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using UP.Ates.Auth.Models;

namespace UP.Ates.Auth.Quickstart.Users
{
    [Route("Users")]
    public class UsersController : Controller
    {
        private UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsers();

            return View(users);
        }

        [HttpGet("New")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("New")]
        public async Task<IActionResult> New(ApplicationUserContract user)
        {
            user.PasswordHash = user.Password.Sha256();
            await _userService.AddUser(user as ApplicationUser);
            return RedirectToAction("Index");
        }
        
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteUser(userId);
            return RedirectToAction("Index");
        }
    }
}