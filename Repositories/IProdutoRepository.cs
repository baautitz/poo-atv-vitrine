using Atividade_06_Vitrine.Models;

namespace Atividade_06_Vitrine.Repositories;

public interface IProdutoRepository {

    List<Produto>? ObterTodosProdutos();
    Produto? RecuperarProdutoPorCodigo(int codigo);
    Produto? AtualizarProduto(Produto produto);
    bool RemoverProduto(int codigo);
}
