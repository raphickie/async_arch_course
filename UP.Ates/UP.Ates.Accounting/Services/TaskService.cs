using System;
using System.Linq;
using System.Threading.Tasks;
using UP.Ates.TaskTracker.Producers;
using UP.Ates.TaskTracker.Repositories;

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
            await _tasksRepository.SaveTaskAsync(task);
            await _producer.ProduceTaskAssigned(task, "TaskAssigned");
        }
    }
}