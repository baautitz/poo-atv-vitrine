using Atividade_06_Vitrine.Models;

namespace Atividade_06_Vitrine.Repositories;

public interface IPedidoRepository {

    Pedido? ObterPedidoPorCodigo(int codigo);
    Pedido? AtulizarPedido(Pedido carrinho);
    bool RemoverPedido(int codigo);

}
