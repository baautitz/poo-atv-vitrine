namespace Atividade_06_Vitrine.Models;

public class Categoria {

    public int Codigo { get; set; }
    public string Nome { get; set; }
    public List<Produto> Produtos { get; set; }

    public Categoria(int codigo, string nome, List<Produto> produtos) {
        Codigo = codigo;
        Nome = nome;
        Produtos = produtos;
    }

}
