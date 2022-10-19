using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using UP.Ates.TaskTracker.Models;
using UP.Ates.TaskTracker.Repositories;
using UP.Ates.TaskTracker.Services;

namespace UP.Ates.TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskService _taskService;
        private readonly TasksRepository _tasksRepository;

        public HomeController(TaskService taskService, TasksRepository tasksRepository)
        {
            _taskService = taskService;
            _tasksRepository = tasksRepository;
        }

        public async Task<IActionResult> Index(string result)
        {
            var model = await _tasksRepository.GetUndoneTasksAsync();
            ViewBag.Result = result;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTasks()
        {
            await _taskService.AssignTasksAsync();
            return RedirectToAction("Index", "Home", new { result = "Заассайнено" });
        }


        public async Task<IActionResult> CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync("https://localhost:6001/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}