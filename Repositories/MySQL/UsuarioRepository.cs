using Atividade_06_Vitrine.Models;
using MySql.Data.MySqlClient;

namespace Atividade_06_Vitrine.Repositories.MySQL;

public class UsuarioRepository : IUsuarioRepository {
    private readonly string _connectionString;

    public UsuarioRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public Usuario? AtualizarUsuario(Usuario usuario) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE usuarios 
                                SET nome = @nome, endereco = @endereco 
                                WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@nome", usuario.Nome);
        command.Parameters.AddWithValue("@endereco", usuario.Endereco);
        command.Parameters.AddWithValue("@codigo", usuario.Codigo);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0 ? usuario : null;
    }

    public Usuario? CriarUsuario(Usuario usuario) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO usuarios (nome, endereco) 
                                VALUES (@nome, @endereco)";
        command.Parameters.AddWithValue("@nome", usuario.Nome);
        command.Parameters.AddWithValue("@endereco", usuario.Endereco);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0) {
            usuario.Codigo = (int) command.LastInsertedId;
            return usuario;
        }

        return null;
    }

    public Usuario? ObterUsuarioPorNome(string name) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM usuarios WHERE nome = @nome";
        command.Parameters.AddWithValue("@nome", name);

        using var reader = command.ExecuteReader();

        if (reader.Read()) {
            return new Usuario(
                reader.GetInt32("codigo"),
                reader.GetString("nome"),
                reader.GetString("endereco")
            );
        }

        return null;
    }

    public bool RemoverUsuario(int codigo) {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM usuarios WHERE codigo = @codigo";
        command.Parameters.AddWithValue("@codigo", codigo);

        return command.ExecuteNonQuery() > 0;
    }
}
