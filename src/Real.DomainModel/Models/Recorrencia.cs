using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class Recorrencia : Entity
{
    public Guid Id { get; set; }

    public TipoLancamentoEnum TipoLancamentoId { get; set; }

    public virtual Conta Conta { get; set; }

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

    //

    public TipoFinancaEnum TipoFinancaId { get; set; }

    public Categoria Categoria { get; set; }

    public string? CategoriaId { get; set; }

    //

    public TipoRecorrenciaEnum TipoRecorrenciaId { get; set; }

    public int Quantidade { get; set; }

    public virtual ICollection<FinancaAVista> Repeticoes { get; set; }
}

//public class Repeticao : Financa
//{
//    public Recorrencia? Recorrencia { get; set; }

//    public Guid? RecorrenciaId { get; set; }
//}

//public class FinancaRecorr : Financa
//{
//    public Recorrencia__ Recorrencia { get; set; }

//    public Guid? RecorrenciaId { get; set; }

//    public FinancaRecorr()
//        : base(TipoRegistroEnum.Misto)

//    {

//    }
//}
