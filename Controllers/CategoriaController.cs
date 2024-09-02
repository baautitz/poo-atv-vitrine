using Atividade_06_Vitrine.Models;
using Atividade_06_Vitrine.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Atividade_06_Vitrine.Controllers;

[Route("CategoriaController")]
public class CategoriaController : Controller {

    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository) {
        _categoriaRepository = categoriaRepository;
    }

    // GET: CategoriaController
    [HttpGet]
    public ActionResult Index() {
        List<Categoria>? categorias = _categoriaRepository.ObterTodasCategorias();

        if (categorias == null) {
            return Problem("Nenhuma categoria encontrada.");
        }

        return View(categorias);
    }

    // GET: CategoriaController/{nome}
    [HttpGet("{nome}")]
    public ActionResult Index(string nome) {
        Categoria? categoria = _categoriaRepository.ObterCategoriaPorNome(nome);

        if (categoria == null) {
            return NotFound();
        }

        return View(categoria);
    }
}
