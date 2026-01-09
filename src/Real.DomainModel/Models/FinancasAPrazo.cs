using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public abstract class FinancaAPrazo : Financa
{
    public bool EhParcelamento { get; set; }

    public bool EhParcela { get; set; }

    protected FinancaAPrazo(TipoRegistroEnum tipoRegistroId)
        : base(tipoRegistroId)
    {

    }
}

public class Parcelamento : FinancaAPrazo
{
    public int NumeroParcelas { get; set; }

    public virtual ICollection<Parcela> Parcelas { get; set; } = new HashSet<Parcela>();

    public Parcelamento()
        : base(TipoRegistroEnum.DeCompetencia)
    {

    }
}

public class Parcela : FinancaAPrazo
{
    public Parcelamento? Parcelamento { get; set; }

    public Guid? ParcelamentoId { get; set; }

    public int? NumeroParcela { get; set; }

    public Parcela()
        : base(TipoRegistroEnum.DeCaixa)
    {

    }
}
