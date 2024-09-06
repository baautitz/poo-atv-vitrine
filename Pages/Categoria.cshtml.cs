using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Atividade_06_Vitrine.Pages {
    public class CategoriaModel : BasePageModel {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaModel(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository) : base (categoriaRepository) {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
        }

        public Categoria Categoria { get; set; }

		public IActionResult OnGet(string nome_categoria) {
			LoadCategorias();
			Categoria = _categoriaRepository.ObterCategoriaPorNome(nome_categoria);

			if (Categoria == null) {
				return RedirectToPage("Error");
			}

			Categoria.Produtos = Categoria.Produtos ?? _produtoRepository.ObterTodosProdutos()
				.Where(p => Categoria.Produtos.Select(prod => prod.Codigo).Contains(p.Codigo)).ToList();

			return Page();
		}

	}
}
