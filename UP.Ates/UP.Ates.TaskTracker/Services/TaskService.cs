using System.Threading.Tasks;
using UP.Ates.TaskTracker.Repositories;

namespace UP.Ates.TaskTracker.Services;

public class TaskService
{
    private TasksRepository _tasksRepository;

    public TaskService(TasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task AssignTasksAsync()
    {
        var allTasks = await _tasksRepository.GetUndoneTasksAsync();
        

    }
}