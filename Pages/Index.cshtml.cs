using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Atividade_06_Vitrine.Pages {
    public class IndexModel : PageModel {
        private readonly IProdutoRepository _produtoRepository;

        public IndexModel(IProdutoRepository produtoRepository) {
            _produtoRepository = produtoRepository;
        }

        public List<Produto> Produtos { get; set; }

        public void OnGet() {
            Produtos = _produtoRepository.ObterTodosProdutos();
        }
    }
}
