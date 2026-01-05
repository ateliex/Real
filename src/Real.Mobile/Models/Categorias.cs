using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Real.Models;

public class ApuracaoFinancasPorCategoriaModel : ObservableObject
{
    public int Id { get; set; }

    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateOnly? Competencia { get; set; }

    [DisplayName("Status")]
    public StatusApuracaoEnum? StatusId { get; set; }

    private decimal _valorReceitasTotal;
    public decimal ValorReceitasTotal
    {
        get { return _valorReceitasTotal; }
        set
        {
            _valorReceitasTotal = value;
            OnPropertyChanged();
        }
    }

    private decimal _valorDespesasTotal;
    public decimal ValorDespesasTotal
    {
        get { return _valorDespesasTotal; }
        set
        {
            _valorDespesasTotal = value;
            OnPropertyChanged();
        }
    }

    private decimal _valorSaldoTotal;
    public decimal ValorSaldoTotal
    {
        get { return _valorSaldoTotal; }
        set
        {
            _valorSaldoTotal = value;

            if (_valorSaldoTotal < 0)
            {
                ValorSaldoTotalNegativo = true;
            }
            else
            {
                ValorSaldoTotalNegativo = false;
            }

            OnPropertyChanged();
        }
    }

    private bool _valorSaldoTotalNegativo;
    public bool ValorSaldoTotalNegativo
    {
        get { return _valorSaldoTotalNegativo; }
        set
        {
            _valorSaldoTotalNegativo = value;
            OnPropertyChanged();
        }
    }

    private decimal _valorAcumuladoAnterior;
    public decimal ValorAcumuladoAnterior
    {
        get { return _valorAcumuladoAnterior; }
        set
        {
            _valorAcumuladoAnterior = value;

            if (_valorAcumuladoAnterior < 0)
            {
                ValorAcumuladoAnteriorNegativo = true;
            }
            else
            {
                ValorAcumuladoAnteriorNegativo = false;
            }

            OnPropertyChanged();
        }
    }

    private bool _valorAcumuladoAnteriorNegativo;
    public bool ValorAcumuladoAnteriorNegativo
    {
        get { return _valorAcumuladoAnteriorNegativo; }
        set
        {
            _valorAcumuladoAnteriorNegativo = value;
            OnPropertyChanged();
        }
    }

    private decimal _valorAcumuladoTotal;
    public decimal ValorAcumuladoTotal
    {
        get { return _valorAcumuladoTotal; }
        set
        {
            _valorAcumuladoTotal = value;

            if (_valorAcumuladoTotal < 0)
            {
                ValorAcumuladoTotalNegativo = true;
            }
            else
            {
                ValorAcumuladoTotalNegativo = false;
            }

            OnPropertyChanged();
        }
    }

    private bool _valorAcumuladoTotalNegativo;
    public bool ValorAcumuladoTotalNegativo
    {
        get { return _valorAcumuladoTotalNegativo; }
        set
        {
            _valorAcumuladoTotalNegativo = value;
            OnPropertyChanged();
        }
    }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }
}

public class ApuracaoCategoriasModel : List<ApuracaoCategoriaModel>
{
    public string Nome { get; set; }

    public decimal Valor { get; set; }

    public ApuracaoCategoriasModel(string nome, decimal valor, List<ApuracaoCategoriaModel> collection) : base(collection)
    {
        Nome = nome;

        Valor = valor;
    }
}

public class ApuracaoCategoriaModel : ObservableObject
{
    public string GroupName { get; set; }

    private string _nome;

    [Required(ErrorMessage = "Teste: Nome Obrigatório")]
    public string Nome
    {
        get { return _nome; }
        set
        {
            _nome = value;

            OnPropertyChanged();
        }
    }

    private decimal _valorPrevistoAno;

    public decimal ValorPrevistoAnual
    {
        get { return _valorPrevistoAno; }
        set
        {
            _valorPrevistoAno = value;

            OnPropertyChanged();
        }
    }

    private decimal _valorPrevistoMes;

    public decimal ValorPrevistoMensal
    {
        get { return _valorPrevistoMes; }
        set
        {
            _valorPrevistoMes = value;

            OnPropertyChanged();
        }
    }

    public required string CategoriaId { get; set; }

    public required string CategoriaNome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int Ordem { get; set; }

    public required string IconFaUnicode { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal Valor { get; set; }

    public ObservableCollection<FinancaPorCategoriaModel> Financas { get; set; } = new ObservableCollection<FinancaPorCategoriaModel>();
}

public class FinancaPorCategoriaModel : ObservableObject
{
    public Guid Id { get; set; }

    private string _nome;

    [Required(ErrorMessage = "Teste: Nome Obrigatório")]
    public string Nome
    {
        get { return _nome; }
        set
        {
            _nome = value;

            OnPropertyChanged();
        }
    }

    //public decimal Valor { get; set; }

    public TipoFinancaEnum TipoId { get; set; }





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
}
