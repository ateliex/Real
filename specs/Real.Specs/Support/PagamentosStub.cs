using Real.Models;
using System;

namespace Real.Support;

public static class PagamentosStub
{
    public static Pagamento ObtemPagamentoComValorQualquer(DateTime data)
    {
        var pagamento = new Pagamento(
            123.45m,
            null,
            data
        );

        return pagamento;
    }

    public static Pagamento ObtemPagamentoComMenorValorPossivel()
    {
        var pagamento = new Pagamento(
            0.001m,
            null,
            DateTime.Today
        );

        return pagamento;
    }
}
