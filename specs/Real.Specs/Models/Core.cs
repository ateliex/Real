using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class ContaData
{
    public required string Nome { get; set; }

    public int Ordem { get; set; }

    public bool Ativa { get; set; }

    public string? Pessoa { get; set; }
}
