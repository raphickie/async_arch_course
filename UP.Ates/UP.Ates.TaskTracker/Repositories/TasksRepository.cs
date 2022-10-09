using System;
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
        using var con = new SqlConnection();
        con.Open();

        var version = con.ExecuteScalar<string>("SELECT @@VERSION");

        Console.WriteLine(version);
    }
}