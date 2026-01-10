using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class ApuracaoCategoriaModel : ObservableObject
{
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

    //public decimal ValorAno { get; set; }

    //public decimal? ValorJaneiro { get; set; }

    //public decimal? ValorFevereiro { get; set; }

    //public decimal? ValorMarco { get; set; }

    //public decimal? ValorAbril { get; set; }

    //public decimal? ValorMaio { get; set; }

    //public decimal? ValorJunho { get; set; }

    //public decimal? ValorJulho { get; set; }

    //public decimal? ValorAgosto { get; set; }

    //public decimal? ValorSetembro { get; set; }

    //public decimal? ValorOutubro { get; set; }

    //public decimal? ValorNovembro { get; set; }

    //public decimal? ValorDezembro { get; set; }

    public TipoFinancaEnum TipoId { get; set; }

    public ObservableCollection<FinancaPorCategoriaModel> Financas { get; set; } = new ObservableCollection<FinancaPorCategoriaModel>();



    public required string CategoriaId { get; set; }

    public required string CategoriaNome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int Ordem { get; set; }

    public required string IconId { get; set; }

    public required string IconFaUnicode { get; set; }

    //[DisplayFormat(DataFormatString = "{0:n}")]
    //public decimal ValorPrevistoAnual { get; set; }

    //[DisplayFormat(DataFormatString = "{0:n}")]
    //public decimal ValorPrevistoMensal { get; set; }

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

    //public ICollection<ApuracaoFinancaModel> Financas { get; set; } = new HashSet<ApuracaoFinancaModel>();

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





    public Guid? FinancaId { get; set; }

    public TipoFinancaEnum TipoFinancaId { get; set; }

    public TipoContaEnum ContaTipoId { get; set; }

    public required string ContaNome { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
    public DateOnly Competencia { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MMM}")]
    public DateTime? Data { get; set; }

    public string? Descricao { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal? Valor { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal? ValorOriginal { get; set; }

    [DisplayFormat(DataFormatString = "{0:n}")]
    public decimal? ValorExcedente { get; set; }

    public bool EhPrevisao { get; set; }
}
