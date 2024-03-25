using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

public class ClientesRepository
{
    private readonly IDbConnection connection;

    public ClientesRepository(IConfiguration configuration)
    {
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<List<Cliente>> GetClientesAsync()
    {
        string query = "SELECT * FROM Clientes";

        var clientes = await connection.QueryAsync<Cliente>(query);

        return clientes.ToList();
    }
}