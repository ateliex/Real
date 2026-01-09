using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Real.Models;
using Real.Models;
using System.ComponentModel;

namespace Real.Pages.ApuracaoMensal;

public class IndexModel : PageModel
{
    private readonly ApuracaoService _apuracaoService;

    private readonly ILogger<IndexModel> _logger;

    [BindProperty(SupportsGet = true)]
    [DisplayName("Competência")]
    public DateOnly? Competencia { get; set; }

    [BindProperty(SupportsGet = true)]
    public ModoVisualizacaoEnum ModoVisualizacao { get; set; } = ModoVisualizacaoEnum.Tabela;

    [BindProperty(SupportsGet = true)]
    public OrdemEnum Ordem { get; set; } = OrdemEnum.Padrao;

    [BindProperty(SupportsGet = true)]
    public RegimeApuracaoEnum RegimeApuracao { get; set; } = RegimeApuracaoEnum.Competencia;

    [BindProperty(SupportsGet = true)]
    public bool ExibirTodasCategorias { get; set; } = false;

    public ApuracaoFinancasPorCategoriaModel Apuracao { get; set; }

    public List<CategoriaApuradaModel> Receitas { get; set; }

    public List<CategoriaApuradaModel> Despesas { get; set; }

    public IndexModel(
        ApuracaoService apuracaoService,
        ILogger<IndexModel> logger)
    {
        _apuracaoService = apuracaoService;
        _logger = logger;
    }

    public async Task OnGet()
    {
        var hoje = DateTime.Today;

        if (Competencia == null)
        {
            Competencia = DateOnly.FromDateTime(hoje);
        }

        var competencia = Competencia.Value;

        var apuracaoCategorias = await _apuracaoService.ApurarCategoriasPorCompetencia(competencia, RegimeApuracao);

        var apuracaoCategoriasModel = await MapFrom(apuracaoCategorias);

        Apuracao = apuracaoCategoriasModel;
    }

    private async Task<ApuracaoFinancasPorCategoriaModel> MapFrom(ApuracaoCategorias apuracaoCategorias)
    {
        var receitas = MapFrom(apuracaoCategorias.Receitas);

        var despesas = MapFrom(apuracaoCategorias.Despesas);

        //

        switch (Ordem)
        {
            case OrdemEnum.Padrao:
                Receitas = receitas
                    .OrderBy(x => x.Ordem)
                    .ToList();

                Despesas = despesas
                    .OrderBy(x => x.Ordem)
                    .ToList();

                break;
            case OrdemEnum.Decrescente:
                Receitas = receitas
                    .OrderByDescending(x => x.Valor)
                    .ToList();

                Despesas = despesas
                    .OrderBy(x => x.Valor)
                    .ToList();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        //

        var apuracao = new ApuracaoFinancasPorCategoriaModel
        {
            Competencia = apuracaoCategorias.Competencia,
            StatusId = StatusApuracaoEnum.Aberta,
            ValorAcumuladoAnterior = apuracaoCategorias.ValorAcumuladoAnterior,
            ValorAcumuladoTotal = apuracaoCategorias.ValorAcumulado,
            ValorSaldoTotal = apuracaoCategorias.ValorSaldo,
            ValorReceitasTotal = apuracaoCategorias.ValorReceitas,
            ValorDespesasTotal = apuracaoCategorias.ValorDespesas,
        };

        return await Task.FromResult(apuracao);
    }

    private List<CategoriaApuradaModel> MapFrom(IEnumerable<ApuracaoCategoria> apuracoesCategoria)
    {
        var apuracaoCategoriaList = new List<CategoriaApuradaModel>();

        foreach (var apuracaoCategoria in apuracoesCategoria)
        {
            if (Math.Abs(apuracaoCategoria.Valor) > 0 || ExibirTodasCategorias)
            {
                var item = new CategoriaApuradaModel
                {
                    CategoriaId = apuracaoCategoria.CategoriaId,
                    CategoriaNome = apuracaoCategoria.Categoria.Nome,
                    AplicaReceita = apuracaoCategoria.Categoria.AplicaReceita,
                    AplicaDespesa = apuracaoCategoria.Categoria.AplicaDespesa,
                    Ordem = apuracaoCategoria.Categoria.Ordem.Value,
                    BiIcon = apuracaoCategoria.Categoria.IconId,
                    Valor = apuracaoCategoria.Valor,
                    Financas = apuracaoCategoria.Financas.Select(x => new FinancaPorCategoriaModel
                    {
                        FinancaId = x.Id,
                        TipoRegistroId = x.TipoRegistroId,
                        TipoFinancaId = x.TipoFinancaId,
                        //ContaTipoId = x.FormaRegistroId,
                        //ContaNome = x.Conta.Nome,
                        Competencia = x.Competencia,
                        Data = x.Data,
                        Descricao = x.Descricao,
                        Valor = x.Valor,
                        ValorOriginal = (x as PrevisaoInteligente)?.ValorOriginal,
                        ValorExcedente = (x as PrevisaoInteligente)?.ValorExcedente,
                        EhPrevisao = x.EhPrevisao,
                        EhRecorrente = (x as FinancaAVista)?.RecorrenciaId.HasValue ?? false
                    })
                    .ToList()
                };

                apuracaoCategoriaList.Add(item);
            }
        }

        return apuracaoCategoriaList;
    }
}
