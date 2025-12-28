using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class CategoriaData
{
    public required string Nome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int? Ordem { get; set; }

    public bool? Ativa { get; set; }

    public string? IconId { get; set; }

    public string? CategoriaPai { get; set; }
}
