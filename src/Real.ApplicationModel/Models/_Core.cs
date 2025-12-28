namespace Real.Models;

public enum TipoContaEnum
{
    ContaDebito,
    ContaCreditoAPagar,
    ContaCreditoAReceber
}

public enum TipoLancamentoEnum
{
    Financa,
    Boleto,
    FinancaComum,
    PrevisaoIndeterminada,
    PrevisaoInteligente,
    PagamentoFatura
}

[Flags]
public enum TipoRegistroEnum
{
    DeCompetencia = 1,
    DeCaixa = 2,
    Misto = 3
}

public class ContaDataModel
{
    public Guid Id { get; set; }

    public required string Nome { get; set; }

    public TipoContaEnum TipoContaId { get; set; }

    public int? Ordem { get; set; }

    public bool Ativa { get; set; }

    public virtual ICollection<LancamentoDataModel> Lancamentos { get; set; }

    public string? Pessoa { get; set; }
}

public abstract class LancamentoDataModel
{
    public Guid Id { get; set; }

    public TipoLancamentoEnum TipoLancamentoId { get; set; }

    public TipoRegistroEnum TipoRegistroId { get; set; }

    public virtual ContaDataModel Conta { get; set; }

    public Guid? ContaId { get; set; }

    public virtual DateOnly Competencia { get; set; }

    /// <summary>
    /// Data que vai ocorrer ou ocorreu o lançamento.
    /// </summary>
    public virtual DateTime Data { get; set; }

    public required string Descricao { get; set; }

    /// <summary>
    /// Valor previsto ou realizado do lançamento.
    /// </summary>
    public virtual decimal Valor { get; set; }

    public int Nivel { get; set; }

    public LancamentoDataModel? LancamentoPai { get; set; }

    public Guid? LancamentoPaiId { get; set; }

    public int? Ordem { get; set; }

    //public Recorrencia__? Recorrencia { get; set; }

    public Guid? RecorrenciaId { get; set; }
}
