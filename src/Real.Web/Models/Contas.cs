using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Real.Models;

public class ApuracaoFinancasPorContaModel
{
    public int Id { get; set; }

    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateOnly Competencia { get; set; }

    [DisplayName("Status")]
    public StatusApuracaoEnum? StatusId { get; set; }

    [DisplayName("Créditos")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorCreditosTotal { get; set; }

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
    public required string CategoriaId { get; set; }

    public required string CategoriaNome { get; set; }

    public FormaRegistroEnum? TipoContaId { get; set; }

    public int? Ordem { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal Valor { get; set; }

    //public string? Pessoa { get; set; }

    public ICollection<LancamentoPorContaModel> Lancamentos { get; set; } = new HashSet<LancamentoPorContaModel>();
}

public class LancamentoPorContaModel
{
    public Guid LancamentoId { get; set; }

    public TipoFinancaEnum? TipoFinancaId { get; set; }

    public string? CategoriaNome { get; set; }

    public string? CategoriaBiIcon { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
    public DateOnly Competencia { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MMM}")]
    public DateTime Data { get; set; }

    public required string Descricao { get; set; }

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
