using Real.Data;
using Real.Models;

namespace Real.StepDefinitions;

[Binding]
public class CategoriasStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;

    private readonly FeatureContext _featureContext;

    private readonly RealDbContext _db;

    public CategoriasStepDefinitions(
        RealDbContext db)
    {
        _db = db;
    }

    [Given("que existe as seguintes categorias:")]
    public void GivenQueExisteAsSeguintesCategorias(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new CategoriaData
        {
            Nome = "Categoria #1",
            AplicaReceita = false,
            AplicaDespesa = true,
            Ordem = 0,
            Ativa = true,
            IconId = "p-circle"
        });

        var categorias = dataSet.Select(x => new Categoria
        {
            Id = x.Nome,
            Nome = x.Nome,
            AplicaReceita = x.AplicaReceita,
            AplicaDespesa = x.AplicaDespesa,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            IconId = x.IconId
        });

        _db.Categorias.AddRange(categorias);
        _db.SaveChanges();
    }

    [Given("que existe as seguintes categorias de receitas:")]
    public void GivenQueExisteAsSeguintesCategoriasDeReceitas(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new CategoriaData
        {
            Nome = "Receita #1",
            AplicaReceita = true,
            AplicaDespesa = false,
            Ordem = 0,
            Ativa = true,
            IconId = "p-circle"
        });

        var categorias = dataSet.Select(x => new Categoria
        {
            Id = x.Nome,
            Nome = x.Nome,
            AplicaReceita = x.AplicaReceita,
            AplicaDespesa = x.AplicaDespesa,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            IconId = x.IconId
        });

        _db.Categorias.AddRange(categorias);
        _db.SaveChanges();
    }

    [Given("que existe as seguintes categorias de despesas:")]
    public void GivenQueExisteAsSeguintesCategoriasDeDespesas(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new CategoriaData
        {
            Nome = "Despesa #1",
            AplicaReceita = false,
            AplicaDespesa = true,
            Ordem = 0,
            Ativa = true,
            IconId = "p-circle"
        });

        var categorias = dataSet.Select(x => new Categoria
        {
            Id = x.Nome,
            Nome = x.Nome,
            AplicaReceita = x.AplicaReceita,
            AplicaDespesa = x.AplicaDespesa,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            IconId = x.IconId
        });

        _db.Categorias.AddRange(categorias);
        _db.SaveChanges();
    }

    [Given("que existe uma categoria {string}")]
    public void GivenQueExisteUmaCategoria(string categoriaNome)
    {
        var categoria = new Categoria
        {
            Id = categoriaNome,
            Nome = categoriaNome,
            AplicaReceita = false,
            AplicaDespesa = true,
            Ordem = 0,
            Ativa = true,
            IconId = "p-circle"
        };

        _db.Categorias.Add(categoria);
        _db.SaveChanges();
    }
}

