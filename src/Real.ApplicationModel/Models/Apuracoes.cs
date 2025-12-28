namespace Real.Models;

public enum RegimeApuracaoEnum
{
    Competencia = 1,
    Caixa = 2
}

public enum StatusApuracaoEnum
{
    Aberta,
    Fechada
}

public interface ConsultaApuracoesInterface
{
    Task<ICollection<ApuracaoDataModel>> ConsultaApuracoes();
    
    Task<ApuracaoDataModel> ConsultaApuracao(DateOnly competencia);
}

public class ApuracaoDataModel
{
    public DateOnly Competencia { get; set; }

    public StatusApuracaoEnum? StatusId { get; set; }

    public decimal? ValorPorCompetencia { get; set; }

    public decimal? ValorPorData { get; set; }

    public string? Observacao { get; set; }
}

public interface ApuracaoInterface
{
    Task<ApuracaoCategoriasDataModel> ApurarCategoriasPorCompetencia(DateOnly competencia, RegimeApuracaoEnum regimeApuracaoId);
}

public class ApuracaoCategoriasDataModel
{
    public DateOnly Competencia { get; set; }

    public decimal ValorReceitas { get; set; }

    public decimal ValorDespesas { get; set; }

    public decimal ValorSaldo { get; set; }

    public decimal ValorAcumuladoAnterior { get; set; }

    public decimal ValorAcumulado { get; set; }

    public required ICollection<ApuracaoCategoriaDataModel> Receitas { get; set; }

    public required ICollection<ApuracaoCategoriaDataModel> Despesas { get; set; }
}

public class ApuracaoCategoriaDataModel
{
    public DateOnly Competencia { get; set; }

    public required CategoriaDataModel Categoria { get; set; }

    public required string CategoriaId { get; set; }

    public decimal Valor { get; set; }

    public ICollection<FinancaDataModel> Financas { get; set; }
}

public interface ExclusaoApuracaoInterface
{
    Task ExcluiApuracao(DateOnly competencia);
}