using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using UP.Ates.TaskTracker.Domain;

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
        await using var con = new SqlConnection();
        await con.OpenAsync();

        var reader = await con.ExecuteReaderAsync(@"
            SELECT t.TaskId, t.Description, t.UserId, t.Status 
            from tasks t
            inner join AspNetusers u
            on t.UserId=u.Id
            where t.Status = 0
");
        while (await reader.ReadAsync())
        {
            result.Add(new PopugTask()
            {
                Id = reader.GetString("TaskId"),
                Description = reader.GetString("Description"),
                UserId = reader.GetString("UserId"),
                Status = reader.GetInt32("Status")
            });
        }

        return result.ToArray();
    }

    public async Task SaveTaskAsync(PopugTask task)
    {
        await using var con = new SqlConnection();
        await con.OpenAsync();

        var resultRows = await con.ExecuteAsync(@"
            update tasks
                
            set TaskId=@taskId, Description=@description, UserId=@userId, Status=@status)
            where t.Id = @taskId");

        return;
    }
}