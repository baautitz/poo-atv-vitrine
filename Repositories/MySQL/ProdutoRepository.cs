using Atividade_06_Vitrine.Models;
using MySql.Data.MySqlClient;

namespace Atividade_06_Vitrine.Repositories.MySQL;

public class ProdutoRepository : IProdutoRepository {
    private readonly string _connectionString;

    public ProdutoRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public List<Produto> ObterTodosProdutos() {
        var produtos = new List<Produto>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM produtos";

        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var produto = new Produto(
                reader.GetInt32("codigo"),
                reader.GetString("descricao"),
                reader.GetDouble("valor_bruto"),
                reader.GetString("image_url") ?? "",
                reader.GetDouble("desconto")
            );

            produtos.Add(produto);
        }

        return produtos;
    }

    public Produto? ObterProdutoPorCodigo(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM produtos WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);

        using var reader = command.ExecuteReader();

        if (reader.Read()) {
            return new Produto(
                reader.GetInt32("codigo"),
                reader.GetString("descricao"),
                reader.GetDouble("valor_bruto"),
                reader.GetString("image_url") ?? "",
                reader.GetDouble("desconto")
            );
        }

        return null;
    }

}
