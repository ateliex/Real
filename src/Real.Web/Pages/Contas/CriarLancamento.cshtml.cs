using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Real.Data;
using Real.Extensions;
using Real.Models;
using Real.Repositories;

namespace Real.Pages.Contas;

public class CriarLancamentoModel : FormPageModel
{
    private readonly RealDbContext _db;
    private readonly ContasRepositoryInterface _contasRepositoryInterface;

    [BindProperty]
    public LancamentoInputModel Input { get; set; }

    public CriarLancamentoModel(RealDbContext db, ContasRepositoryInterface contasRepositoryInterface)
    {
        _db = db;
        _contasRepositoryInterface = contasRepositoryInterface;
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

        if (!ModelState.IsValid)
        {
            ViewData["ContaId"] = new SelectList(_db.Contas.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "Id", "Nome", contaId);

            ViewData["CategoriaId"] = new SelectList(_db.Categorias.Where(x => x.Ativa == true).OrderBy(x => x.Nome), "Id", "Nome", categoriaId);

            return Page();
        }

        Input.CreationDate = transaction.DateTime;

        var conta = await _db.Contas.FindAsync(Input.ContaId);

        var categoria = await _db.Categorias.FindAsync(Input.CategoriaId);
        
        var lancamento = new FinancaAVista(
            conta,
            Guid.NewGuid(),
            Input.TipoLancamentoId,
            Input.TipoCompetenciaId,
            Input.Data,
            Input.Descricao,
            Input.Valor,
            Input.TipoFinancaId,
            categoria,
            Input.EhPrevisao,
            null);

        await conta.Adiciona(lancamento, _contasRepositoryInterface);

        _db.Contas.Update(conta);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Input.Id });

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
