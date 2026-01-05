using Real.Repositories;
using System.DomainModel;
using System.Text.Json.Serialization;

namespace Real.Models;

//public class Conta : Entity
//{
//    public Guid Id { get; set; }

//    public required string Nome { get; set; }

//    public FormaRegistroEnum TipoContaId { get; set; }

//    public int? Ordem { get; set; }

//    public bool Ativa { get; set; }

//    //public virtual ICollection<Lancamento> Lancamentos { get; set; } = new HashSet<Lancamento>();

//    public string? Pessoa { get; set; }
//}

//public abstract class Lancamento : Entity
//{
//    public Guid Id { get; set; }

//    public TipoLancamentoEnum TipoLancamentoId { get; set; }

//    public TipoRegistroEnum TipoRegistroId { get; set; }

//    public virtual Conta Conta { get; set; }

//    public Guid? ContaId { get; set; }

//    public virtual DateOnly Competencia { get; set; }

//    /// <summary>
//    /// Data que vai ocorrer ou ocorreu o lançamento.
//    /// </summary>
//    public virtual DateTime Data { get; set; }

//    public required string Descricao { get; set; }

//    public string? TransacaoId { get; set; }

//    public string? Transacao { get; set; }

//    /// <summary>
//    /// Valor previsto ou realizado do lançamento.
//    /// </summary>
//    public virtual decimal Valor { get; set; }

//    public int? Ordem { get; set; }

//    #region Grupamento

//    //public bool EhGrupo { get; set; }

//    //public Grupo? Grupo { get; set; }

//    //public Guid? GrupoId { get; set; }

//    //public int Nivel { get; set; }

//    #endregion

//    #region Parcelamento

//    public bool EhParcelamento { get; set; }

//    public int NumeroParcelas { get; set; }

//    public virtual ICollection<Lancamento> Parcelas { get; set; } = new HashSet<Lancamento>();

//    public bool EhParcela { get => NumeroParcela.HasValue; }

//    public Lancamento? Parcelamento { get; set; }

//    public Guid? ParcelamentoId { get; set; }

//    public int? NumeroParcela { get; set; }

//    #endregion

//    #region Recorrência

//    public bool EhRecorrente { get; set; }

//    public Recorrencia? Recorrencia { get; set; }

//    public Guid? RecorrenciaId { get; set; }

//    #endregion
//}

//public class Grupo : Lancamento
//{
//    public virtual ICollection<Lancamento> Lancamentos { get; set; } = new HashSet<Lancamento>();

//    public Grupo()
//    {
//        EhGrupo = true;
//    }
//}
