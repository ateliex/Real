using Microsoft.EntityFrameworkCore;
using Real.Models;
using Real.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Real.Data.Services;

public class ContasSqliteService : ContasRepositoryInterface
{
    private readonly RealDbContext _db;

    public ContasSqliteService(
        RealDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Conta>> ObtemContas()
    {
        var contas = await _db.Contas
            .ToListAsync();

        return contas;
    }

    public Task<Conta> ObtemConta(string nome)
    {
        throw new NotImplementedException();
    }

    public Task<Conta> ObtemConta(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> ObtemApuracao(Conta conta, DateOnly competencia)
    {
        throw new NotImplementedException();
    }

    public Task<Apuracao> ObtemApuracaoOrDefault(Conta conta, DateOnly competencia)
    {
        throw new NotImplementedException();
    }

    public Task<Financa> ObtemFinanca(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Financa>> ConsultaFinancasPorCompetencia(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Financas
            .Include(x => x.Categoria)
            .Include(x => x.Categoria.Icon)
            .Include(x => x.Conta)
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year == competencia.Year)
                .Where(x => x.Competencia.Month == competencia.Month);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year == competencia.Year)
                .Where(x => x.Data.Month == competencia.Month);
        }

        var financas = await query.ToListAsync();

        return financas;
    }

    public async Task<ICollection<Financa>> ConsultaFinancasPorCompetenciaAnual(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Financas
            .Include(x => x.Categoria)
            .Include(x => x.Categoria.Icon)
            .Include(x => x.Conta)
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year == competencia.Year);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year == competencia.Year);
        }

        var financas = await query.ToListAsync();

        return financas;
    }

    public async Task<decimal> ObtemValorTotalPorCompetencia(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Financas
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year == competencia.Year)
                .Where(x => x.Competencia.Month == competencia.Month);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year == competencia.Year)
                .Where(x => x.Data.Month == competencia.Month);
        }

        var valorTotal = await query.SumAsync(x => x.Valor);

        return valorTotal;
    }

    public async Task<decimal> ObtemValorTotalPorCompetenciaAnual(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Financas
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year == competencia.Year);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year == competencia.Year);
        }

        var valorTotal = await query.SumAsync(x => x.Valor);

        return valorTotal;
    }

    public async Task<decimal> ObtemValorAcumuladoPorCompetencia(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Financas
            .Where(x => x.EhPrevisao == false)
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => false
                    || x.Competencia.Year < competencia.Year
                    || (true
                        && x.Competencia.Year == competencia.Year
                        && x.Competencia.Month < competencia.Month));
        }
        else
        {
            query = query
                .Where(x => false
                    || x.Data.Year < competencia.Year
                    || (true
                        && x.Data.Year == competencia.Year
                        && x.Data.Month < competencia.Month));
        }

        var valorAcumuladoComum = await query.SumAsync(x => x.Valor);

        var valorAcumuladoPrevisto = 0;

        //var valorAcumuladoPrevisto = await _db.Financas
        //    .Where(x => x.EhPrevisaoInteligente == true)
        //    .Where(x => false
        //        || x.Data.Year < competencia.Year
        //        || (true
        //            && x.Data.Year == competencia.Year
        //            && x.Data.Month < competencia.Month))
        //    .SumAsync(x => x.Valor);

        var valorAcumulado = valorAcumuladoComum + valorAcumuladoPrevisto;

        return valorAcumulado;
    }

    public async Task<decimal> ObtemValorAcumuladoPorCompetenciaAnual(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Financas
            .Where(x => x.EhPrevisao == false)
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year < competencia.Year);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year < competencia.Year);
        }

        var valorAcumuladoComum = await query.SumAsync(x => x.Valor);

        var valorAcumuladoPrevisto = 0;

        //var valorAcumuladoPrevisto = await _db.Financas
        //    .Where(x => x.EhPrevisaoInteligente == true)
        //    .Where(x => false
        //        || x.Data.Year < competencia.Year
        //        || (true
        //            && x.Data.Year == competencia.Year
        //            && x.Data.Month < competencia.Month))
        //    .SumAsync(x => x.Valor);

        var valorAcumulado = valorAcumuladoComum + valorAcumuladoPrevisto;

        return valorAcumulado;
    }

    public async Task<ICollection<Lancamento>> ConsultaLancamentosEmContas(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Lancamentos
            .Include(x => ((Financa)x).Categoria)
            .Include(x => ((Financa)x).Categoria.Icon)
            .Include(x => x.Conta)
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year == competencia.Year)
                .Where(x => x.Competencia.Month == competencia.Month);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year == competencia.Year)
                .Where(x => x.Data.Month == competencia.Month);
        }

        var lancamentos = await query.ToListAsync();

        return lancamentos;
    }

    public async Task<decimal> ObtemValorTotalEmContas(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Lancamentos
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => x.Competencia.Year == competencia.Year)
                .Where(x => x.Competencia.Month == competencia.Month);
        }
        else
        {
            query = query
                .Where(x => x.Data.Year == competencia.Year)
                .Where(x => x.Data.Month == competencia.Month);
        }

        var valorTotal = await query.SumAsync(x => x.Valor);

        return valorTotal;
    }

    public async Task<decimal> ObtemValorAcumuladoEmContas(DateOnly competencia, TipoRegistroEnum tipoRegistroId)
    {
        var query = _db.Lancamentos
            .Where(x => x.TipoRegistroId.HasFlag(tipoRegistroId));
            //.Where(x => false
            //    || x.Data.Year < competencia.Year
            //    || (true
            //        && x.Data.Year == competencia.Year
            //        && x.Data.Month < competencia.Month))
            //.SumAsync(x => x.Valor);

        if (tipoRegistroId.HasFlag(TipoRegistroEnum.DeCompetencia))
        {
            query = query
                .Where(x => false
                    || x.Competencia.Year < competencia.Year
                    || (true
                        && x.Competencia.Year == competencia.Year
                        && x.Competencia.Month < competencia.Month));
        }
        else
        {
            query = query
                .Where(x => false
                    || x.Data.Year < competencia.Year
                    || (true
                        && x.Data.Year == competencia.Year
                        && x.Data.Month < competencia.Month));
        }

        var valorAcumuladoComum = await query.SumAsync(x => x.Valor);

        var valorAcumuladoPrevisto = 0;

        //var valorAcumuladoPrevisto = await _db.Financas
        //    .Where(x => x.EhPrevisaoInteligente == true)
        //    .Where(x => false
        //        || x.Data.Year < competencia.Year
        //        || (true
        //            && x.Data.Year == competencia.Year
        //            && x.Data.Month < competencia.Month))
        //    .SumAsync(x => x.Valor);

        var valorAcumulado = valorAcumuladoComum + valorAcumuladoPrevisto;

        return valorAcumulado;
    }

    public async Task<ICollection<Lancamento>> ConsultaLancamentosEmCaixa(DateOnly competencia)
    {
        var lancamentos = await _db.Lancamentos
            .Include(x => ((Financa)x).Categoria)
            .Include(x => ((Financa)x).Categoria.Icon)
            .Include(x => x.Conta)
            .Where(x => x.Data.Year == competencia.Year)
            .Where(x => x.Data.Month == competencia.Month)
            .ToListAsync();

        return lancamentos;
    }

    public async Task<decimal> ObtemValorTotalEmCaixa(DateOnly competencia)
    {
        var valorTotal = await _db.Lancamentos
            .Where(x => x.Data.Year == competencia.Year)
            .Where(x => x.Data.Month == competencia.Month)
            .SumAsync(x => x.Valor);

        return valorTotal;
    }

    public async Task<decimal> ObtemValorAcumuladoEmCaixa(DateOnly competencia)
    {
        var valorAcumulado = await _db.Lancamentos
            .Where(x => false
                || x.Data.Year < competencia.Year
                || (true
                    && x.Data.Year == competencia.Year
                    && x.Data.Month < competencia.Month))
            .SumAsync(x => x.Valor);

        return valorAcumulado;
    }

    public Task Adiciona(Financa financa)
    {
        throw new NotImplementedException();
    }

    public Task Atualiza(Financa financa)
    {
        throw new NotImplementedException();
    }

    public Task Adiciona(Conta conta)
    {
        throw new NotImplementedException();
    }

    public Task Adiciona(Apuracao apuracao)
    {
        throw new NotImplementedException();
    }

    public Task Adiciona(Lancamento lancamento)
    {
        throw new NotImplementedException();
    }

    public Task Atualiza(Conta conta)
    {
        throw new NotImplementedException();
    }
}
