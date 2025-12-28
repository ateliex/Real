using Real.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Factories;

public static class ApuracaoFactory
{
    public static Apuracao MapFrom(this ApuracaoConta apuracaoConta)
    {
        var apuracao = new Apuracao
        {
            Competencia = apuracaoConta.Competencia,
            StatusId = StatusApuracaoEnum.Aberta,
            ValorPorCompetencia = apuracaoConta.Valor,
            ValorPorData = apuracaoConta.Valor,
            //ValorAcumuladoAnterior = valorAcumuladoAnterior,
            //ValorAcumuladoTotal = valorAcumuladoTotal,
            //ValorSaldoTotal = valorSaldoTotal,
            //ValorDebitosTotal = 0,
            //ValorCreditosAReceberTotal = valorCreditosAReceberTotal,
            //ValorCreditosAPagarTotal = valorCreditosAPagarTotal,
            Observacao = null
        };

        return apuracao;
    }
}
