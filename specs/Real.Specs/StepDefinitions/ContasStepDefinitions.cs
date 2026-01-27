using Real.Data;
using Real.Models;

namespace Real.StepDefinitions;

[Binding]
public class ContasStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;

    private readonly FeatureContext _featureContext;

    private readonly RealDbContext _db;

    public ContasStepDefinitions(
        RealDbContext db)
    {
        _db = db;
    }

    [Given("que existe as seguintes contas:")]
    public void GivenQueExisteAsSeguintesContas(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new ContaData
        {
            Nome = "Conta #1",
            Ordem = 0,
            Ativa = true
        });

        var contas = dataSet.Select(x => new Conta
        {
            Id = Guid.NewGuid(),
            Nome = x.Nome,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            Pessoa = x.Pessoa,
            TipoContaId = TipoContaEnum.CreditoAPagar
        });

        _db.Contas.AddRange(contas);
        _db.SaveChanges();
    }

    [Given("que existe as seguintes contas de débito:")]
    public void GivenQueExisteAsSeguintesContasDeDebito(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new ContaData
        {
            Nome = "Conta de Débito #1",
            Ordem = 0,
            Ativa = true
        });

        var contas = dataSet.Select(x => new Conta
        {
            Id = Guid.NewGuid(),
            Nome = x.Nome,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            Pessoa = x.Pessoa,
            TipoContaId = TipoContaEnum.Debito
        });

        _db.Contas.AddRange(contas);
        _db.SaveChanges();
    }

    [Given("que existe as seguintes contas de crédito a receber:")]
    public void GivenQueExisteAsSeguintesContasDeCreditoAReceber(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new ContaData
        {
            Nome = "Conta de Crédito a Receber #1",
            Ordem = 0,
            Ativa = true
        });

        var contas = dataSet.Select(x => new Conta
        {
            Id = Guid.NewGuid(),
            Nome = x.Nome,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            Pessoa = x.Pessoa,
            TipoContaId = TipoContaEnum.CreditoAReceber
        });

        _db.Contas.AddRange(contas);
        _db.SaveChanges();
    }

    [Given("que existe as seguintes contas de crédito a pagar:")]
    public void GivenQueExisteAsSeguintesContasDeCreditoAPagar(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new ContaData
        {
            Nome = "Conta de Crédito a Pagar #1",
            Ordem = 0,
            Ativa = true
        });

        var contas = dataSet.Select(x => new Conta
        {
            Id = Guid.NewGuid(),
            Nome = x.Nome,
            Ordem = x.Ordem,
            Ativa = x.Ativa,
            Pessoa = x.Pessoa,
            TipoContaId = TipoContaEnum.CreditoAPagar
        });

        _db.Contas.AddRange(contas);
        _db.SaveChanges();
    }

    [Given("que existe uma conta {string}")]
    public void GivenQueExisteUmaConta(string contaNome)
    {
        var conta = new Conta
        {
            Id = Guid.NewGuid(),
            Nome = contaNome,
            Ordem = 0,
            Ativa = true
        };

        _db.Contas.Add(conta);
        _db.SaveChanges();
    }
}
