using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using UP.Ates.TaskTracker.Models;

namespace UP.Ates.TaskTracker.Repositories;

public class UserRepository
{
    private RepositoryConnectionSettings _settings;

    public UserRepository(RepositoryConnectionSettings settings)
    {
        _settings = settings;
    }

    public async Task<ApplicationUser[]> GetAllUsersAsync()
    {
        var result = new List<ApplicationUser>();
        await using var con = new SqliteConnection(_settings.ConnectionString);
        await con.OpenAsync();

        var reader = await con.ExecuteReaderAsync(@"
            SELECT *
            from AspNetUsers
");
        while (await reader.ReadAsync())
        {
            result.Add(new ApplicationUser()
            {
                Id = reader.GetString("Id"),
            });
        }
        return result.ToArray();
    }
}