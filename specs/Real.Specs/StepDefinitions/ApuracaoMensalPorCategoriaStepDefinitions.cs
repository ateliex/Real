using System;
using Microsoft.VisualBasic;
using Real.Data;
using Real.Drivers;
using Real.Models;
using Reqnroll;

namespace Real.StepDefinitions;

[Binding]
public class ApuracaoMensalPorCategoriaStepDefinitions
{
    private readonly CategoriasPageDriver _categoriasPageDriver;
    private readonly FinancasPageDriver _financasPageDriver;
    private readonly RealDbContext _db;

    private ApuracaoMensalOutput _output;

    public ApuracaoMensalPorCategoriaStepDefinitions(
        CategoriasPageDriver categoriasPageDriver,
        FinancasPageDriver financasPageDriver,
        RealDbContext db)
    {
        _categoriasPageDriver = categoriasPageDriver;
        _financasPageDriver = financasPageDriver;
        _db = db;
    }

    [Given("que existe as seguintes finanças à vista:")]
    public void GivenQueExisteAsSeguintesFinancasAVista(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new FinancaData
        {

        });

        var financas =
            from data in dataSet
            select new Financa
            {
                Id = Guid.NewGuid(),
                Competencia = DateOnly.FromDateTime(data.Competencia),
                Data = data.Data,
                CategoriaId = data.Categoria,
                Descricao = data.Descricao,
                Valor = data.Valor,
                TipoFinancaId = data.Tipo
            };

        _db.Financas.AddRange(financas);
        _db.SaveChanges();
    }

    [Given("que existe as seguintes finanças a prazo:")]
    public void GivenQueExisteAsSeguintesFinancasAPrazo(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new FinancaData
        {

        });

        var financas =
            from data in dataSet
            select new Financa
            {
                Id = Guid.NewGuid(),
                Competencia = DateOnly.FromDateTime(data.Competencia),
                Data = data.Data,
                CategoriaId = data.Categoria,
                Descricao = data.Descricao,
                Valor = data.Valor,
                TipoFinancaId = data.Tipo
            };

        _db.Financas.AddRange(financas);
        _db.SaveChanges();
    }

    [Given("que uma finança a prazo foi criada como:")]
    public void GivenQueUmaFinancaAPrazoFoiCriadaComo(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new CriacaoFinancaAPrazoInput
        {
            Conta = "Conta",
            Competencia = DateTime.Now,
            Data = DateTime.Now,
            Categoria = "Categoria",
            Descricao = "Descrição",
            Valor = 0,
            Tipo = TipoFinancaEnum.Despesa
        });

        var financas =
            from data in dataSet
            select new Financa
            {
                Id = Guid.NewGuid(),
                Competencia = DateOnly.FromDateTime(data.Competencia),
                Data = data.Data,
                CategoriaId = data.Categoria,
                Descricao = data.Descricao,
                Valor = data.Valor,
                TipoFinancaId = data.Tipo
            };

        _db.Financas.AddRange(financas);
        _db.SaveChanges();
    }

    [When("eu criar uma finança a prazo como:")]
    public void WhenEuCriarUmaFinancaAPrazoComo(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new CriacaoFinancaAPrazoInput
        {
            Conta = "Conta",
            Competencia = DateTime.Now,
            Data = DateTime.Now,
            Categoria = "Categoria",
            Descricao = "Descrição",
            Valor = 0,
            Tipo = TipoFinancaEnum.Despesa
        });

        foreach (var item in dataSet)
        {
            _financasPageDriver.CriarFinancaAPrazo(item);
        }
    }

    [When("eu apurar as finanças por categoria do mês de {string} de {int} em regime de competência")]
    public void WhenEuApurarAsFinancasPorCategoriaDoMesEmRegimeDeCompetencia(string mes, int ano)
    {
        var input = new ApuracaoMensalInput
        {
            Competencia = new DateTime(ano, DateTime.ParseExact(mes, "MMMM", null).Month, 1),
        };

        _output = _categoriasPageDriver.Buscar(input);
    }

    [Then("a apuração mensal de finanças por categoria deverá ser:")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraSer(DataTable dataTable)
    {

    }

    [Then("a apuração mensal de finanças por categoria deverá ter {float} de receitas")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerDeReceitas(decimal valor)
    {
        _output.ValorReceitas.Should().Be(valor);
    }

    [Then("a apuração mensal de finanças por categoria deverá ter {float} de despesas")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerDeDespesas(decimal valor)
    {
        _output.ValorDespesas.Should().Be(valor);
    }

    [Then("a apuração mensal de finanças por categoria deverá ter {float} de saldo")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerDeSaldo(decimal valor)
    {
        _output.ValorSaldo.Should().Be(valor);
    }

    [Then("a apuração mensal de finanças por categoria deverá ter {float} de saldo acumulado")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerDeSaldoAcumulado(decimal valor)
    {
        _output.ValorSaldoAcumulado.Should().Be(valor);
    }

    [Then("a apuração mensal de finanças por categoria deverá ter {float} de saldo total")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerDeSaldoTotal(decimal valor)
    {
        _output.ValorSaldoTotal.Should().Be(valor);
    }

    [Then("a apuração mensal de finanças por categoria deverá ter as seguintes receitas:")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerAsSeguintesReceitas(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new ApuracaoCategoriaOutput
        {
            Nome = "Receita #1",
        });

        dataTable.CompareToSet(_output.Receitas);
    }

    [Then("a apuração mensal de finanças por categoria deverá ter as seguintes despesas:")]
    public void ThenAApuracaoMensalDeFinancasPorCategoriaDeveraTerAsSeguintesDespesas(DataTable dataTable)
    {
        var dataSet = dataTable.CreateSet(() => new ApuracaoCategoriaOutput
        {
            Nome = "Despesa #1",
        });

        dataTable.CompareToSet(_output.Despesas);
    }
}
