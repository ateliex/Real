using Real.Data;
using Real.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real.Models;

namespace Real.Pages.ApuracaoAnual;

public class ExcluirModel : FormPageModel
{
    private readonly RealDbContext _db;

    [BindProperty]
    public Categoria Categoria { get; set; }

    public ExcluirModel(RealDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null || _db.Categorias == null)
        {
            return NotFound();
        }

        var categoria = await _db.Categorias.FirstOrDefaultAsync(m => m.Id == id);

        if (categoria == null)
        {
            return NotFound();
        }
        else
        {
            Categoria = categoria;
        }

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? id)
    {
        if (id == null || _db.Categorias == null)
        {
            return NotFound();
        }

        var categoria = await _db.Categorias.FindAsync(id);

        if (categoria != null)
        {
            Categoria = categoria;

            _db.Categorias.Remove(Categoria);

            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Categoria excluído com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return RedirectToPage("./Index");
        }
    }
}
