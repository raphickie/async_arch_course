using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using UP.Ates.Auth.Models;
using UP.Ates.TaskTracker.Domain;

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
        using var con = new SqlConnection();
        con.Open();

        var reader = await con.ExecuteReaderAsync(@"
            SELECT t.TaskId, t.Description, t.UserId, t.Status 
            from tasks t
            inner join AspNetusers u
            on t.UserId=u.Id
            where t.Status = 0
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