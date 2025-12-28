using AngleSharp.Html.Dom;
using Real.Helpers;
using Real.Support;

namespace Real.Drivers;

public class CategoriasPageDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    //public IHtmlAnchorElement CategoriasAnchor { get; private set; }

    public CategoriasPageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Categorias");

        //

        //CategoriasAnchor = Document.GetAnchor("Criacao.Empregador");

        //CadastroEmpregadorAnchor.Should().NotBeNull("a tela de empregadores deve ter um link de criação de empregador");
    }

    private void Identifica(string nomeEmpregador)
    {
        var table = Document.GetTable("Empregadores");

        var tableRow = table.GetTableRowByDataName(nomeEmpregador);

        //

        //DetalheEmpregadorAnchor = tableRow.GetAnchor("Detalhe");

        ////DetalheEmpregadorAnchor.Should().NotBeNull("a lista de empregadores deve ter um link de detalhe do empregador cadastrado");

        ////

        //EdicaoEmpregadorAnchor = tableRow.GetAnchor("Edicao");

        ////EdicaoEmpregadorAnchor.Should().NotBeNull("a lista de empregadores deve ter um link de edição do empregador cadastrado");

        ////

        //ExclusaoEmpregadorAnchor = tableRow.GetAnchor("Exclusao");

        //ExclusaoEmpregadorAnchor.Should().NotBeNull("a lista de empregadores deve ter um link de exclusão do empregador cadastrado");
    }

    //public Empregador SolicitarCadastroEmpregador()
    //{
    //    GoTo();

    //    Document = _angleSharp.GetDocument(CategoriasAnchor.Href);

    //    var form = Document.GetForm();

    //    var cadastroEmpregador = new Empregador
    //    {
    //        Nome = form.GetInput("CadastroEmpregador.Nome").Value,
    //    };

    //    return cadastroEmpregador;
    //}

    public ApuracaoMensalOutput Buscar(ApuracaoMensalInput input)
    {
        GoTo();

        var form = Document.GetForm();

        //var contrato = input.Contrato;

        //form.GetSelect("ContratoId").GetOption(contrato.Nome).IsSelected = true;
        form.GetInput("Competencia").ValueAsDate = input.Competencia;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var output = IdentificaFolhaParaApuracao();

        return output;
    }

    private ApuracaoMensalOutput IdentificaFolhaParaApuracao()
    {
        var receitasCollection = Document.QuerySelectorAll($".Receita");

        var receitas = receitasCollection.Select(element =>
        {
            var receitaElement = (IHtmlElement)element;

            var receita = new ApuracaoCategoriaOutput
            {
                Nome = receitaElement.QuerySelector($".Nome").TextContent.Trim(),
                Total = decimal.Parse(receitaElement.QuerySelector($".Total").TextContent)
            };

            return receita;
        });

        var despesasCollection = Document.QuerySelectorAll($".Despesa");

        var despesas = despesasCollection.Select(element =>
        {
            var despesaElement = (IHtmlElement)element;

            var despesa = new ApuracaoCategoriaOutput
            {
                Nome = despesaElement.QuerySelector($".Nome").TextContent.Trim(),
                Total = decimal.Parse(despesaElement.QuerySelector($".Total").TextContent)
            };

            return despesa;
        });

        var output = new ApuracaoMensalOutput
        {
            ValorReceitas = decimal.Parse(Document.QuerySelector(".ValorReceitas").TextContent),
            ValorDespesas = decimal.Parse(Document.QuerySelector(".ValorDespesas").TextContent),
            ValorSaldo = decimal.Parse(Document.QuerySelector(".ValorSaldo").TextContent),
            ValorSaldoAcumulado = decimal.Parse(Document.QuerySelector(".ValorSaldoAcumulado").TextContent),
            ValorSaldoTotal = decimal.Parse(Document.QuerySelector(".ValorSaldoTotal").TextContent),
            Receitas = receitas,
            Despesas = despesas
        };

        return output;
    }
}
