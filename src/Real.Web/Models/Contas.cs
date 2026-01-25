using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Real.Models;

public class LancamentoInputModel
{
    public Guid Id { get; set; }

    [DisplayName("Tipo Lançamento")]
    public TipoLancamentoEnum TipoLancamentoId { get; set; }

    [DisplayName("Tipo Registro")]
    public TipoRegistroEnum TipoRegistroId { get; set; }

    [DisplayName("Conta")]
    public Guid ContaId { get; set; }

    [DisplayName("Tipo Competência")]
    public TipoCompetenciaEnum TipoCompetenciaId { get; set; }

    [DisplayName("Data")]
    public virtual DateTime Data { get; set; }

    [DisplayName("Descrição")]
    public required string Descricao { get; set; }

    [DisplayName("Transação Id")]
    public string? TransacaoId { get; set; }

    [DisplayName("Transação")]
    public string? Transacao { get; set; }

    [DisplayName("Valor")]
    public virtual decimal Valor { get; set; }

    //

    [DisplayName("Tipo Finança")]
    public TipoFinancaEnum TipoFinancaId { get; set; }

    [DisplayName("Categoria")]
    public string CategoriaId { get; set; }

    [DisplayName("É Previsão?")]
    public bool EhPrevisao { get; set; }

    [DisplayName("É Recorrente?")]
    public bool EhRecorrente { get; set; }

    //

    [DisplayName("Recorrência")]
    public Guid? RecorrenciaId { get; set; }

    //

    public DateTime? CreationDate { get; set; }

    [Timestamp]
    public byte[]? Version { get; set; }
}

public class ApuracaoFinancasPorContaModel
{
    public int Id { get; set; }

    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateOnly Competencia { get; set; }

    [DisplayName("Status")]
    public StatusApuracaoEnum? StatusId { get; set; }

    [DisplayName("Créditos a Receber")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorCreditosAReceberTotal { get; set; }

    [DisplayName("Créditos a Pagar")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorCreditosAPagarTotal { get; set; }

    [DisplayName("Saldo")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSaldoTotal { get; set; }

    [DisplayName("Débitos")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDebitosTotal { get; set; }

    [DisplayName("Saldo + Débitos")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSaldoComDebitosTotal { get; set; }

    [DisplayName("Acumulado")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoAnterior { get; set; }

    [DisplayName("Total")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoTotal { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }
}

public class ContaApuradaModel
{
    public required Guid ContaId { get; set; }

    public required string ContaNome { get; set; }

    public TipoContaEnum? TipoContaId { get; set; }

    public int? Ordem { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal Valor { get; set; }

    public string? Pessoa { get; set; }

    public ICollection<LancamentoPorContaModel> Lancamentos { get; set; } = new HashSet<LancamentoPorContaModel>();
}

public class LancamentoPorContaModel
{
    public Guid LancamentoId { get; set; }

    public TipoRegistroEnum TipoRegistroId { get; set; }

    public TipoFinancaEnum? TipoFinancaId { get; set; }

    public string? CategoriaNome { get; set; }

    public string? CategoriaBiIcon { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
    public DateOnly Competencia { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MMM}")]
    public DateTime Data { get; set; }

    public required string Descricao { get; set; }
    
    public string? Transacao { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal Valor { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal? ValorOriginal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal? ValorExcedente { get; set; }

    public bool EhPrevisao { get; set; }

    public bool? EstaDentroPrevisto { get; set; }

    public bool? EstaForaPrevisto { get; set; }

    public bool EhFinanca { get; set; }

    public bool EhGrupo { get; set; }

    public int Nivel { get; set; }

    public Guid? LancamentoPaiId { get; set; }

    public bool EhRecorrente { get; set; }
}
