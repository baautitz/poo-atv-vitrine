using Atividade_06_Vitrine.Models;
using MySql.Data.MySqlClient;

namespace Atividade_06_Vitrine.Repositories.MySQL;

public class ProdutoRepository : IProdutoRepository {
    private readonly string _connectionString;

    public ProdutoRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public Produto? AtualizarProduto(Produto produto) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE produtos 
                                SET descricao = @descricao, valor_bruto = @valorBruto, desconto = @desconto, image_url = @imageURL 
                                WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@descricao", produto.Descricao);
        command.Parameters.AddWithValue("@valorBruto", produto.ValorBruto);
        command.Parameters.AddWithValue("@desconto", produto.Desconto);
        command.Parameters.AddWithValue("@imageURL", produto.ImageURL);
        command.Parameters.AddWithValue("@codigo", produto.Codigo);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0 ? produto : null;
    }

    public List<Produto>? ObterTodosProdutos() {
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

    public Produto? RecuperarProdutoPorCodigo(int codigo) {
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

    public bool RemoverProduto(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM produtos WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);

        return command.ExecuteNonQuery() > 0;
    }
}
