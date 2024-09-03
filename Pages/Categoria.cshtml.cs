using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Atividade_06_Vitrine.Pages {
    public class CategoriaModel : PageModel {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaModel(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository) {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
        }

        public Categoria Categoria { get; set; }

        public void OnGet(string nome_categoria) {
            Categoria = _categoriaRepository.ObterCategoriaPorNome(nome_categoria);
            if (Categoria != null) {
                // Load products for the category
                Categoria.Produtos = Categoria.Produtos ?? _produtoRepository.ObterTodosProdutos()
                    .Where(p => Categoria.Produtos.Select(prod => prod.Codigo).Contains(p.Codigo)).ToList();
            }
        }
    }
}
