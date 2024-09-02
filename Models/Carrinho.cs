namespace Atividade_06_Vitrine.Models;

public class Carrinho {

    private double _desconto = 0;

    public int Codigo { get; set; }
    public Usuario Usuario { get; set; }
    public List<Produto> Produtos { get; set; }

    public Carrinho(int codigo, Usuario usuario, List<Produto> produtos) {
        Codigo = codigo;
        Usuario = usuario;
        Produtos = produtos;
    }

    public double CalcularValorBruto() {
        double total = 0;
        foreach (Produto produto in Produtos) {
            total += produto.ValorBruto;
        }

        return total;
    }

    public double CalcularValorLiquido() {
        double total = 0;
        foreach (Produto produto in Produtos) {
            total += produto.CalcularValorLiquido();
        }

        return total - (total * Desconto);
    }

    public double Desconto {
        get => _desconto;
        set {
            if (value < 0 || value > 1) {
                throw new ArgumentOutOfRangeException(nameof(value), "O desconto deve estar entre 0 e 1.");
            }

            _desconto = value;
        }
    }

}
