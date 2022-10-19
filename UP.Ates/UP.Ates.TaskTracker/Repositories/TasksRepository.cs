using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using UP.Ates.TaskTracker.Domain;
using TaskStatus = UP.Ates.TaskTracker.Domain.TaskStatus;

namespace UP.Ates.TaskTracker.Repositories;

public class TasksRepository
{
    private RepositoryConnectionSettings _settings;

    public TasksRepository(RepositoryConnectionSettings settings)
    {
        _settings = settings;
    }

    public async Task<PopugTask[]> GetUndoneTasksAsync()
    {
        var result = new List<PopugTask>();
        await using var con = new SqliteConnection(_settings.ConnectionString);
        await con.OpenAsync();

        var reader = await con.ExecuteReaderAsync(@"
            SELECT t.TaskId, t.Description, t.UserId, t.Status 
            from tasks t
            inner join AspNetusers u
            on t.UserId=u.Id
            where t.Status in (0,1)
");
        while (await reader.ReadAsync())
        {
            result.Add(new PopugTask
            {
                Id = reader.GetString("TaskId"),
                Title = reader.GetString("Description"),
                UserId = reader.GetString("UserId"),
                Status = (TaskStatus)reader.GetInt32("Status")
            });
        }

        return result.ToArray();
    }

    public async Task AddTaskAsync(PopugTask task)
    {
        await using var con = new SqliteConnection(_settings.ConnectionString);
        await con.OpenAsync();

        await con.ExecuteScalarAsync(@"
            insert into tasks (TaskId, Description, UserId, Status)
                values (@TaskId, @Description, @UserId, @Status);
                ", new
        {
            TaskId = task.Id,
            Description = task.Title,
            task.UserId,
            Status = (int)task.Status
        });
    }

    public async Task UpdateTaskAsync(PopugTask task)
    {
        await using var con = new SqliteConnection(_settings.ConnectionString);
        await con.OpenAsync();

        var resultRows = await con.ExecuteScalarAsync(@"
                    update Tasks
        set Description=@Description, UserId=@UserId, Status=@Status
        where TaskId = @taskId
                ", new
        {
            TaskId = task.Id,
            Description = task.Title,
            task.UserId,
            Status = (int)task.Status
        });
    }
}