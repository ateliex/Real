using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Models;

namespace Real.Pages.Categorias;

public class DetalharModel : PageModel
{
    private readonly RealDbContext _db;

    public DetalharModel(RealDbContext db)
    {
        _db = db;
    }

  public Categoria Categoria { get; set; }

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
        return Page();
    }
}
