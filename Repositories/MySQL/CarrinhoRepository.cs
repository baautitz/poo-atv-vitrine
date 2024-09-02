using Atividade_06_Vitrine.Models;
using MySql.Data.MySqlClient;

namespace Atividade_06_Vitrine.Repositories.MySQL;

public class CarrinhoRepository : ICarrinhoRepository {
    private readonly string _connectionString;

    public CarrinhoRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public Carrinho? AtualizarCarrinho(Carrinho carrinho) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE carrinhos 
                                SET usuario_codigo = @usuarioCodigo, desconto = @desconto 
                                WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@usuarioCodigo", carrinho.Usuario.Codigo);
        command.Parameters.AddWithValue("@desconto", carrinho.Desconto);
        command.Parameters.AddWithValue("@codigo", carrinho.Codigo);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0) {
            command.CommandText = @"DELETE FROM carrinho_produto WHERE carrinho_codigo = @codigo";
            command.ExecuteNonQuery();

            foreach (var produto in carrinho.Produtos) {
                command.CommandText = @"INSERT INTO carrinho_produto (carrinho_codigo, produto_codigo) 
                                        VALUES (@codigo, @produtoCodigo)";
                command.Parameters.AddWithValue("@produtoCodigo", produto.Codigo);
                command.ExecuteNonQuery();
            }

            return carrinho;
        }

        return null;
    }

    public Carrinho? ObterCarrinhoPorCodigo(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT c.codigo, c.usuario_codigo, c.desconto, 
                                u.nome, u.endereco 
                                FROM carrinhos c
                                JOIN usuarios u ON c.usuario_codigo = u.codigo
                                WHERE c.codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);

        using var reader = command.ExecuteReader();

        if (reader.Read()) {
            var usuario = new Usuario(reader.GetInt32("usuario_codigo"),
                                      reader.GetString("nome"),
                                      reader.GetString("endereco"));

            var produtos = ObterProdutosPorCarrinho(codigo);

            return new Carrinho(
                reader.GetInt32("codigo"),
                usuario,
                produtos
            ) {
                Desconto = reader.GetDouble("desconto")
            };
        }

        return null;
    }

    public bool RemoverCarrinho(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM carrinho_produto WHERE carrinho_codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);
        command.ExecuteNonQuery();

        command.CommandText = @"DELETE FROM carrinhos WHERE codigo = @codigo";
        return command.ExecuteNonQuery() > 0;
    }

    private List<Produto> ObterProdutosPorCarrinho(int carrinhoCodigo) {
        var produtos = new List<Produto>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT p.codigo, p.descricao, p.valor_bruto, p.desconto, p.image_url
                                FROM carrinho_produto cp
                                JOIN produtos p ON cp.produto_codigo = p.codigo
                                WHERE cp.carrinho_codigo = @carrinhoCodigo";
        command.Parameters.AddWithValue("@carrinhoCodigo", carrinhoCodigo);

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
}
