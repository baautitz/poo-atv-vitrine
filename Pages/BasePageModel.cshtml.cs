using Microsoft.AspNetCore.Mvc.RazorPages;
using Atividade_06_Vitrine.Repositories;

public class BasePageModel : PageModel {
    protected readonly ICategoriaRepository _categoriaRepository;

    public BasePageModel(ICategoriaRepository categoriaRepository) {
        _categoriaRepository = categoriaRepository;
    }

    public void LoadCategorias() {
        var categorias = _categoriaRepository.ObterTodasCategorias();
        ViewData["Categorias"] = categorias;
    }
}