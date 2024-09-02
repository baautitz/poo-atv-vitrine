using Atividade_06_Vitrine.Models;

namespace Atividade_06_Vitrine.Repositories;

public interface IUsuarioRepository {

    Usuario? ObterUsuarioPorNome(string name);
    Usuario? CriarUsuario();
    Usuario? AtualizarUsuario(Usuario usuario);
    bool RemoverUsuario(int codigo);

}
