using Real.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Real.Models;

public class ContaModel : ObservableObject
{
    private readonly Conta _conta;
    private readonly ContasRepositoryInterface _contasRepository;

    public ContaModel(Conta conta, ContasRepositoryInterface contasRepository)
    {
        _conta = conta;
        _contasRepository = contasRepository;
    }

    [Required(ErrorMessage = "Teste: Nome Obrigatório")]
    public string Nome
    {
        get { return _conta.Nome; }
        set
        {
            _conta.AlteraNome(value);

            OnPropertyChanged();
        }
    }

    public bool Ativa
    {
        get { return _conta.Ativa; }
        set
        {
            if (value)
            {
                _conta.Ativar();
            }
            else
            {
                _conta.Desativar();
            }

            OnPropertyChanged();
        }
    }

    public void Transferir(Conta contaDestino, decimal valor)
    {
        _conta.Creditar(contaDestino, valor, _contasRepository);
    }
}

public class LancamentoModel : ObservableObject
{
    private readonly Lancamento _lancamento;

    [Required(ErrorMessage = "Descrição Obrigatória")]
    public string Descricao
    {
        get { return _lancamento.Descricao; }
        set
        {
            _lancamento.Descricao = value;

            OnPropertyChanged();
        }
    }

    public LancamentoModel(Lancamento lancamento)
    {
        _lancamento = lancamento;
    }
}