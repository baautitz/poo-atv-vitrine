using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Atividade_06_Vitrine.Controllers;

[Route("ProdutoController")]
public class ProdutoController : Controller {

    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository) {
        _produtoRepository = produtoRepository;
    }

    // GET: ProdutoController
    [HttpGet]
    public ActionResult Index() {
        List<Produto>? produtos = _produtoRepository.ObterTodosProdutos();

        if (produtos == null) {
            return Problem("Nenhum produto encontrado.");
        }

        return View(produtos);
    }

    // GET: ProdutoController/{id}
    [HttpGet("{id}")]
    public ActionResult Details(int id) {
        Produto? produto = _produtoRepository.ObterProdutoPorCodigo(id);

        if (produto == null) {
            return NotFound();
        }

        return View(produto);
    }
}
