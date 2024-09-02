using Atividade_06_Vitrine.Models;
using MySql.Data.MySqlClient;

namespace Atividade_06_Vitrine.Repositories.MySQL;

public class PedidoRepository : IPedidoRepository {
    private readonly string _connectionString;

    public PedidoRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public Pedido? AtualizarPedido(Pedido pedido) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE pedidos 
                                SET usuario_codigo = @usuarioCodigo, carrinho_codigo = @carrinhoCodigo, estado = @estado 
                                WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@usuarioCodigo", pedido.Usuario.Codigo);
        command.Parameters.AddWithValue("@carrinhoCodigo", pedido.Carrinho.Codigo);
        command.Parameters.AddWithValue("@estado", pedido.Estado.ToString());
        command.Parameters.AddWithValue("@codigo", pedido.Codigo);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0 ? pedido : null;
    }

    public Pedido? ObterPedidoPorCodigo(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT p.codigo, p.usuario_codigo, p.carrinho_codigo, p.estado, 
                                u.nome, u.endereco
                                FROM pedidos p
                                JOIN usuarios u ON p.usuario_codigo = u.codigo
                                WHERE p.codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);

        using var reader = command.ExecuteReader();

        if (reader.Read()) {
            var usuario = new Usuario(reader.GetInt32("usuario_codigo"),
                                      reader.GetString("nome"),
                                      reader.GetString("endereco"));

            var carrinho = ObterCarrinhoPorCodigo(reader.GetInt32("carrinho_codigo"));

            return new Pedido(
                reader.GetInt32("codigo"),
                usuario,
                carrinho!
            ) {
                Estado = Enum.Parse<Pedido.Estado>(reader.GetString("estado"))
            };
        }

        return null;
    }

    public bool RemoverPedido(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM pedidos WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);

        return command.ExecuteNonQuery() > 0;
    }

    private Carrinho? ObterCarrinhoPorCodigo(int codigo) {
        var carrinhoRepository = new CarrinhoRepository(_connectionString);
        return carrinhoRepository.ObterCarrinhoPorCodigo(codigo);
    }
}
