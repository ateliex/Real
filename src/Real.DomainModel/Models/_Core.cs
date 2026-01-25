using Real.Repositories;
using System.DomainModel;
using System.Text.Json.Serialization;

namespace Real.Models;

public class Conta : Entity
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public TipoContaEnum TipoContaId { get; set; }

    public int Ordem { get; set; }

    public bool Ativa { get; set; }

    public virtual ICollection<Lancamento> Lancamentos { get; set; } = new HashSet<Lancamento>();

    public string? Pessoa { get; set; }

    public Conta(
        Guid id,
        string nome,
        TipoContaEnum tipoContaId,
        int ordem,
        string? pessoa)
    {
        Id = id;
        Nome = nome;
        TipoContaId = tipoContaId;
        Ordem = ordem;
        Pessoa = pessoa;
    }

    public void AlteraNome(string nome)
    {
        Nome = nome;
    }

    public void AlteraOrdem(int ordem)
    {
        Ordem = ordem;
    }

    public void Ativar()
    {
        Ativa = true;
    }

    public void Desativar()
    {
        Ativa = false;
    }

    public void AlteraPessoa(string? pessoa)
    {
        Pessoa = pessoa;
    }

    public void Creditar(
        Conta contaDestino,
        decimal valor,
        ContasRepositoryInterface contasRepositoryInterface)
    {
        var debito = new Movimento()
        {
            ContaId = Id,
            Valor = -valor,
            Descricao = $"Transferência para {contaDestino.Nome}"
        };

        contasRepositoryInterface.Adiciona(debito);

        contaDestino.Debitar(this, valor, contasRepositoryInterface);
    }

    public void Debitar(
        Conta contaOrigem,
        decimal valor,
        ContasRepositoryInterface contasRepositoryInterface)
    {
        var credito = new Movimento()
        {
            ContaId = Id,
            Valor = valor,
            Descricao = $"Transferência recebida de {contaOrigem.Nome}"
        };

        contasRepositoryInterface.Adiciona(credito);
    }

    public void ConverterParaFinancaAVista(
        Movimento movimento,
        TipoFinancaEnum tipoFinancaId,
        Categoria categoria,
        ContasRepositoryInterface contasRepositoryInterface)
    {
        var financaAVista = new FinancaAVista()
        {
            ContaId = movimento.ContaId,
            Competencia = movimento.Competencia,
            Data = movimento.Data,
            Descricao = movimento.Descricao,
            TransacaoId = movimento.TransacaoId,
            Transacao = movimento.Transacao,
            Valor = movimento.Valor,
            TipoFinancaId = tipoFinancaId,
            Categoria = categoria,
            CategoriaId = categoria.Id
        };

        contasRepositoryInterface.Adiciona(financaAVista);

        contasRepositoryInterface.Remove(movimento);
    }

    public async Task Adiciona(FinancaAVista lancamento, ContasRepositoryInterface contasRepositoryInterface)
    {
        await contasRepositoryInterface.Adiciona(lancamento);
    }

    public Conta()
    {
        Nome = string.Empty;
    }
}

public abstract class Lancamento : Entity
{
    public Guid Id { get; set; }

    public TipoLancamentoEnum TipoLancamentoId { get; set; }

    public TipoRegistroEnum TipoRegistroId { get; set; }

    public virtual Conta Conta { get; set; }

    public Guid ContaId { get; set; }

    public TipoCompetenciaEnum TipoCompetenciaId { get; set; }

    public virtual DateOnly Competencia { get; set; }

    /// <summary>
    /// Data que vai ocorrer ou ocorreu o lançamento.
    /// </summary>
    public virtual DateTime Data { get; set; }

    public string Descricao { get; set; }

    public string? TransacaoId { get; set; }

    public string? Transacao { get; set; }

    /// <summary>
    /// Valor previsto ou realizado do lançamento.
    /// </summary>
    public virtual decimal Valor { get; set; }

    #region Grupamento

    //public bool EhGrupo { get; set; }

    //public Grupo? Grupo { get; set; }

    //public Guid? GrupoId { get; set; }

    //public int Nivel { get; set; }

    #endregion

    protected Lancamento(
        Conta conta,
        Guid id,
        TipoLancamentoEnum tipoLancamentoId,
        TipoRegistroEnum tipoRegistroId,
        TipoCompetenciaEnum tipoCompetenciaId,
        DateTime data,
        string descricao,
        decimal valor)
    {
        Id = id;
        TipoLancamentoId = tipoLancamentoId;
        TipoRegistroId = tipoRegistroId;
        Conta = conta;
        ContaId = conta.Id;

        if (tipoCompetenciaId == TipoCompetenciaEnum.Anual)
        {
            Competencia = new DateOnly(data.Year, 1, 1);
        }
        else if (tipoCompetenciaId == TipoCompetenciaEnum.Mensal)
        {
            Competencia = new DateOnly(data.Year, data.Month, 1);
        }
        else if (tipoCompetenciaId == TipoCompetenciaEnum.Diaria)
        {
            Competencia = new DateOnly(data.Year, data.Month, data.Day);
        }

        Data = data;
        Descricao = descricao;
        Valor = valor;
    }

    protected Lancamento(TipoRegistroEnum tipoRegistroId)
    {
        TipoRegistroId = tipoRegistroId;
    }
}

//public class Grupo : Lancamento
//{
//    public virtual ICollection<Lancamento> Lancamentos { get; set; } = new HashSet<Lancamento>();

//    public Grupo()
//    {
//        EhGrupo = true;
//    }
//}
