using Real.Models;

namespace Real.Mappers;

public static class CategoriaMapper
{
    public static CategoriaDataModel ToModel(this Categoria categoria)
    {
        return new CategoriaDataModel
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            AplicaReceita = categoria.AplicaReceita,
            AplicaDespesa = categoria.AplicaDespesa,
            Ordem = categoria.Ordem,
            Ativa = categoria.Ativa,
            BiIcon = categoria.IconId,
            CategoriaPaiId = categoria.CategoriaPaiId
        };
    }
    
    public static ICollection<CategoriaDataModel> ToModel(this ICollection<Categoria> categorias)
    {
        return categorias.Select(ToModel).ToArray();
    }
}
