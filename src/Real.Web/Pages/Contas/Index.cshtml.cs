using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Real.Data;
using Real.Models;
using Real.Models;
using Real.Repositories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Real.Pages.Contas;

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

    public ApuracaoFinancasPorContaModel Apuracao { get; set; }

    public List<ContaApuradaModel> CreditosAReceber { get; set; }

    public List<ContaApuradaModel> CreditosAPagar { get; set; }

    public List<ContaApuradaModel> Debitos { get; set; }

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

        var apuracaoContas = await _apuracaoService.ApurarContasPorCompetencia(competencia, RegimeApuracao); //if (Math.Abs(valorApurado) > 0 || ExibirTodasCategorias)

        var apuracaoContasModel = await MapFrom(apuracaoContas);

        Apuracao = apuracaoContasModel;
    }

    private async Task<ApuracaoFinancasPorContaModel> MapFrom(ApuracaoContas apuracaoContas)
    {
        var creditosAReceber = MapFrom(apuracaoContas.CreditosAReceber);

        var creditosAPagar = MapFrom(apuracaoContas.CreditosAPagar);

        var debitos = MapFrom(apuracaoContas.Debitos);

        //

        switch (Ordem)
        {
            case OrdemEnum.Padrao:
                CreditosAReceber = creditosAReceber
                    .OrderBy(x => x.Ordem)
                    .ToList();

                CreditosAPagar = creditosAPagar
                    .OrderBy(x => x.Ordem)
                    .ToList();

                Debitos = debitos
                    .OrderBy(x => x.Ordem)
                    .ToList();

                break;
            case OrdemEnum.Decrescente:
                CreditosAReceber = creditosAReceber
                    .OrderByDescending(x => x.Valor)
                    .ToList();

                CreditosAPagar = creditosAPagar
                    .OrderByDescending(x => x.Valor)
                    .ToList();

                Debitos = debitos
                    .OrderByDescending(x => x.Valor)
                    .ToList();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        //

        var apuracao = new ApuracaoFinancasPorContaModel
        {
            Competencia = apuracaoContas.Competencia,
            StatusId = StatusApuracaoEnum.Aberta,
            ValorAcumuladoAnterior = apuracaoContas.ValorAcumuladoAnterior,
            ValorAcumuladoTotal = apuracaoContas.ValorAcumulado,
            ValorSaldoTotal = apuracaoContas.ValorSaldo,
            ValorDebitosTotal = apuracaoContas.ValorDebitos,
            ValorSaldoComDebitosTotal = apuracaoContas.ValorSaldoComDebitos,
            ValorCreditosAReceberTotal = apuracaoContas.ValorCreditosAReceber,
            ValorCreditosAPagarTotal = apuracaoContas.ValorCreditosAPagar,
        };

        return await Task.FromResult(apuracao);
    }

    private static List<ContaApuradaModel> MapFrom(IEnumerable<ApuracaoConta> apuracoesConta)
    {
        var apuracaoContaList = new List<ContaApuradaModel>();

        foreach (var apuracaoConta in apuracoesConta)
        {
            var apuracaoContaModel = new ContaApuradaModel
            {
                ContaId = apuracaoConta.Conta.Id,
                ContaNome = apuracaoConta.Conta.Nome,
                TipoContaId = apuracaoConta.Conta.TipoContaId,
                Ordem = apuracaoConta.Conta.Ordem,
                Valor = apuracaoConta.Valor,
                Pessoa = apuracaoConta.Conta.Pessoa,
                Lancamentos = apuracaoConta.Lancamentos.Select(x => new LancamentoPorContaModel
                {
                    LancamentoId = x.Id,
                    TipoRegistroId = x.TipoRegistroId,
                    TipoFinancaId = (x as Financa)?.TipoFinancaId,
                    CategoriaNome = (x as Financa)?.Categoria?.Nome,
                    CategoriaBiIcon = (x as Financa)?.Categoria?.IconId,
                    Competencia = x.Competencia, // DateOnly.FromDateTime(x.Data),
                    Data = x.Data,
                    Descricao = x.Descricao,
                    Transacao = x.Transacao,
                    Valor = x.Valor,
                    ValorOriginal = (x as PrevisaoInteligente)?.ValorOriginal, //ValorOriginal = null,
                    ValorExcedente = (x as PrevisaoInteligente)?.ValorExcedente,
                    EhPrevisao = (x as Financa)?.EhPrevisao ?? false,
                    //Nivel = x.Nivel,
                    //LancamentoPaiId = x.GrupoId,
                    EhRecorrente = (x as FinancaAVista)?.EhRecorrente ?? false,
                }).ToList()
            };

            apuracaoContaList.Add(apuracaoContaModel);
        }

        return apuracaoContaList;
    }
}
