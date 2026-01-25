using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class FinancaAVista : Financa
{
    public bool EhRecorrente { get => RecorrenciaId.HasValue; }

    public Recorrencia? Recorrencia { get; set; }

    public Guid? RecorrenciaId { get; set; }

    public FinancaAVista(
        Conta conta,
        Guid id,
        TipoLancamentoEnum tipoLancamentoId,
        TipoCompetenciaEnum tipoCompetenciaId,
        DateTime data,
        string descricao,
        decimal valor,
        TipoFinancaEnum tipoFinancaId,
        Categoria categoria,
        bool ehPrevisao,
        Recorrencia recorrencia)
        : base(
            conta,
            id,
            tipoLancamentoId,
            TipoRegistroEnum.Misto,
            tipoCompetenciaId,
            data,
            descricao,
            valor,
            tipoFinancaId,
            categoria,
            ehPrevisao)
    {
        Recorrencia = recorrencia;
        RecorrenciaId = recorrencia?.Id;
    }

    public FinancaAVista()
        : base(TipoRegistroEnum.Misto)
    {

    }
}
