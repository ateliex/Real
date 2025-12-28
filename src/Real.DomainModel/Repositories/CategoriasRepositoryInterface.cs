using Real.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Repositories;

public interface CategoriasRepositoryInterface
{
    Task<ICollection<Categoria>> ObtemCategorias();

    Task<Categoria> ObtemCategoria(string id);

    Task<Categoria?> ObtemCategoriaOrDefault(string id);

    Task Adiciona(Categoria categoria);

    Task Atualiza(Categoria categoria);

    Task Exclui(Categoria categoria);
}
