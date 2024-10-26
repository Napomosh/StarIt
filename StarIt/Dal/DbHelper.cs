using System.Data;
using Dapper;
using MySqlConnector;

namespace StarIt.Dal;

public static class DbHelper
{
    private const string CONNECTION_STRING = "User ID=root;Password=1111;Host=localhost;Port=3302;Database=file_sharing_system;Allow User Variables=true";

    public static async Task<T?> QueryScalarAsync<T>(string sql, object param)
    {
        await using var connection = new MySqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }
    
    public static async Task ExecuteAsync(string sql, object model)
    {
        await using var connection = new MySqlConnection(CONNECTION_STRING);
        await connection.OpenAsync(); 
        await connection.ExecuteAsync(sql, model);
    }
    
    public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
    {
        await using var connection = new MySqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        return await connection.QueryAsync<T>(sql, model);
    }
}