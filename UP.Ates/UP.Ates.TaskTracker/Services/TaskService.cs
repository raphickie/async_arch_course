using System;
using System.Linq;
using System.Threading.Tasks;
using UP.Ates.TaskTracker.Domain;
using UP.Ates.TaskTracker.Producers;
using UP.Ates.TaskTracker.Repositories;
using TaskStatus = UP.Ates.TaskTracker.Domain.TaskStatus;

namespace UP.Ates.TaskTracker.Services;

public class TaskService
{
    private readonly TasksRepository _tasksRepository;
    private UserRepository _userRepository;
    private PopugProducer _producer;
    
    public TaskService(TasksRepository tasksRepository, UserRepository userRepository)
    {
        _tasksRepository = tasksRepository;
        _userRepository = userRepository;
    }

    public async Task AssignTasksAsync()
    {
        var allTasks = await _tasksRepository.GetUndoneTasksAsync();
        var allPopugs = await _userRepository.GetAllUsersAsync();
        var rnd = new Random();
        foreach (var task in allTasks)
        {
            task.UserId = allPopugs[rnd.Next(allPopugs.Length)].Id;
            await _tasksRepository.UpdateTaskAsync(task);
            await _producer.ProduceTaskAssigned(task, "TaskAssigned");
        }
    }

    public async Task SaveTaskAsync(PopugTask task)
    {
        var random = new Random();
        var availablePopugs = await _userRepository.GetAllUsersAsync();
        var taskUser = random.Next(availablePopugs.Length);
        task.UserId = availablePopugs[taskUser].Id;
        task.Id = Guid.NewGuid().ToString();
        task.Status = TaskStatus.NotDone;
        await _tasksRepository.AddTaskAsync(task);
    }
}