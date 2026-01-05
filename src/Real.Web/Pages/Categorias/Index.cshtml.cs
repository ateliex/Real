using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Models;
using Real.Pages.Shared;
using System.ComponentModel.DataAnnotations;

namespace Real.Pages.Categorias;

public class IndexModel : PageModel
{
    private readonly RealDbContext _db;

    public IndexModel(RealDbContext db)
    {
        _db = db;
    }

    [MinLength(3)]
    [MaxLength(35)]
    [BindProperty(SupportsGet = true)]
    public string? Nome { get; set; }

    public IList<Categoria> Categorias { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        var totalRegistros = await _db.Categorias.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Categorias != null)
        {
            Categorias = await _db.Categorias
                .Where(x => true
                    && (Nome == null || x.Nome == Nome))
                .OrderByDescending(x => x.Nome)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}
