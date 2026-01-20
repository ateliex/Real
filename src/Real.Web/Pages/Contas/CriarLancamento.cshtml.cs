using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Real.Data;
using Real.Extensions;
using Real.Models;

namespace Real.Pages.Contas;

public class CriarLancamentoModel : FormPageModel
{
    private readonly RealDbContext _db;

    [BindProperty]
    public FinancaAVista Lancamento { get; set; }

    public CriarLancamentoModel(RealDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet(Guid? contaId, string? categoriaId)
    {
        ViewData["ContaId"] = new SelectList(_db.Contas.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "Id", "Nome", contaId);
        
        //ViewData["TipoRegistroId"] = new SelectList(_db.CobrancaSituacoes.OrderBy(x => x.Id), "Id", "Nome").AddEmptyValue();

        ViewData["CategoriaId"] = new SelectList(_db.Categorias.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "Id", "Nome", categoriaId);

        //Lancamento = new FinancaAVista() { Descricao = "" };

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(Guid? contaId, string? categoriaId)
    {
        var transaction = User.CreateTransaction();

        Lancamento.CreationDate = transaction.DateTime;

        if (!ModelState.IsValid)
        {
            ViewData["ContaId"] = new SelectList(_db.Contas.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "Id", "Nome", contaId);
            
            ViewData["CategoriaId"] = new SelectList(_db.Categorias.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "Id", "Nome", categoriaId);

            return Page();
        }

        var conta = await _db.Contas.FindAsync(Lancamento.ContaId);

        Lancamento.Conta = conta;
        Lancamento.ContaId = conta.Id;

        var categoria = await _db.Categorias.FindAsync(Lancamento.CategoriaId);

        Lancamento.Categoria = categoria;
        Lancamento.CategoriaId = categoria.Id;

        _db.Lancamentos.Add(Lancamento);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Lancamento.Id });

        AddTempSuccessMessageWithDetailLink("Lançamento criado com sucesso", detalharPage);

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
