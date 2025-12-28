using Microsoft.EntityFrameworkCore;
using Real.Mappers;
using Real.Models;
using Real.Repositories;
using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Data.Services;

public class ApuracoesSqlServerService : ApuracoesRepositoryInterface,
    ConsultaApuracoesInterface
{
    private readonly RealDbContext _db;

    public ApuracoesSqlServerService(
        RealDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<ApuracaoDataModel>> ConsultaApuracoes()
    {
        var apuracoes = await _db.Apuracoes
            .AsNoTracking()
            .Select(x => x.ToModel())
            .ToListAsync();

        return apuracoes;
    }

    public async Task<ApuracaoDataModel> ConsultaApuracao(DateOnly competencia)
    {
        var apuracao = await _db.Apuracoes
            .FirstOrDefaultAsync(x => x.Competencia == competencia);

        if (apuracao == default)
        {
            throw new EntityNotFoundException<Apuracao>($"Competência '{competencia}'");
        }

        var model = apuracao.ToModel();

        return model;
    }

    public async Task<Apuracao> ObtemApuracao(DateOnly competencia)
    {
        var apuracao = await _db.Apuracoes
            .FirstOrDefaultAsync(x => x.Competencia == competencia);

        if (apuracao == default)
        {
            throw new EntityNotFoundException<Apuracao>($"Competência '{competencia}'");
        }

        return apuracao;
    }

    public async Task<Apuracao?> ObtemApuracaoOrDefault(DateOnly competencia)
    {
        var apuracao = await _db.Apuracoes
            .FirstOrDefaultAsync(x => x.Competencia == competencia);

        return apuracao;
    }

    public async Task Adiciona(Apuracao apuracao)
    {
        _db.Apuracoes.Add(apuracao);

        await _db.SaveChangesAsync();
    }

    public async Task Atualiza(Apuracao apuracao)
    {
        await _db.SaveChangesAsync();
    }

    public async Task Exclui(Apuracao apuracao)
    {
        _db.Apuracoes.Remove(apuracao);

        await _db.SaveChangesAsync();
    }
}
