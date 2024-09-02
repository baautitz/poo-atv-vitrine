namespace Atividade_06_Vitrine.Models;

public class Pedido {

    public enum Estado {
        PAGO,
        RECUSADO,
        PROCESSANDO,
        FINALIZADO
    }

    public int Codigo { get; set; }
    public Usuario Usuario { get; set; }
    public Carrinho Carrinho { get; set; }

    public Pedido(int codigo, Usuario usuario, Carrinho carrinho) {
        Codigo = codigo;
        Usuario = usuario;
        Carrinho = carrinho;
    }

}
