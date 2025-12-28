using AngleSharp.Html.Dom;
using Real.Helpers;
using Real.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Drivers;

public class FinancasPageDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement NovaFinancaAPrazoAnchor { get; private set; }

    public FinancasPageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Financas");

        //

        NovaFinancaAPrazoAnchor = Document.GetAnchor("Criacao.Empregador");

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

    //public Empregador SolicitarNovaFinancaAPrazo()
    //{
    //    GoTo();

    //    Document = _angleSharp.GetDocument(NovaFinancaAPrazoAnchor.Href);

    //    var form = Document.GetForm();

    //    var cadastroEmpregador = new Empregador
    //    {
    //        Nome = form.GetInput("CadastroEmpregador.Nome").Value,
    //    };

    //    return cadastroEmpregador;
    //}

    public void CriarFinancaAPrazo(CriacaoFinancaAPrazoInput input)
    {
        GoTo();

        Document = _angleSharp.GetDocument(NovaFinancaAPrazoAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("CadastroEmpregador.Descricao").Value = input.Descricao;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        if (hasErrors)
        {
            var erros = Document.GetValidationErrors();

            var span = erros.FirstSpan();

            throw new Exception(span.InnerHtml);
        }
        else
        {
            //var empregadorCadastrado = await ObtemDetalhes();

            //return empregadorCadastrado;
        }
    }
}
