using System;
using Real.Data;
using Real.Models;
using Reqnroll;

namespace Real.StepDefinitions;

[Binding]
public class PrevisaoInteligenteStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;

    private readonly FeatureContext _featureContext;

    private readonly RealDbContext _db;

    private Conta _conta;

    public PrevisaoInteligenteStepDefinitions(
        RealDbContext db)
    {
        _db = db;
    }

    [Given("que existe uma previsão indeterminada de R$ {float} da categoria {string}")]
    public void GivenQueExisteUmaPrevisaoIndeterminadaDeRDaCategoria(decimal valor, string categoriaNome)
    {
        var previsao = new Financa
        {
            Id = Guid.NewGuid(),
            Descricao = "Previsão",
            Valor = valor,
        };

        _db.Financas.Add(previsao);
        _db.SaveChanges();
    }

    [When("eu lançar uma finança de R$ {float} na {string} da categoria {string}")]
    public void WhenEuLancarUmaFinancaDeRNaDaCategoria(decimal valor, string contaNome, string categoriaNome)
    {
        throw new PendingStepException();
    }

    [Then("o valor da previsão inteligente deverá ser de R$ {float}")]
    public void ThenOValorDaPrevisaoInteligenteDeveraSerDeR(decimal valor)
    {
        throw new PendingStepException();
    }
}
