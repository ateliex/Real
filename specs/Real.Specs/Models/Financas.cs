using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class FinancaData
{
    public string Conta { get; set; }

    public DateTime Competencia { get; set; }

    public DateTime Data { get; set; }

    public string Categoria { get; set; }

    public string Descricao { get; set; }

    public decimal Valor { get; set; }

    public TipoFinancaEnum Tipo { get; set; }
}

public class CriacaoFinancaAPrazoInput
{
    public string Conta { get; set; }

    public DateTime Competencia { get; set; }

    public DateTime Data { get; set; }

    public string Categoria { get; set; }

    public string Descricao { get; set; }

    public decimal Valor { get; set; }

    public TipoFinancaEnum Tipo { get; set; }
}
