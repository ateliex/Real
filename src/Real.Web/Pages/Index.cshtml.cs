using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Real.Models;
using System.ComponentModel;

namespace Real.Pages;

public class IndexModel : PageModel
{
    private readonly ApuracaoService _apuracaoService;

    private readonly ILogger<IndexModel> _logger;

    [BindProperty(SupportsGet = true)]
    [DisplayName("Ano")]
    public int? Ano { get; set; }

    [BindProperty(SupportsGet = true)]
    public ModoVisualizacaoEnum ModoVisualizacao { get; set; } = ModoVisualizacaoEnum.Tabela;

    [BindProperty(SupportsGet = true)]
    public OrdemEnum Ordem { get; set; } = OrdemEnum.Padrao;

    [BindProperty(SupportsGet = true)]
    public RegimeApuracaoEnum RegimeApuracao { get; set; } = RegimeApuracaoEnum.Competencia;

    [BindProperty(SupportsGet = true)]
    public bool ExibirTodasCategorias { get; set; } = false;

    public ApuracaoAnualFinancasPorCategoriaModel Apuracao { get; set; }

    public List<CategoriaApuradaPorAnoModel> Receitas { get; set; }

    public List<CategoriaApuradaPorAnoModel> Despesas { get; set; }

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

        if (Ano == null)
        {
            Ano = hoje.Year;
        }

        var competencia = new DateOnly(Ano.Value, 1, 1);

        var apuracaoAnualCategorias = await _apuracaoService.ApurarCategoriasPorCompetenciaAnual(competencia, RegimeApuracao);

        var apuracaoAnualCategoriasModel = await MapFrom(apuracaoAnualCategorias);

        Apuracao = apuracaoAnualCategoriasModel;
    }

    private async Task<ApuracaoAnualFinancasPorCategoriaModel> MapFrom(ApuracaoCategorias apuracaoCategorias)
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
                    .OrderByDescending(x => x.ValorTotal)
                    .ToList();

                Despesas = despesas
                    .OrderBy(x => x.ValorTotal)
                    .ToList();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        //

        var competencia = apuracaoCategorias.Competencia;

        var valorJaneiroReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(0)).Sum(x => x.Valor);
        var valorFevereiroReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(1)).Sum(x => x.Valor);
        var valorMarcoReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(2)).Sum(x => x.Valor);
        var valorAbrilReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(3)).Sum(x => x.Valor);
        var valorMaioReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(4)).Sum(x => x.Valor);
        var valorJunhoReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(5)).Sum(x => x.Valor);
        var valorJulhoReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(6)).Sum(x => x.Valor);
        var valorAgostoReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(7)).Sum(x => x.Valor);
        var valorSetembroReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(8)).Sum(x => x.Valor);
        var valorOutubroReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(9)).Sum(x => x.Valor);
        var valorNovembroReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(10)).Sum(x => x.Valor);
        var valorDezembroReceitasTotal = apuracaoCategorias.Receitas.Where(x => x.Competencia == competencia.AddMonths(11)).Sum(x => x.Valor);

        var valorReceitasTotal = valorJaneiroReceitasTotal + valorFevereiroReceitasTotal + valorMarcoReceitasTotal + valorAbrilReceitasTotal + valorMaioReceitasTotal + valorJunhoReceitasTotal + valorJulhoReceitasTotal + valorAgostoReceitasTotal + valorSetembroReceitasTotal + valorOutubroReceitasTotal + valorNovembroReceitasTotal + valorDezembroReceitasTotal;

        var valorJaneiroDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(0)).Sum(x => x.Valor);
        var valorFevereiroDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(1)).Sum(x => x.Valor);
        var valorMarcoDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(2)).Sum(x => x.Valor);
        var valorAbrilDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(3)).Sum(x => x.Valor);
        var valorMaioDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(4)).Sum(x => x.Valor);
        var valorJunhoDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(5)).Sum(x => x.Valor);
        var valorJulhoDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(6)).Sum(x => x.Valor);
        var valorAgostoDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(7)).Sum(x => x.Valor);
        var valorSetembroDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(8)).Sum(x => x.Valor);
        var valorOutubroDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(9)).Sum(x => x.Valor);
        var valorNovembroDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(10)).Sum(x => x.Valor);
        var valorDezembroDespesasTotal = apuracaoCategorias.Despesas.Where(x => x.Competencia == competencia.AddMonths(11)).Sum(x => x.Valor);

        var valorDespesasTotal = valorJaneiroDespesasTotal + valorFevereiroDespesasTotal + valorMarcoDespesasTotal + valorAbrilDespesasTotal + valorMaioDespesasTotal + valorJunhoDespesasTotal + valorJulhoDespesasTotal + valorAgostoDespesasTotal + valorSetembroDespesasTotal + valorOutubroDespesasTotal + valorNovembroDespesasTotal + valorDezembroDespesasTotal;

        var valorJaneiroSaldoTotal = valorJaneiroReceitasTotal + valorJaneiroDespesasTotal;
        var valorFevereiroSaldoTotal = valorFevereiroReceitasTotal + valorFevereiroDespesasTotal;
        var valorMarcoSaldoTotal = valorMarcoReceitasTotal + valorMarcoDespesasTotal;
        var valorAbrilSaldoTotal = valorAbrilReceitasTotal + valorAbrilDespesasTotal;
        var valorMaioSaldoTotal = valorMaioReceitasTotal + valorMaioDespesasTotal;
        var valorJunhoSaldoTotal = valorJunhoReceitasTotal + valorJunhoDespesasTotal;
        var valorJulhoSaldoTotal = valorJulhoReceitasTotal + valorJulhoDespesasTotal;
        var valorAgostoSaldoTotal = valorAgostoReceitasTotal + valorAgostoDespesasTotal;
        var valorSetembroSaldoTotal = valorSetembroReceitasTotal + valorSetembroDespesasTotal;
        var valorOutubroSaldoTotal = valorOutubroReceitasTotal + valorOutubroDespesasTotal;
        var valorNovembroSaldoTotal = valorNovembroReceitasTotal + valorNovembroDespesasTotal;
        var valorDezembroSaldoTotal = valorDezembroReceitasTotal + valorDezembroDespesasTotal;

        var valorSaldoTotal = valorReceitasTotal + valorDespesasTotal;

        var apuracao = new ApuracaoAnualFinancasPorCategoriaModel
        {
            ValorJaneiroReceitasTotal = valorJaneiroReceitasTotal,
            ValorFevereiroReceitasTotal = valorFevereiroReceitasTotal,
            ValorMarcoReceitasTotal = valorMarcoReceitasTotal,
            ValorAbrilReceitasTotal = valorAbrilReceitasTotal,
            ValorMaioReceitasTotal = valorMaioReceitasTotal,
            ValorJunhoReceitasTotal = valorJunhoReceitasTotal,
            ValorJulhoReceitasTotal = valorJulhoReceitasTotal,
            ValorAgostoReceitasTotal = valorAgostoReceitasTotal,
            ValorSetembroReceitasTotal = valorSetembroReceitasTotal,
            ValorOutubroReceitasTotal = valorOutubroReceitasTotal,
            ValorNovembroReceitasTotal = valorNovembroReceitasTotal,
            ValorDezembroReceitasTotal = valorDezembroReceitasTotal,
            ValorReceitasTotal = valorReceitasTotal,
            ValorJaneiroDespesasTotal = valorJaneiroDespesasTotal,
            ValorFevereiroDespesasTotal = valorFevereiroDespesasTotal,
            ValorMarcoDespesasTotal = valorMarcoDespesasTotal,
            ValorAbrilDespesasTotal = valorAbrilDespesasTotal,
            ValorMaioDespesasTotal = valorMaioDespesasTotal,
            ValorJunhoDespesasTotal = valorJunhoDespesasTotal,
            ValorJulhoDespesasTotal = valorJulhoDespesasTotal,
            ValorAgostoDespesasTotal = valorAgostoDespesasTotal,
            ValorSetembroDespesasTotal = valorSetembroDespesasTotal,
            ValorOutubroDespesasTotal = valorOutubroDespesasTotal,
            ValorNovembroDespesasTotal = valorNovembroDespesasTotal,
            ValorDezembroDespesasTotal = valorDezembroDespesasTotal,
            ValorDespesasTotal = valorDespesasTotal,
            ValorJaneiroSaldoTotal = valorJaneiroSaldoTotal,
            ValorFevereiroSaldoTotal = valorFevereiroSaldoTotal,
            ValorMarcoSaldoTotal = valorMarcoSaldoTotal,
            ValorAbrilSaldoTotal = valorAbrilSaldoTotal,
            ValorMaioSaldoTotal = valorMaioSaldoTotal,
            ValorJunhoSaldoTotal = valorJunhoSaldoTotal,
            ValorJulhoSaldoTotal = valorJulhoSaldoTotal,
            ValorAgostoSaldoTotal = valorAgostoSaldoTotal,
            ValorSetembroSaldoTotal = valorSetembroSaldoTotal,
            ValorOutubroSaldoTotal = valorOutubroSaldoTotal,
            ValorNovembroSaldoTotal = valorNovembroSaldoTotal,
            ValorDezembroSaldoTotal = valorDezembroSaldoTotal,
            ValorAcumuladoJaneiro = apuracaoCategorias.ValorAcumuladoAnterior,
            ValorAcumuladoFevereiro = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal,
            ValorAcumuladoMarco = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal,
            ValorAcumuladoAbril = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal,
            ValorAcumuladoMaio = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal,
            ValorAcumuladoJunho = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal,
            ValorAcumuladoJulho = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal + valorJunhoSaldoTotal,
            ValorAcumuladoAgosto = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal + valorJunhoSaldoTotal + valorJulhoSaldoTotal,
            ValorAcumuladoSetembro = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal + valorJunhoSaldoTotal + valorJulhoSaldoTotal + valorAgostoSaldoTotal,
            ValorAcumuladoOutubro = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal + valorJunhoSaldoTotal + valorJulhoSaldoTotal + valorAgostoSaldoTotal + valorSetembroSaldoTotal,
            ValorAcumuladoNovembro = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal + valorJunhoSaldoTotal + valorJulhoSaldoTotal + valorAgostoSaldoTotal + valorSetembroSaldoTotal + valorOutubroSaldoTotal,
            ValorAcumuladoDezembro = apuracaoCategorias.ValorAcumuladoAnterior + valorJaneiroSaldoTotal + valorFevereiroSaldoTotal + valorMarcoSaldoTotal + valorAbrilSaldoTotal + valorMaioSaldoTotal + valorJunhoSaldoTotal + valorJulhoSaldoTotal + valorAgostoSaldoTotal + valorSetembroSaldoTotal + valorOutubroSaldoTotal + valorNovembroSaldoTotal,
            ValorSaldoTotal = valorSaldoTotal,
            ValorTotal = apuracaoCategorias.ValorAcumuladoAnterior + valorSaldoTotal
        };

        //

        //decimal valorAcumuladoAnterior;

        //if (RegimeApuracao == RegimeApuracaoEnum.Competencia)
        //{
        //    valorAcumuladoAnterior = await _contasRepository.ObtemValorAcumuladoPorCompetencia(competencia);
        //}
        //else
        //{
        //    valorAcumuladoAnterior = await _contasRepository.ObtemValorAcumuladoPorData(competencia);
        //}

        //var valorAcumuladoTotal = valorAcumuladoAnterior + valorSaldoTotal;

        return apuracao;
    }

    private List<CategoriaApuradaPorAnoModel> MapFrom(IEnumerable<ApuracaoCategoria> apuracoesMensaisCategoria)
    {
        var apuracaoAnualCategoriaList = new List<CategoriaApuradaPorAnoModel>();

        var apuracoesCategoria = apuracoesMensaisCategoria
            .GroupBy(x => new
            {
                x.CategoriaId,
                CategoriaNome = x.Categoria.Nome,
                x.Categoria.AplicaReceita,
                x.Categoria.AplicaDespesa,
                x.Categoria.Ordem,
                x.Categoria.IconId
            });

        foreach (var apuracaoCategoria in apuracoesCategoria)
        {
            //var financasPorCategoria = apuracaoCategoria.Financas
            //    //.OrderBy(x => x.Competencia)
            //    .Where(x => x.CategoriaId == apuracaoCategoria.CategoriaId);

            var valorJaneiro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 1).Sum(y => y.Valor);
            var valorFevereiro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 2).Sum(y => y.Valor);
            var valorMarco = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 3).Sum(y => y.Valor);
            var valorAbril = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 4).Sum(y => y.Valor);
            var valorMaio = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 5).Sum(y => y.Valor);
            var valorJunho = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 6).Sum(y => y.Valor);
            var valorJulho = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 7).Sum(y => y.Valor);
            var valorAgosto = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 8).Sum(y => y.Valor);
            var valorSetembro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 9).Sum(y => y.Valor);
            var valorOutubro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 10).Sum(y => y.Valor);
            var valorNovembro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 11).Sum(y => y.Valor);
            var valorDezembro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 12).Sum(y => y.Valor);

            var valorTotal = valorJaneiro + valorFevereiro + valorMarco + valorAbril + valorMaio + valorJunho + valorJulho + valorAgosto + valorSetembro + valorOutubro + valorNovembro + valorDezembro;

            //var valorJaneiro = apuracoesCategoria.Where(x => x.Competencia.Month == 1 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorFevereiro = apuracoesCategoria.Where(x => x.Competencia.Month == 2 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorMarco = apuracoesCategoria.Where(x => x.Competencia.Month == 3 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorAbril = apuracoesCategoria.Where(x => x.Competencia.Month == 4 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorMaio = apuracoesCategoria.Where(x => x.Competencia.Month == 5 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorJunho = apuracoesCategoria.Where(x => x.Competencia.Month == 6 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorJulho = apuracoesCategoria.Where(x => x.Competencia.Month == 7 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorAgosto = apuracoesCategoria.Where(x => x.Competencia.Month == 8 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorSetembro = apuracoesCategoria.Where(x => x.Competencia.Month == 9 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorOutubro = apuracoesCategoria.Where(x => x.Competencia.Month == 10 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorNovembro = apuracoesCategoria.Where(x => x.Competencia.Month == 11 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorDezembro = apuracoesCategoria.Where(x => x.Competencia.Month == 12 && !x.EhPrevisao).Sum(y => y.Valor);

            //var valorPrevistoJaneiro = apuracoesCategoria.Where(x => x.Competencia.Month == 1 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoFevereiro = apuracoesCategoria.Where(x => x.Competencia.Month == 2 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoMarco = apuracoesCategoria.Where(x => x.Competencia.Month == 3 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoAbril = apuracoesCategoria.Where(x => x.Competencia.Month == 4 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoMaio = apuracoesCategoria.Where(x => x.Competencia.Month == 5 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoJunho = apuracoesCategoria.Where(x => x.Competencia.Month == 6 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoJulho = apuracoesCategoria.Where(x => x.Competencia.Month == 7 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoAgosto = apuracoesCategoria.Where(x => x.Competencia.Month == 8 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoSetembro = apuracoesCategoria.Where(x => x.Competencia.Month == 9 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoOutubro = apuracoesCategoria.Where(x => x.Competencia.Month == 10 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoNovembro = apuracoesCategoria.Where(x => x.Competencia.Month == 11 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoDezembro = apuracoesCategoria.Where(x => x.Competencia.Month == 12 && x.EhPrevisao).Sum(y => y.Valor);

            //var valorTotal = apuracoesCategoria.Sum(y => y.Valor);

            var valorTotalAbsoluto = Math.Abs(valorTotal);

            if (Math.Abs(valorTotalAbsoluto) > 0 || ExibirTodasCategorias)
            {
                //var valorMeta = controleCategoria.Sum(y => y.ValorMeta);

                //var calculaValor = (decimal valor, decimal valorPrevisto) =>
                //{
                //    var valorAbsoluto = Math.Abs(valor);
                //    var valorPrevistoAbsoluto = Math.Abs(valorPrevisto);

                //    if (valorAbsoluto > valorPrevistoAbsoluto)
                //    {
                //        return valor;
                //    }
                //    else
                //    {
                //        return valorPrevisto;
                //    }
                //};

                var item = new CategoriaApuradaPorAnoModel
                {
                    CategoriaId = apuracaoCategoria.Key.CategoriaId,
                    CategoriaNome = apuracaoCategoria.Key.CategoriaNome,
                    AplicaReceita = apuracaoCategoria.Key.AplicaReceita,
                    AplicaDespesa = apuracaoCategoria.Key.AplicaDespesa,
                    Ordem = apuracaoCategoria.Key.Ordem.Value,
                    IconId = apuracaoCategoria.Key.IconId,
                    //ValorPrevistoAnual = categoria.ValorPrevistoAnual,
                    //ValorPrevistoMensal = categoria.ValorPrevistoMensal,
                    //ValorJaneiro = calculaValor(valorJaneiro, valorPrevistoJaneiro),
                    //ValorFevereiro = calculaValor(valorFevereiro, valorPrevistoFevereiro),
                    //ValorMarco = calculaValor(valorMarco, valorPrevistoMarco),
                    //ValorAbril = calculaValor(valorAbril, valorPrevistoAbril),
                    //ValorMaio = calculaValor(valorMaio, valorPrevistoMaio),
                    //ValorJunho = calculaValor(valorJunho, valorPrevistoJunho),
                    //ValorJulho = calculaValor(valorJulho, valorPrevistoJulho),
                    //ValorAgosto = calculaValor(valorAgosto, valorPrevistoAgosto),
                    //ValorSetembro = calculaValor(valorSetembro, valorPrevistoSetembro),
                    //ValorOutubro = calculaValor(valorOutubro, valorPrevistoOutubro),
                    //ValorNovembro = calculaValor(valorNovembro, valorPrevistoOutubro),
                    //ValorDezembro = calculaValor(valorDezembro, valorPrevistoDezembro),
                    ValorJaneiro = valorJaneiro,
                    ValorFevereiro = valorFevereiro,
                    ValorMarco = valorMarco,
                    ValorAbril = valorAbril,
                    ValorMaio = valorMaio,
                    ValorJunho = valorJunho,
                    ValorJulho = valorJulho,
                    ValorAgosto = valorAgosto,
                    ValorSetembro = valorSetembro,
                    ValorOutubro = valorOutubro,
                    ValorNovembro = valorNovembro,
                    ValorDezembro = valorDezembro,
                    //ValorJaneiro = valorPrevistoJaneiro,
                    //ValorFevereiro = valorPrevistoFevereiro,
                    //ValorMarco = valorPrevistoMarco,
                    //ValorAbril = valorPrevistoAbril,
                    //ValorMaio = valorPrevistoMaio,
                    //ValorJunho = valorPrevistoJunho,
                    //ValorJulho = valorPrevistoJulho,
                    //ValorAgosto = valorPrevistoAgosto,
                    //ValorSetembro = valorPrevistoSetembro,
                    //ValorOutubro = valorPrevistoOutubro,
                    //ValorNovembro = valorPrevistoNovembro,
                    //ValorDezembro = valorPrevistoDezembro,
                    ValorTotal = valorTotal,
                    Financas = apuracaoCategoria.SelectMany(y => y.Financas.Select(x => new FinancaPorCategoriaModel
                    {
                        FinancaId = x.Id,
                        TipoRegistroId = x.TipoRegistroId,
                        TipoFinancaId = x.TipoFinancaId,
                        ContaTipoId = x.Conta.TipoContaId,
                        ContaNome = x.Conta.Nome,
                        Competencia = x.Competencia,
                        Data = x.Data,
                        Descricao = x.Descricao,
                        Valor = x.Valor,
                        ValorOriginal = (x as PrevisaoInteligente)?.ValorOriginal,
                        ValorExcedente = (x as PrevisaoInteligente)?.ValorExcedente,
                        EhPrevisao = x.EhPrevisao,
                        EhRecorrente = (x as FinancaAVista)?.EhRecorrente ?? false
                    })).ToList()
                };

                apuracaoAnualCategoriaList.Add(item);
            }
        }

        return apuracaoAnualCategoriaList;
    }
}
