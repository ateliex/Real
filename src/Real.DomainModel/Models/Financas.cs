using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Real.Models;

public class Financa : Entity
{
    public Guid Id { get; set; }

    public TipoLancamentoEnum TipoLancamentoId { get; set; }

    public TipoRegistroEnum TipoRegistroId { get; set; }

    //public virtual Conta Conta { get; set; }

    //public Guid? ContaId { get; set; }

    public virtual DateOnly Competencia { get; set; }

    /// <summary>
    /// Data que vai ocorrer ou ocorreu o lançamento.
    /// </summary>
    public virtual DateTime Data { get; set; }

    public required string Descricao { get; set; }

    public string? TransacaoId { get; set; }

    public string? Transacao { get; set; }

    /// <summary>
    /// Valor previsto ou realizado do lançamento.
    /// </summary>
    public virtual decimal Valor { get; set; }

    public int? Ordem { get; set; }

    #region Grupamento

    //public bool EhGrupo { get; set; }

    //public Grupo? Grupo { get; set; }

    //public Guid? GrupoId { get; set; }

    //public int Nivel { get; set; }

    #endregion

    #region Parcelamento

    public bool EhParcelamento { get; set; }

    public int NumeroParcelas { get; set; }

    public virtual ICollection<Financa> Parcelas { get; set; } = new HashSet<Financa>();

    public bool EhParcela { get => NumeroParcela.HasValue; }

    public Financa? Parcelamento { get; set; }

    public Guid? ParcelamentoId { get; set; }

    public int? NumeroParcela { get; set; }

    #endregion

    #region Recorrência

    public bool EhRecorrente { get; set; }

    public Recorrencia? Recorrencia { get; set; }

    public Guid? RecorrenciaId { get; set; }

    #endregion

    //
    //public FormaRegistroEnum FormaRegistroId { get; set; }

    //

    public TipoFinancaEnum TipoFinancaId { get; set; }

    public Categoria Categoria { get; set; }

    public string? CategoriaId { get; set; }

    public bool EhPrevisao { get; set; }

    public Financa()
        : this(TipoRegistroEnum.Misto)
    {

    }

    public Financa(TipoRegistroEnum tipoRegistroId)
    {
        TipoLancamentoId = TipoLancamentoEnum.Financa;

        TipoRegistroId = tipoRegistroId;
    }
}

//public class FinancaAvulsa : Financa
//{
//    public FinancaAvulsa()
//        : base(TipoRegistroEnum.Misto)
//    {

//    }
//}

//public class GrupoFinancas : Lancamento
//{
//    public virtual ICollection<FinancaEmGrupo> Financas { get; set; }

//    public GrupoFinancas()
//    {

//    }
//}

//public class FinancaEmGrupo : Financa
//{
//    public virtual GrupoFinancas Grupo { get; set; }

//    public Guid? GrupoId { get; set; }

//    public FinancaEmGrupo()
//        : base(TipoRegistroEnum.Misto)
//    {

//    }
//}

//public class GrupoParcelamento : Lancamento
//{
//    public virtual ICollection<Parcelamento_> Parcelamentos { get; set; }

//    public GrupoParcelamento()
//    {

//    }
//}

//public class GrupoParcela : Lancamento
//{
//    public virtual ICollection<Parcelamento_> Parcelas { get; set; }

//    public GrupoParcela()
//    {

//    }
//}

//public class FinancaAVista : Financa
//{
//    public FinancaAVista()
//        : base(TipoRegistroEnum.Misto)
//    {

//    }
//}

//public class FinancaAPrazo : Financa
//{
//    //public bool EhParcelamento { get; set; }

//    //public virtual Prazo________ Prazo { get; set; }

//    //public virtual ICollection<Prazo________> Prazos { get; set; }

//    public FinancaAPrazo()
//        : base(TipoRegistroEnum.Misto) // : base(TipoRegistroEnum.DeCompetencia)
//    {

//    }
//}

//public class Prazo________ : Financa
//{
//    public bool EhParcela { get; set; }

//    public virtual FinancaAPrazo Referencia { get; set; }

//    public Guid? ReferenciaId { get; set; }

//    public Prazo________()
//        : base(TipoRegistroEnum.DeCaixa)
//    {

//    }
//}

//public class Parcelamento : Financa
//{
//    public int NumeroParcelas { get; set; }

//    public virtual ICollection<Parcela> Parcelas { get; set; }

//    public Parcelamento()
//        : base(TipoRegistroEnum.DeCompetencia)
//    {

//    }
//}

//public class Parcela : Financa
//{
//    public Parcelamento? Parcelamento { get; set; }

//    public Guid? ParcelamentoId { get; set; }

//    public int Numero { get; set; }

//    public Parcela()
//        : base(TipoRegistroEnum.DeCaixa)
//    {

//    }
//}

//public class PrevisaoIndeterminada : Financa
//{
//    public decimal ValorPrevisto { get; set; }

//    public bool EstaDentroPrevisto
//    {
//        get
//        {
//            if (TipoFinancaId == TipoFinancaEnum.Receita)
//            {
//                if (Math.Abs(Valor) > Math.Abs(ValorPrevisto))
//                {
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                if (Math.Abs(Valor) < Math.Abs(ValorPrevisto))
//                {
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }
//    }

//    public bool EstaForaPrevisto { get => !EstaDentroPrevisto; }

//    public PrevisaoIndeterminada()
//        : base(TipoRegistroEnum.Misto)
//    {
//        EhPrevisao = true;
//    }
//}

public class FinancaProcessadaEvent
{
    public Financa Financa { get; set; }
}
