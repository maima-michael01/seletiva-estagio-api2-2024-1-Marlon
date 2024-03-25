using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

public class ProdutosRepository
{
    private readonly IDbConnection connection;

    public ProdutosRepository(IConfiguration configuration)
    {
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public List<Produto> GetProdutos()
    {
        string query = @"SELECT * 
                        FROM Produtos p
                            INNER JOIN Clientes c
                            ON p.IdCliente = c.Id";

        List<Produto> produtos = connection.Query<Produto, Cliente, Produto>(query, (produto, cliente) => {
            produto.Cliente = cliente;
            return produto;
        }).ToList();
        
        return produtos;
    }

    public async Task<bool> PostProduto(Produto produto)
    {
        var sql = "INSERT INTO Produtos (IdCliente, Nome, Imagem) VALUES (" + produto.Cliente.Id + ", @Nome, @Imagem)";
        var resultado = await connection.ExecuteAsync(sql, produto);

        return resultado > 0;
    }

    public async Task<bool> PutProduto(Produto produto)
    {
        var sql = "UPDATE Produtos SET Nome = @Nome, Imagem = @Imagem WHERE Id = @Id";
        var resultado = await connection.ExecuteAsync(sql, produto);

        return resultado > 0;
    }

    public async Task<bool> DeleteProduto(int idProduto)
    {
        var sql = "DELETE FROM Produtos WHERE Id = " + idProduto;
        var resultado = await connection.ExecuteAsync(sql);

        return resultado > 0;
    }
}