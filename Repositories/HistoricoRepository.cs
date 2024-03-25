using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

public class HistoricoRepository
{
    private readonly IDbConnection connection;
    private const int MIN_RANDOM = 0;
    private const int MAX_RANDOM = 200;
    private const int ULTIMOS_VALORES_HISTORICO = 10;

    public HistoricoRepository(IConfiguration configuration)
    {
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<bool> LoadDadosUtilizacaoPorProdutoAsync(Cliente cliente)
    {
        Random random = new();
        int intervaloColetaSegundos = 1;

        int quantidadeRandom = random.Next(MIN_RANDOM, MAX_RANDOM);

        var query = "INSERT INTO Historico (IdCliente, Quantidade) VALUES (" + cliente.Id + ", " + quantidadeRandom + ")";
        var resultado = await connection.ExecuteAsync(query);

        if (resultado == 0){
            return false;
        }

        Thread.Sleep(intervaloColetaSegundos * 1000);

        return true;
    }

    public List<Historico> GetHistoricosAsync(int idCliente)
    {
        string query = @"SELECT TOP(" +ULTIMOS_VALORES_HISTORICO.ToString()+ @") 
                                CONVERT(VARCHAR, DataHora, 8) AS [Horario],
	                            Quantidade  
                        FROM Historico h
                            INNER JOIN Clientes c
                            ON h.IdCliente = c.Id
                        WHERE c.Id = " + idCliente.ToString() + @"
                        ORDER BY h.Id DESC";

        List<Historico> historicos = connection.Query<Historico>(query).ToList();

        return historicos.OrderBy(it => it.Horario).ToList();
    }
}