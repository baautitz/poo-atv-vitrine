using Atividade_06_Vitrine.Models;

namespace Atividade_06_Vitrine.Repositories;

public interface ICarrinhoRepository {

    Carrinho? ObterCarrinhoPorCodigo(int codigo);
    Carrinho? AtulizarCarrinho(Carrinho carrinho);
    bool RemoverCarrinho(int codigo);

}
