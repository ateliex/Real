using Real.Models;

namespace Real.Mappers;

public static class ApuracaoMapper
{
    public static ApuracaoCategoriasDataModel ToModel(this ApuracaoCategorias apuracaoCategorias)
    {
        return new ApuracaoCategoriasDataModel
        {
            Competencia = apuracaoCategorias.Competencia,
            ValorReceitas = apuracaoCategorias.ValorReceitas,
            ValorDespesas = apuracaoCategorias.ValorDespesas,
            ValorSaldo = apuracaoCategorias.ValorSaldo,
            ValorAcumuladoAnterior = apuracaoCategorias.ValorAcumuladoAnterior,
            ValorAcumulado = apuracaoCategorias.ValorAcumulado,
            Receitas = apuracaoCategorias.Receitas.Select(x => x.ToModel()).ToList(),
            Despesas = apuracaoCategorias.Despesas.Select(x => x.ToModel()).ToList()
        };
    }

    public static ApuracaoCategoriaDataModel ToModel(this ApuracaoCategoria apuracaoCategoria)
    {
        return new ApuracaoCategoriaDataModel
        {
            Competencia = apuracaoCategoria.Competencia,
            CategoriaId = apuracaoCategoria.CategoriaId,
            Categoria = apuracaoCategoria.Categoria.ToModel(),
            Valor = apuracaoCategoria.Valor,
            Financas = apuracaoCategoria.Financas.Select(f => f.ToModel()).ToList()
        };
    }

    public static ICollection<ApuracaoDataModel> ToModel(this ICollection<Apuracao> apuracoes)
    {
        return apuracoes.Select(x => x.ToModel())
            .ToList();
    }

    public static ApuracaoDataModel ToModel(this Apuracao apuracao)
    {
        return new ApuracaoDataModel
        {
            Competencia = apuracao.Competencia,
            StatusId = apuracao.StatusId,
            ValorPorCompetencia = apuracao.ValorPorCompetencia,
            ValorPorData = apuracao.ValorPorData,
            Observacao = apuracao.Observacao
        };
    }
}
