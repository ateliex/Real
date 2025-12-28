using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public enum TipoFinancaEnum
{
    Receita,
    Despesa
}

public class FinancaDataModel : LancamentoDataModel
{
    public TipoFinancaEnum TipoFinancaId { get; set; }

    public CategoriaDataModel Categoria { get; set; }

    public string? CategoriaId { get; set; }

    public bool EhPrevisao { get; set; }
}

public class FinancaComumDataModel : FinancaDataModel
{

}
