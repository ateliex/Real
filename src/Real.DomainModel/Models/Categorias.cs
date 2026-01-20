using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public class Categoria : Entity
{
    public string? Id { get; set; }

    public string? Nome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int? Ordem { get; set; }

    public bool Ativa { get; set; }

    public Icon? Icon { get; set; }

    public string? IconId { get; set; }

    public Categoria? CategoriaPai { get; set; }

    public string? CategoriaPaiId { get; set; }

    public Categoria(string id, string nome)
    {
        Id = id;
        Nome = nome;

        Ativa = true;
    }

    public Categoria()
    {

    }
}
