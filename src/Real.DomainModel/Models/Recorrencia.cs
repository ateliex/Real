using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class Recorrencia
{
    public Guid Id { get; set; }

    public TipoLancamentoEnum TipoLancamentoId { get; set; }

    //public virtual Conta Conta { get; set; }

    //public Guid? ContaId { get; set; }

    /// <summary>
    /// Competência Inicial.
    /// </summary>
    public DateOnly Competencia { get; set; }

    /// <summary>
    /// Data Inicial.
    /// </summary>
    public DateTime Data { get; set; }

    public required string Descricao { get; set; }

    /// <summary>
    /// Valor previsto ou realizado do lançamento.
    /// </summary>
    public virtual decimal Valor { get; set; }

    public int Nivel { get; set; }

    //public Lancamento? LancamentoPai { get; set; }

    //public Guid? LancamentoPaiId { get; set; }

    public int? Ordem { get; set; }

    //

    public FormaRegistroEnum FormaRegistroId { get; set; }

    //

    public TipoFinancaEnum TipoFinancaId { get; set; }

    public Categoria Categoria { get; set; }

    public string? CategoriaId { get; set; }

    //public bool EhPrevisao { get; set; }

    //

    public TipoRecorrenciaEnum TipoRecorrenciaId { get; set; }

    public int Quantidade { get; set; }

    public virtual ICollection<Financa> Repeticoes { get; set; }
}

//public class FinancaRecorr : Financa
//{
//    public Recorrencia__ Recorrencia { get; set; }

//    public Guid? RecorrenciaId { get; set; }

//    public FinancaRecorr()
//        : base(TipoRegistroEnum.Misto)

//    {

//    }
//}
