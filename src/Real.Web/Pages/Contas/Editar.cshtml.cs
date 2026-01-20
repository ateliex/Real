using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Extensions;
using Real.Models;

namespace Real.Pages.Contas;

public class EditarModel : FormPageModel
{
    private readonly RealDbContext _db;

    [BindProperty]
    public Conta Conta { get; set; } = default!;

    public EditarModel(RealDbContext db)
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

        Conta = conta;

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Conta.Id = id;
        Conta.CreationDate = transaction.DateTime;

        _db.Attach(Conta).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContaExists(Conta.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Conta.Id });

        AddTempSuccessMessage("Conta editada com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool ContaExists(Guid id)
    {
        return _db.Contas.Any(e => e.Id == id);
    }
}
