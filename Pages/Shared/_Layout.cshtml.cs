using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Atividade_06_Vitrine.Pages.Shared;

public class _LayoutModel : PageModel {

    private readonly ICategoriaRepository _categoriaRepository;

    public _LayoutModel(ICategoriaRepository categoriaRepository) {
        _categoriaRepository = categoriaRepository;
    }

    public List<Categoria> Categorias { get; set; }

    public void OnGet() {
        Categorias = _categoriaRepository.ObterTodasCategorias();
    }

}
