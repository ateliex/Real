using Real.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Repositories;

public interface ContasRepositoryInterface
{
    Task<ICollection<Conta>> ObtemContas();

    Task<Conta> ObtemConta(string nome);

    Task<Conta> ObtemConta(Guid id);

    Task<decimal> ObtemApuracao(Conta conta, DateOnly competencia);

    Task<Apuracao> ObtemApuracaoOrDefault(Conta conta, DateOnly competencia);

    Task<ICollection<Financa>> ConsultaFinancasPorCompetencia(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<ICollection<Financa>> ConsultaFinancasPorCompetenciaAnual(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<decimal> ObtemValorTotalPorCompetencia(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<decimal> ObtemValorTotalPorCompetenciaAnual(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<decimal> ObtemValorAcumuladoPorCompetencia(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<decimal> ObtemValorAcumuladoPorCompetenciaAnual(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<ICollection<Lancamento>> ConsultaLancamentosEmContas(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<decimal> ObtemValorTotalEmContas(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<decimal> ObtemValorAcumuladoEmContas(DateOnly competencia, TipoRegistroEnum tipoRegistroId);

    Task<ICollection<Lancamento>> ConsultaLancamentosEmCaixa(DateOnly competencia);

    Task<decimal> ObtemValorTotalEmCaixa(DateOnly competencia);

    Task<decimal> ObtemValorAcumuladoEmCaixa(DateOnly competencia);

    Task<Financa> ObtemFinanca(Guid id);

    Task Adiciona(Financa financa);

    Task Atualiza(Financa financa);

    Task Adiciona(Conta conta);

    Task Adiciona(Apuracao apuracao);

    Task Adiciona(Lancamento lancamento);

    Task Atualiza(Conta conta);
}
