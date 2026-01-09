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

    public FinancaAVista()
        : base(TipoRegistroEnum.Misto)
    {

    }
}
