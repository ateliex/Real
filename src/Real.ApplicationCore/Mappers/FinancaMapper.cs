using Real.Models;

namespace Real.Mappers;

public static class FinancaMapper
{
    public static FinancaDataModel ToModel(this Financa financa)
    {
        return new FinancaDataModel
        {
            Id = financa.Id,
            TipoFinancaId = financa.TipoFinancaId,
            Data = financa.Data,
            Descricao = financa.Descricao,
            Valor = financa.Valor,
            CategoriaId = financa.CategoriaId,
            Categoria = financa.Categoria.ToModel()
        };
    }
    
    public static ICollection<FinancaDataModel> ToModel(this ICollection<Financa> financas)
    {
        return financas.Select(ToModel).ToArray();
    }
}
