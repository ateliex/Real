namespace Real.Models;

public class PrevisaoInteligente : Financa
{
    public decimal ValorPrevisto { get; set; }

    public decimal? ValorOriginal { get; set; }

    public decimal? ValorExcedente { get; set; }

    public bool EstaDentroPrevisto
    {
        get
        {
            if (TipoFinancaId == TipoFinancaEnum.Receita)
            {
                if (Math.Abs(Valor) > Math.Abs(ValorPrevisto))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Math.Abs(Valor) < Math.Abs(ValorPrevisto))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public bool EstaForaPrevisto { get => !EstaDentroPrevisto; }

    public PrevisaoInteligente()
        : base(TipoRegistroEnum.Misto)
    {

    }
}

public class FinancasInteligentesProcuder
{
    public async Task<ICollection<Financa>> Procude(IEnumerable<Financa> financas)
    {
        var financasComuns = financas.Where(x => x.EhPrevisao == false);

        var list = new List<Financa>(financasComuns);

        var previsoesIndeterminadas = financas.Where(x => x.EhPrevisao == true); //.Cast<PrevisaoIndeterminada>();

        var financasComunsPorCategoriaId = financasComuns.GroupBy(x => x.CategoriaId);

        list.AddRange(previsoesIndeterminadas
            .Join(financasComunsPorCategoriaId,
                previsao => previsao.CategoriaId,
                g => g.Key,
                (previsao, g) => new PrevisaoInteligente
                {
                    Id = previsao.Id,
                    TipoLancamentoId = previsao.TipoLancamentoId,
                    TipoRegistroId = previsao.TipoRegistroId,
                    TipoFinancaId = previsao.TipoFinancaId,
                    Competencia = previsao.Competencia,
                    Data = CalculaDataCorte(previsao.Data),
                    Descricao = previsao.Descricao,
                    Valor = CalculaValorPrevistoInteligente(previsao, valorPrevistoIndeterminado: previsao.Valor, valorApurado: g.Sum(y => y.Valor), out decimal? valorExcedente),
                    ValorOriginal = previsao.Valor,
                    ValorPrevisto = previsao.Valor,
                    ValorExcedente = valorExcedente,
                    Categoria = previsao.Categoria,
                    CategoriaId = previsao.CategoriaId,
                    Conta = previsao.Conta,
                    ContaId = previsao.ContaId,
                    EhPrevisao = true,
                    Nivel = previsao.Nivel,
                    Grupo = previsao.Grupo,
                    GrupoId = previsao.GrupoId,
                    Ordem = previsao.Ordem
                }));

        var categoriasId = financasComunsPorCategoriaId.Select(x => x.Key);

        list.AddRange(previsoesIndeterminadas
            .Where(previsao => !categoriasId.Contains(previsao.CategoriaId))
            .Select(previsao => new PrevisaoInteligente
            {
                Id = previsao.Id,
                TipoLancamentoId = previsao.TipoLancamentoId,
                TipoRegistroId = previsao.TipoRegistroId,
                TipoFinancaId = previsao.TipoFinancaId,
                Competencia = previsao.Competencia,
                Data = CalculaDataCorte(previsao.Data),
                Descricao = previsao.Descricao,
                Valor = CalculaValorPrevistoInteligente(previsao, valorPrevistoIndeterminado: previsao.Valor, valorApurado: 0, out decimal? valorExcedente),
                ValorOriginal = previsao.Valor,
                ValorPrevisto = previsao.Valor,
                ValorExcedente = valorExcedente,
                Categoria = previsao.Categoria,
                CategoriaId = previsao.CategoriaId,
                Conta = previsao.Conta,
                ContaId = previsao.ContaId,
                EhPrevisao = true,
                Nivel = previsao.Nivel,
                Grupo = previsao.Grupo,
                GrupoId = previsao.GrupoId,
                Ordem = previsao.Ordem
            }));

        //var valorApurado = financasComunsPorConta.Where(x => x.Categoria != null).Sum(x => x.Valor);

        //valorApurado += financasAPrazoList.Where(x => x.CategoriaNome != null && x.EstaDentroPrevisto == true).Sum(x => x.Valor) ?? 0;

        return await Task.FromResult(list);
    }

    private DateTime CalculaDataCorte(DateTime data)
    {
        var dataNoMesQueVira = data.AddMonths(1);

        var dataCorte = dataNoMesQueVira.AddDays(-dataNoMesQueVira.Day);

        return dataCorte;
    }

    private decimal CalculaValorPrevistoInteligente(
        Financa previsaoIndeterminada,
        decimal valorPrevistoIndeterminado,
        decimal valorApurado,
        out decimal? valorExcedente)
    {
        var valorPrevistoIndeterminadoAbsoluto = Math.Abs(valorPrevistoIndeterminado);
        var valorApuradoAbsoluto = Math.Abs(valorApurado);

        decimal valorPrevistoInteligente;

        var dataCorte = CalculaDataCorte(previsaoIndeterminada.Data);

        if (dataCorte >= DateTime.Today)
        {
            if (valorApuradoAbsoluto > valorPrevistoIndeterminadoAbsoluto)
            {
                valorExcedente = valorApuradoAbsoluto - valorPrevistoIndeterminadoAbsoluto;

                valorPrevistoInteligente = 0;
            }
            else
            {
                valorExcedente = null;

                if (previsaoIndeterminada.TipoFinancaId == TipoFinancaEnum.Receita)
                {
                    valorPrevistoInteligente = valorPrevistoIndeterminadoAbsoluto - valorApuradoAbsoluto;
                }
                else
                {
                    valorPrevistoInteligente = -(valorPrevistoIndeterminadoAbsoluto - valorApuradoAbsoluto);
                }
            }
        }
        else
        {
            if (valorApuradoAbsoluto > valorPrevistoIndeterminadoAbsoluto)
            {
                valorExcedente = valorApuradoAbsoluto - valorPrevistoIndeterminadoAbsoluto;

                valorPrevistoInteligente = 0;
            }
            else
            {
                if (previsaoIndeterminada.TipoFinancaId == TipoFinancaEnum.Receita)
                {
                    valorExcedente = valorPrevistoIndeterminadoAbsoluto - valorApuradoAbsoluto;
                }
                else
                {
                    valorExcedente = -(valorPrevistoIndeterminadoAbsoluto - valorApuradoAbsoluto);
                }

                valorPrevistoInteligente = 0;
            }
        }

        return valorPrevistoInteligente;
    }
}
