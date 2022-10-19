using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UP.Ates.TaskTracker.Domain;
using UP.Ates.TaskTracker.Repositories;
using UP.Ates.TaskTracker.Services;

namespace UP.Ates.TaskTracker.Controllers;

public class TasksController : Controller
{
    private TasksRepository _tasksRepository;
    private TaskService _tasksService;

    public TasksController(TasksRepository tasksRepository, TaskService tasksService)
    {
        _tasksRepository = tasksRepository;
        _tasksService = tasksService;
    }

    [HttpGet("New")]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost("New")]
    public async Task<IActionResult> New(PopugTask task)
    {
        await _tasksService.SaveTaskAsync(task);
        
        return RedirectToAction("Index", "Home", new{result="Таска создана"});
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        // await _tasksRepository.Delete
        // var model = await _tasksRepository.GetUndoneTasksAsync();
        // return View(model);
        return Ok("");
    }
}