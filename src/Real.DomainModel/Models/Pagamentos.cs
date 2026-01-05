using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Models;

//public class Boleto : Lancamento
//{
//    public Documento? Documento { get; set; }

//    public DateOnly DataVencimento { get; set; }

//    public ICollection<Financa> Financas { get; set; }
//}

public class Pagamento : ValueObject
{
    public decimal Valor { get; set; }

    public string Descricao { get; set; }

    public DateTime Data { get; set; }

    public Pagamento(decimal valor, string descricao, DateTime data)
    {
        Descricao = descricao;

        Valor = valor;

        Data = data;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Valor;

        yield return Descricao;

        yield return Data;
    }

    public Pagamento()
    {

    }
}