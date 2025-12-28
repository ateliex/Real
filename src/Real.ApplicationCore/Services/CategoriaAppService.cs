using Real.Models;
using Real.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Services;

public class CategoriaAppService :
    CadastroCategoriaInterface,
    ExclusaoCategoriaInterface
{
    private readonly CategoriasRepositoryInterface _categoriasRepository;

    public CategoriaAppService(CategoriasRepositoryInterface categoriasRepository)
    {
        _categoriasRepository = categoriasRepository;
    }

    public async Task CadastraCategoria(CategoriaInputModel input)
    {
        var categoria = new Categoria()
        {
            Id = input.Id,
            Nome = input.Nome,
            AplicaReceita = input.AplicaReceita,
            AplicaDespesa = input.AplicaDespesa,
            Ordem = input.Ordem,
            Ativa = input.Ativa,
            IconId = input.BiIcon,
            CategoriaPaiId = input.CategoriaPaiId
        };

        await _categoriasRepository.Adiciona(categoria);
    }

    public async Task AtualizaCategoria(CategoriaInputModel input)
    {
        var categoriaExistente = await _categoriasRepository.ObtemCategoria(input.Id);

        categoriaExistente.Nome = input.Nome;

        await _categoriasRepository.Atualiza(categoriaExistente);
    }

    public async Task ExcluiCategoria(string id)
    {
        var categoria = await _categoriasRepository.ObtemCategoria(id);

        await _categoriasRepository.Exclui(categoria);
    }
}
