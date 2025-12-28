using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

public interface ConsultaCategoriasInterface
{
    Task<ICollection<CategoriaDataModel>> ConsultaCategorias();

    Task<CategoriaDataModel> ConsultaCategoria(string id);
}

public class CategoriaDataModel
{
    public string? Id { get; set; }

    public string? Nome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int? Ordem { get; set; }

    public bool? Ativa { get; set; }

    public string? BiIcon { get; set; }

    //public Categoria? CategoriaPai { get; set; }

    public string? CategoriaPaiId { get; set; }
}

public interface CadastroCategoriaInterface
{
    Task CadastraCategoria(CategoriaInputModel input);

    Task AtualizaCategoria(CategoriaInputModel input);
}

public class CategoriaInputModel
{
    public string? Id { get; set; }

    public string? Nome { get; set; }

    public bool AplicaReceita { get; set; }

    public bool AplicaDespesa { get; set; }

    public int? Ordem { get; set; }

    public bool? Ativa { get; set; }

    public string? BiIcon { get; set; }

    //public Categoria? CategoriaPai { get; set; }

    public string? CategoriaPaiId { get; set; }
}

public interface ExclusaoCategoriaInterface
{
    Task ExcluiCategoria(string id);
}
