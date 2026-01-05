using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Real.Models;

public class ApuracaoFinancasPorCategoriaModel
{
    public int Id { get; set; }

    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateOnly? Competencia { get; set; }

    [DisplayName("Status")]
    public StatusApuracaoEnum? StatusId { get; set; }

    [DisplayName("Receitas")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorReceitasTotal { get; set; }

    [DisplayName("Despesas")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDespesasTotal { get; set; }

    [DisplayName("Saldo")]
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSaldoTotal { get; set; }

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

public class CategoriaApuradaModel
{
    public required string CategoriaId { get; set; }

    public required string CategoriaNome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int Ordem { get; set; }

    public required string BiIcon { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal Valor { get; set; }

    public ICollection<FinancaPorCategoriaModel> Financas { get; set; } = new HashSet<FinancaPorCategoriaModel>();
}

public class ApuracaoAnualFinancasPorCategoriaModel
{
    public int Id { get; set; }

    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateOnly? Competencia { get; set; }

    [DisplayName("Status")]
    public StatusApuracaoEnum? StatusId { get; set; }


    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoJaneiro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoFevereiro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoMarco { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoAbril { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoMaio { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoJunho { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoJulho { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoAgosto { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoSetembro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoOutubro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoNovembro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAcumuladoDezembro { get; set; }


    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJaneiroSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorFevereiroSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMarcoSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAbrilSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMaioSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJunhoSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJulhoSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAgostoSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSetembroSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorOutubroSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorNovembroSaldoTotal { get; set; }
    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDezembroSaldoTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSaldoTotal { get; set; }


    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJaneiroReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorFevereiroReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMarcoReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAbrilReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMaioReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJunhoReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJulhoReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAgostoReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSetembroReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorOutubroReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorNovembroReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDezembroReceitasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorReceitasTotal { get; set; }


    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJaneiroDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorFevereiroDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMarcoDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAbrilDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMaioDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJunhoDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJulhoDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAgostoDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSetembroDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorOutubroDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorNovembroDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDezembroDespesasTotal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDespesasTotal { get; set; }


    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorTotal { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }
}

public class CategoriaApuradaPorAnoModel
{
    public required string CategoriaId { get; set; }

    public required string CategoriaNome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int Ordem { get; set; }

    public required string IconId { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorPrevistoAnual { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorPrevistoMensal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAno { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJaneiro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorFevereiro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMarco { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAbril { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorMaio { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJunho { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorJulho { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorAgosto { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorSetembro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorOutubro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorNovembro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorDezembro { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal ValorTotal { get; set; }

    public ICollection<FinancaPorCategoriaModel> Financas { get; set; } = new HashSet<FinancaPorCategoriaModel>();
}

public class FinancaPorCategoriaModel
{
    public Guid FinancaId { get; set; }

    public TipoFinancaEnum TipoFinancaId { get; set; }

    //public FormaRegistroEnum ContaTipoId { get; set; }

    //public required string ContaNome { get; set; }

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

    public bool EhRecorrente { get; set; }
}
