using Microsoft.AspNetCore.Mvc;
using Real.Data;
using Real.Extensions;
using Real.Models;

namespace Real.Pages.Categorias;

public class CriarModel : FormPageModel
{
    private readonly RealDbContext _db;

    [BindProperty]
    public Categoria CadastroCategoria { get; set; }

    public CriarModel(RealDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        CadastroCategoria.CreationDate = transaction.DateTime;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.Categorias.Add(CadastroCategoria);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = CadastroCategoria.Id });

        AddTempSuccessMessageWithDetailLink("Categoria criado com sucesso", detalharPage);

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }
}
