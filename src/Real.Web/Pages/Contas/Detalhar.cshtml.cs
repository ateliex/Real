using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Models;

namespace Real.Pages.Contas;

public class DetalharModel : PageModel
{
    private readonly RealDbContext _db;

    public DetalharModel(RealDbContext db)
    {
        _db = db;
    }

  public Conta Conta { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == null || _db.Contas == null)
        {
            return NotFound();
        }

        var categoria = await _db.Contas.FirstOrDefaultAsync(m => m.Id == id);
        if (categoria == null)
        {
            return NotFound();
        }
        else 
        {
            Conta = categoria;
        }
        return Page();
    }
}
