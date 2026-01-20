using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Models;
using Real.Pages.Shared;
using System.ComponentModel.DataAnnotations;

namespace Real.Pages.Contas;

public class ConsultarModel : PageModel
{
    private readonly RealDbContext _db;

    public ConsultarModel(RealDbContext db)
    {
        _db = db;
    }

    [MinLength(3)]
    [MaxLength(35)]
    [BindProperty(SupportsGet = true)]
    public string? Nome { get; set; }

    public IList<Conta> Contas { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        var totalRegistros = await _db.Contas.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Contas != null)
        {
            Contas = await _db.Contas
                .Where(x => true
                    && (Nome == null || x.Nome == Nome))
                .OrderByDescending(x => x.Nome)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}
