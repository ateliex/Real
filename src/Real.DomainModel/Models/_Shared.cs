using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class Icon
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string FaClass { get; set; }

    public string FaUnicode { get; set; }

    public string BiClass { get; set; }

    public string BiUnicode { get; set; }
}

public class Documento
{
    public string? Numero { get; set; }

    public string? Banco { get; set; }

    public string? Descricao { get; set; }

    public string? Pessoa { get; set; }
}
