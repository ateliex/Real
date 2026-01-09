using Real.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DomainModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Real.Models;

public class Apuracao
{
    public DateOnly Competencia { get; set; }

    public StatusApuracaoEnum? StatusId { get; set; }

    public decimal? ValorPorCompetencia { get; set; }

    public decimal? ValorPorData { get; set; }

    public string? Observacao { get; set; }

    //public ICollection<Lancamento> ObtemLancamentosPorCompetencia()
    //{
    //    var dataInicio = Competencia;
    //    var dataFim = Competencia.AddMonths(1);

    //    var lancamentos = Conta.Lancamentos.Where(x => true
    //        && x.Competencia >= dataInicio
    //        && x.Competencia < dataFim);

    //    return lancamentos.ToList();
    //}

    //public ICollection<Lancamento> ObtemLancamentosPorData()
    //{
    //    var dataInicio = Competencia.ToDateTime(TimeOnly.MinValue);
    //    var dataFim = Competencia.AddMonths(1).ToDateTime(TimeOnly.MinValue);

    //    var lancamentos = Conta.Lancamentos.Where(x => true
    //        && x.Data >= dataInicio
    //        && x.Data < dataFim);

    //    return lancamentos.ToList();
    //}
}

public class ApuracaoCategorias : ValueObject
{
    public DateOnly Competencia { get; set; }

    public decimal ValorReceitas { get; set; }

    public decimal ValorDespesas { get; set; }

    public decimal ValorSaldo { get; set; }

    public decimal ValorAcumuladoAnterior { get; set; }

    public decimal ValorAcumulado { get; set; }

    public required ICollection<ApuracaoCategoria> Receitas { get; set; }

    public required ICollection<ApuracaoCategoria> Despesas { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Competencia;
        yield return ValorReceitas;
        yield return ValorDespesas;
        yield return ValorSaldo;
        yield return ValorAcumuladoAnterior;
        yield return ValorAcumulado;
    }
}

public class ApuracaoCategoria : ValueObject
{
    public DateOnly Competencia { get; set; }

    public required Categoria Categoria { get; set; }

    public required string CategoriaId { get; set; }

    public decimal Valor { get; set; }

    public ICollection<Financa> Financas { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Competencia;
        yield return CategoriaId;
        yield return Valor;
    }
}

public class ApuracaoContas : ValueObject
{
    public DateOnly Competencia { get; set; }

    public decimal Valor { get; set; }

    public required ICollection<ApuracaoConta> Creditos { get; set; }

    public required ICollection<ApuracaoConta> Debitos { get; set; }

    public decimal ValorCreditos { get; set; }

    public decimal ValorSaldo { get; set; }

    public decimal ValorDebitos { get; set; }

    public decimal ValorSaldoComDebitos { get; set; }

    public decimal ValorAcumuladoAnterior { get; set; }

    public decimal ValorAcumulado { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Competencia;
        yield return ValorCreditos;
        yield return ValorSaldo;
        yield return ValorDebitos;
        yield return ValorSaldoComDebitos;
        yield return ValorAcumuladoAnterior;
        yield return ValorAcumulado;
    }
}

public class ApuracaoConta : ValueObject
{
    public DateOnly Competencia { get; set; }

    public required Categoria Categoria { get; set; }

    public required string CategoriaId { get; set; }

    public decimal Valor { get; set; }

    public ICollection<Financa> Lancamentos { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Competencia;
        yield return CategoriaId;
        yield return Valor;
    }
}

public class ApuracaoService
{
    private readonly FinancasInteligentesProcuder _financasInteligentesProcuder;
    private readonly ContasRepositoryInterface _contasRepository;
    private readonly CategoriasRepositoryInterface _categoriasRepository;

    public ApuracaoService(
        FinancasInteligentesProcuder financasInteligentesProcuder,
        ContasRepositoryInterface contasRepository,
        CategoriasRepositoryInterface categoriasRepository)
    {
        _financasInteligentesProcuder = financasInteligentesProcuder;
        _contasRepository = contasRepository;
        _categoriasRepository = categoriasRepository;
    }

    public async Task<ApuracaoCategorias> ApurarCategoriasPorCompetenciaAnual(DateOnly competencia, RegimeApuracaoEnum regimeApuracaoId)
    {
        var tipoRegistroId = regimeApuracaoId switch
        {
            RegimeApuracaoEnum.Competencia => TipoRegistroEnum.DeCompetencia,
            RegimeApuracaoEnum.Caixa => TipoRegistroEnum.DeCaixa,
            _ => throw new NotImplementedException()
        };

        var lancamentos = await _contasRepository.ConsultaFinancasPorCompetenciaAnual(competencia, tipoRegistroId);

        var financas = await _financasInteligentesProcuder.Procude(lancamentos);

        var categorias = await _categoriasRepository.ObtemCategorias();

        //

        var categoriasReceita = categorias.Where(x => x.AplicaReceita);

        var receitas = ApurarCategoriaPorCompetenciaAnual(competencia, financas, categoriasReceita, TipoFinancaEnum.Receita, tipoRegistroId);

        var valorReceitas = receitas.Sum(x => x.Valor);

        //

        var categoriasDespesa = categorias.Where(x => x.AplicaDespesa);

        var despesas = ApurarCategoriaPorCompetenciaAnual(competencia, financas, categoriasDespesa, TipoFinancaEnum.Despesa, tipoRegistroId);

        var valorDespesas = despesas.Sum(x => x.Valor);

        //

        var valorSaldo = valorReceitas + valorDespesas;

        var valorAcumuladoAnterior = await _contasRepository.ObtemValorAcumuladoPorCompetenciaAnual(competencia, tipoRegistroId);

        var valorAcumulado = valorAcumuladoAnterior + valorSaldo;

        var valorTotal = valorReceitas + valorDespesas;

        var apuracaoCategorias = new ApuracaoCategorias
        {
            Competencia = competencia,
            ValorReceitas = valorReceitas,
            ValorDespesas = valorDespesas,
            ValorSaldo = valorSaldo,
            ValorAcumuladoAnterior = valorAcumuladoAnterior,
            ValorAcumulado = valorAcumulado,
            Receitas = receitas,
            Despesas = despesas
        };


        return apuracaoCategorias;
    }

    private static ICollection<ApuracaoCategoria> ApurarCategoriaPorCompetenciaAnual(
        DateOnly competencia,
        IEnumerable<Financa> financas,
        IEnumerable<Categoria> categorias,
        TipoFinancaEnum tipoFinancaId,
        TipoRegistroEnum tipoRegistroId)
    {
        var apuracaoCategoriaList = new List<ApuracaoCategoria>();

        foreach (var categoria in categorias)
        {
            var query = financas
                .Where(x => x.TipoFinancaId == tipoFinancaId)
                .Where(x => x.CategoriaId == categoria.Id);

            IEnumerable<IGrouping<int, Financa>> financasPorCategoriaMensal;

            if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
            {
                financasPorCategoriaMensal = query.GroupBy(x => x.Competencia.Month);
            }
            else
            {
                financasPorCategoriaMensal = query.GroupBy(x => x.Data.Month);
            }

            foreach (var financasPorCategoria in financasPorCategoriaMensal)
            {
                var valorApurado = financasPorCategoria.Sum(x => x.Valor);

                var item = new ApuracaoCategoria
                {
                    Competencia = new DateOnly(competencia.Year, financasPorCategoria.Key, 1),
                    Categoria = categoria,
                    CategoriaId = categoria.Id,
                    Valor = valorApurado,
                    Financas = financasPorCategoria.ToList()
                };

                apuracaoCategoriaList.Add(item);
            }
        }

        return apuracaoCategoriaList;
    }

    public async Task<ApuracaoCategorias> ApurarCategoriasPorCompetencia(DateOnly competencia, RegimeApuracaoEnum regimeApuracaoId)
    {
        var tipoRegistroId = regimeApuracaoId switch
        {
            RegimeApuracaoEnum.Competencia => TipoRegistroEnum.DeCompetencia,
            RegimeApuracaoEnum.Caixa => TipoRegistroEnum.DeCaixa,
            _ => throw new NotImplementedException()
        };

        var lancamentos = await _contasRepository.ConsultaFinancasPorCompetencia(competencia, tipoRegistroId);

        var financas = await _financasInteligentesProcuder.Procude(lancamentos);

        var categorias = await _categoriasRepository.ObtemCategorias();

        //

        var categoriasReceita = categorias.Where(x => x.AplicaReceita);

        var receitas = ApurarCategoriaPorCompetencia(competencia, financas, categoriasReceita, TipoFinancaEnum.Receita);

        var valorReceitas = receitas.Sum(x => x.Valor);

        //

        var categoriasDespesa = categorias.Where(x => x.AplicaDespesa);

        var despesas = ApurarCategoriaPorCompetencia(competencia, financas, categoriasDespesa, TipoFinancaEnum.Despesa);

        var valorDespesas = despesas.Sum(x => x.Valor);

        //

        var valorSaldo = valorReceitas + valorDespesas;

        var valorAcumuladoAnterior = await _contasRepository.ObtemValorAcumuladoPorCompetencia(competencia, tipoRegistroId);

        var valorAcumulado = valorAcumuladoAnterior + valorSaldo;

        var valorTotal = valorReceitas + valorDespesas;

        var apuracaoCategorias = new ApuracaoCategorias
        {
            Competencia = competencia,
            ValorReceitas = valorReceitas,
            ValorDespesas = valorDespesas,
            ValorSaldo = valorSaldo,
            ValorAcumuladoAnterior = valorAcumuladoAnterior,
            ValorAcumulado = valorAcumulado,
            Receitas = receitas,
            Despesas = despesas
        };

        return apuracaoCategorias;
    }

    private static List<ApuracaoCategoria> ApurarCategoriaPorCompetencia(
        DateOnly competencia,
        IEnumerable<Financa> financas,
        IEnumerable<Categoria> categorias,
        TipoFinancaEnum tipoFinancaId)
    {
        var apuracaoCategoriaList = new List<ApuracaoCategoria>();

        foreach (var categoria in categorias)
        {
            var financasPorCategoria = financas
                .Where(x => x.TipoFinancaId == tipoFinancaId)
                .Where(x => x.CategoriaId == categoria.Id);

            var valorApurado = financasPorCategoria.Sum(x => x.Valor);

            var item = new ApuracaoCategoria
            {
                Competencia = competencia,
                Categoria = categoria,
                CategoriaId = categoria.Id,
                Valor = valorApurado,
                Financas = financasPorCategoria.ToList()
            };

            apuracaoCategoriaList.Add(item);
        }

        return apuracaoCategoriaList;
    }

    //public async Task<ApuracaoContas> ApurarContasPorCompetencia(DateOnly competencia, RegimeApuracaoEnum regimeApuracaoId)
    //{
    //    var tipoRegistroId = regimeApuracaoId switch
    //    {
    //        RegimeApuracaoEnum.Competencia => TipoRegistroEnum.DeCompetencia,
    //        RegimeApuracaoEnum.Caixa => TipoRegistroEnum.DeCaixa,
    //        _ => throw new NotImplementedException()
    //    };

    //    var lancamentos = await _contasRepository.ConsultaLancamentosEmContas(competencia, tipoRegistroId);

    //    //var financasComunsPorConta = financasPorConta.Where(x => !x.EhPrevisaoInteligente);

    //    //var financasAPrazoList = new List<FinancaPorContaModel>(financasComunsPorConta.Select(x => new FinancaPorContaModel
    //    //{
    //    //    Id = x.Id,
    //    //    CategoriaNome = x.Categoria?.Nome,
    //    //    Data = x.Data,
    //    //    Descricao = x.Descricao,
    //    //    Valor = x.Valor,
    //    //    ValorOriginal = null,
    //    //    EhPrevisaoInteligente = x.EhPrevisaoInteligente,
    //    //    TipoId = x.TipoId,
    //    //    Nivel = x.Nivel,
    //    //    FinancaPaiId = x.FinancaPaiId
    //    //}));

    //    //var financasInteligentesPorConta = financasPorConta.Where(x => x.EhPrevisaoInteligente);

    //    //var grupoFinancasComunsPorConta = financasComunsPorConta
    //    //    .GroupBy(x => new { x.CategoriaId });

    //    //financasAPrazoList.AddRange(financasInteligentesPorConta
    //    //    .Join(grupoFinancasComunsPorConta,
    //    //        x => x.CategoriaId,
    //    //        g => g.Key.CategoriaId,
    //    //        (x, g) => new FinancaPorContaModel
    //    //        {
    //    //            Id = x.Id,
    //    //            CategoriaNome = x.Categoria?.Nome,
    //    //            Data = x.Data.AddMonths(1).AddDays(-1),
    //    //            Descricao = x.Descricao,
    //    //            Valor = x.Valor - g.Sum(x => x.Valor),
    //    //            ValorOriginal = x.Valor,
    //    //            EhPrevisaoInteligente = x.EhPrevisaoInteligente,
    //    //            TipoId = x.TipoId,
    //    //            Nivel = x.Nivel,
    //    //            FinancaPaiId = x.FinancaPaiId
    //    //        }));

    //    //var valorApurado = financasComunsPorConta.Where(x => x.Categoria != null).Sum(x => x.Valor);

    //    //valorApurado += financasAPrazoList.Where(x => x.CategoriaNome != null && x.EstaDentroPrevisto == true).Sum(x => x.Valor) ?? 0;




    //    //valorApurado += financasPorConta.Where(x => x.CategoriaId != null && x.EstaDentroPrevisto == true).Sum(x => x.Valor);




    //    //var financasTratadas = await _financasInteligentesProcuder.DerivaFinancasDe(financas);


    //    var categorias = await _categoriasRepository.ObtemCategorias();

    //    //

    //    var creditos = ApurarContasPorCompetencia(lancamentos, categorias, FormaRegistroEnum.Credito);

    //    var debitos = ApurarContasPorCompetencia(lancamentos, categorias, FormaRegistroEnum.Debito_);

    //    //

    //    var valorCreditos = creditos.Sum(x => x.Valor);

    //    var valorSaldo = valorCreditos;

    //    var valorDebitos = debitos.Sum(x => x.Valor);

    //    var valorSaldoComDebitos = valorSaldo + valorDebitos;

    //    //var valorAcumuladoAPrazoTotal = await _db.Lancamentos
    //    //    .Where(x => x.ContaId != null)
    //    //    //.Where(x => x.Conta.Data.Year <= ano)
    //    //    //.Where(x => x.Conta.Data.Month < mes)
    //    //    .SumAsync(x => x.Valor);

    //    //var valorAcumuladoAVista = await _db.Lancamentos
    //    //    .Where(x => x.ContaId == null)
    //    //    .Where(x => x.Data.Year <= apuracaoContas.Competencia.Year)
    //    //    .Where(x => x.Data.Month < apuracaoContas.Competencia.Month)
    //    //    .SumAsync(x => x.Valor);

    //    //var valorAcumuladoTotal = valorAcumuladoAPrazoTotal + valorAcumuladoAVista + valorSaldoTotal;

    //    var valorAcumuladoAnterior = await _contasRepository.ObtemValorAcumuladoEmContas(competencia, tipoRegistroId);

    //    var valorAcumulado = valorAcumuladoAnterior + valorSaldo;

    //    //

    //    var apuracaoContas = new ApuracaoContas
    //    {
    //        Competencia = competencia,
    //        Creditos = creditos,
    //        Debitos = debitos,
    //        ValorAcumuladoAnterior = valorAcumuladoAnterior,
    //        ValorAcumulado = valorAcumulado,
    //        ValorSaldo = valorSaldo,
    //        ValorDebitos = valorDebitos,
    //        ValorSaldoComDebitos = valorSaldoComDebitos,
    //        ValorCreditos = valorCreditos,
    //    };

    //    //var apuracaoContas = new ApuracaoContas
    //    //{
    //    //    Competencia = competencia,
    //    //    Valor = apuracaoContaList.Sum(x => x.Valor),
    //    //    Contas = apuracaoContaList
    //    //};

    //    return apuracaoContas;
    //}

    //private static ICollection<ApuracaoConta> ApurarContasPorCompetencia(
    //    IEnumerable<Financa> lancamentos,
    //    IEnumerable<Categoria> categorias,
    //    FormaRegistroEnum tipoContaId)
    //{
    //    var apuracaoContaList = new List<ApuracaoConta>();

    //    foreach (var categoria in categorias)
    //    {
    //        var lancamentosPorConta = lancamentos
    //        .Where(x => x.FormaRegistroId == tipoContaId)
    //        .Where(x => x.CategoriaId == categoria.Id);

    //        var valorApurado = lancamentosPorConta.Sum(x => x.Valor); //.Where(x => x.Categoria != null)

    //        //valorApurado += financasPorConta.Where(x => x.CategoriaId != null && x.EstaDentroPrevisto == true).Sum(x => x.Valor);

    //        if (valorApurado != 0)
    //        {
    //            var apuracaoConta = new ApuracaoConta
    //            {
    //                Categoria = categoria,
    //                CategoriaId = categoria.Id,
    //                TipoContaId = tipoContaId,
    //                Valor = valorApurado,
    //                Lancamentos = lancamentosPorConta.ToList()
    //            };

    //            apuracaoContaList.Add(apuracaoConta);
    //        }
    //    }

    //    return apuracaoContaList;
    //}

    public async Task<ICollection<Financa>> ConsultaLancamentosADebito(DateOnly competencia)
    {
        IEnumerable<Financa> lancamentos = await _contasRepository.ConsultaLancamentosEmCaixa(competencia);

        //var financasTratadas = await _financasInteligentesProcuder.DerivaFinancasDe(financas);

        return lancamentos.ToList();
    }
}
