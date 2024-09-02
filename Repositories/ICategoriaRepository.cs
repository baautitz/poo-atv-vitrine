using Atividade_06_Vitrine.Models;

namespace Atividade_06_Vitrine.Repositories;

public interface ICategoriaRepository {

    List<Categoria> ObterTodasCategorias();
    Categoria? ObterCategoriaPorNome(string nome);

}
