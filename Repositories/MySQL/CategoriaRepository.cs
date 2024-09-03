using Atividade_06_Vitrine.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Atividade_06_Vitrine.Repositories.MySQL;

public class CategoriaRepository : ICategoriaRepository {
    private readonly string _connectionString;

    public CategoriaRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public Categoria? ObterCategoriaPorNome(string nome) {
        Categoria? categoria = null;
        List<Produto> produtos = new List<Produto>();

        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = @"
            SELECT c.codigo, c.nome, p.codigo AS produto_codigo, p.descricao, p.valor_bruto, p.desconto, p.image_url
            FROM categorias c
            LEFT JOIN produto_categoria pc ON c.codigo = pc.categoria_codigo
            LEFT JOIN produtos p ON pc.produto_codigo = p.codigo
            WHERE c.nome = @nome";

        using MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@nome", nome);

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read()) {
            if (categoria == null) {
                categoria = new Categoria(
                    reader.GetInt32("codigo"),
                    reader.GetString("nome"),
                    produtos
                );
            }

            if (!reader.IsDBNull(reader.GetOrdinal("produto_codigo"))) {
                produtos.Add(new Produto(
                    reader.GetInt32("produto_codigo"),
                    reader.GetString("descricao"),
                    reader.GetDouble("valor_bruto"),
                    reader.GetString("image_url"),
                    reader.GetDouble("desconto")
                ));
            }
        }

        return categoria;
    }

    public List<Categoria> ObterTodasCategorias() {
        List<Categoria> categorias = new List<Categoria>();

        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = @"
            SELECT c.codigo, c.nome, p.codigo AS produto_codigo, p.descricao, p.valor_bruto, p.desconto, p.image_url
            FROM categorias c
            LEFT JOIN produto_categoria pc ON c.codigo = pc.categoria_codigo
            LEFT JOIN produtos p ON pc.produto_codigo = p.codigo
            ORDER BY c.codigo";

        using MySqlCommand command = new MySqlCommand(query, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        Dictionary<int, Categoria> categoriaMap = new Dictionary<int, Categoria>();

        while (reader.Read()) {
            int categoriaCodigo = reader.GetInt32("codigo");

            if (!categoriaMap.ContainsKey(categoriaCodigo)) {
                categoriaMap[categoriaCodigo] = new Categoria(
                    categoriaCodigo,
                    reader.GetString("nome"),
                    new List<Produto>()
                );
            }

            if (!reader.IsDBNull(reader.GetOrdinal("produto_codigo"))) {
                categoriaMap[categoriaCodigo].Produtos.Add(new Produto(
                    reader.GetInt32("produto_codigo"),
                    reader.GetString("descricao"),
                    reader.GetDouble("valor_bruto"),
                    reader.GetString("image_url"),
                    reader.GetDouble("desconto")
                ));
            }
        }

        categorias.AddRange(categoriaMap.Values);

        return categorias;
    }
}
