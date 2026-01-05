using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Extensions;
using Real.Models;

namespace Real.Pages.ApuracaoAnual;

public class EditarModel : FormPageModel
{
    private readonly RealDbContext _db;

    [BindProperty]
    public Categoria Categoria { get; set; } = default!;

    public EditarModel(RealDbContext db)
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

        Categoria = categoria;

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(string? id)
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Categoria.Id = id;
        Categoria.CreationDate = transaction.DateTime;

        _db.Attach(Categoria).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoriaExists(Categoria.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Categoria.Id });

        AddTempSuccessMessage("Categoria editado com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool CategoriaExists(string? id)
    {
        return _db.Categorias.Any(e => e.Id == id);
    }
}
