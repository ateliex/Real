using Real.Mappers;
using Real.Models;
using Real.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Services;

public class ApuracoesAppService : 
    ApuracaoInterface,
    ExclusaoApuracaoInterface
{
    private readonly ApuracoesRepositoryInterface _apuracoesRepository;
    private readonly ApuracaoService _apuracaoService;

    public ApuracoesAppService(
        ApuracoesRepositoryInterface apuracoesRepository,
        ApuracaoService apuracaoService)
    {
        _apuracoesRepository = apuracoesRepository;
        _apuracaoService = apuracaoService;
    }

    public async Task<ApuracaoCategoriasDataModel> ApurarCategoriasPorCompetencia(DateOnly competencia, RegimeApuracaoEnum regimeApuracaoId)
    {
        var apuracaoCategorias = await _apuracaoService.ApurarCategoriasPorCompetencia(competencia, regimeApuracaoId);

        return apuracaoCategorias.ToModel();
    }

    public async Task ExcluiApuracao(DateOnly competencia)
    {
        var apuracao = await _apuracoesRepository.ObtemApuracao(competencia);

        await _apuracoesRepository.Exclui(apuracao);
    }
}
