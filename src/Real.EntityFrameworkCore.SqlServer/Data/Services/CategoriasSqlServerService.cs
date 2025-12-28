using Microsoft.EntityFrameworkCore;
using Real.Mappers;
using Real.Models;
using Real.Repositories;
using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Data.Services;

public class CategoriasSqlServerService : CategoriasRepositoryInterface,
    ConsultaCategoriasInterface
{
    private readonly RealDbContext _db;

    public CategoriasSqlServerService(
        RealDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<CategoriaDataModel>> ConsultaCategorias()
    {
        var categorias = await _db.Categorias
            .AsNoTracking()
            .Select(x => x.ToModel())
            .ToListAsync();

        return categorias;
    }

    public async Task<CategoriaDataModel> ConsultaCategoria(string id)
    {
        var categoria = await _db.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categoria == default)
        {
            throw new EntityNotFoundException<Categoria>($"Id '{id}'");
        }

        var model = categoria.ToModel();

        return model;
    }

    public async Task<ICollection<Categoria>> ObtemCategorias()
    {
        var categorias = await _db.Categorias
            .ToListAsync();

        return categorias;
    }

    public async Task<Categoria> ObtemCategoria(string id)
    {
        var categoria = await _db.Categorias
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categoria == default)
        {
            throw new EntityNotFoundException<Categoria>($"Id '{id}'");
        }

        return categoria;
    }

    public async Task<Categoria?> ObtemCategoriaOrDefault(string id)
    {
        var categoria = await _db.Categorias
            .FirstOrDefaultAsync(x => x.Id == id);

        return categoria;
    }

    public async Task Adiciona(Categoria categoria)
    {
        _db.Categorias.Add(categoria);

        await _db.SaveChangesAsync();
    }

    public async Task Atualiza(Categoria categoria)
    {
        await _db.SaveChangesAsync();
    }

    public async Task Exclui(Categoria categoria)
    {
        _db.Categorias.Remove(categoria);

        await _db.SaveChangesAsync();
    }
}
