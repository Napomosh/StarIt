using Dapper;
using Npgsql;

namespace StarIt.Dal;

public static class DbHelper
{
    private const string CONNECTION_STRING = "Host=localhost;Username=user;Password=1;Database=postgres";

    public static async Task<T?> QueryScalarAsync<T>(string sql, object param)
    {
        await using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }
    
    public static async Task ExecuteAsync(string sql, object model)
    {
        await using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync(); 
        await connection.ExecuteAsync(sql, model);
    }
    
    public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
    {
        await using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        return await connection.QueryAsync<T>(sql, model);
    }
}