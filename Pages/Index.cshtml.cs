using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Atividade_06_Vitrine.Pages {
    public class IndexModel : BasePageModel {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _catiaRepository;

        public IndexModel(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository) : base(categoriaRepository)  {
            _produtoRepository = produtoRepository;
        }

        public List<Produto> Produtos { get; set; }

        public void OnGet() {
            LoadCategorias();
            Produtos = _produtoRepository.ObterTodosProdutos();
        }
    }
}
