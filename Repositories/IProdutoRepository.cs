using Atividade_06_Vitrine.Models;

namespace Atividade_06_Vitrine.Repositories;

public interface IProdutoRepository {

    List<Produto> ObterTodosProdutos();
    Produto? ObterProdutoPorCodigo(int codigo);

}
