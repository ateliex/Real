using Real.Data;
using Real.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real.Models;

namespace Real.Pages.Contas;

public class ExcluirModel : FormPageModel
{
    private readonly RealDbContext _db;

    [BindProperty]
    public Conta Conta { get; set; }

    public ExcluirModel(RealDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == null || _db.Contas == null)
        {
            return NotFound();
        }

        var conta = await _db.Contas.FirstOrDefaultAsync(m => m.Id == id);

        if (conta == null)
        {
            return NotFound();
        }
        else
        {
            Conta = conta;
        }

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (id == null || _db.Contas == null)
        {
            return NotFound();
        }

        var conta = await _db.Contas.FindAsync(id);

        if (conta != null)
        {
            Conta = conta;

            _db.Contas.Remove(Conta);

            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Conta excluída com sucesso");

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
