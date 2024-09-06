namespace Atividade_06_Vitrine.Models;

public class Produto {

    private double _desconto = 0;

    public int Codigo { get; set; }
    public string Descricao { get; set; }
    public double ValorBruto { get; set; }
    public string? ImageURL { get; set; }

    public Produto(int codigo, string descricao, double valorBruto, string? imageURL, double desconto) {
        Codigo = codigo;
        Descricao = descricao;
        ValorBruto = valorBruto;
        ImageURL = imageURL;
        Desconto = desconto;
    }

    public Produto(int codigo, string descricao, double valorBruto, string? imageURL) {
        Codigo = codigo;
        Descricao = descricao;
        ValorBruto = valorBruto;
        ImageURL = imageURL;
    }

    public double CalcularValorLiquido() {
        return ValorBruto - (ValorBruto * Desconto);
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
    
    public double CalcularPorcentagem()
    {
        return Desconto * 100;
    }

}