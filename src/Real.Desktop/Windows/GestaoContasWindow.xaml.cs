using Real.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Real.Models;
using Real.Repositories;

namespace Real.Windows;

public partial class GestaoContasWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly RealDbContext _db;

    private readonly ContasRepositoryInterface _contasRepositoryInterface;

    private CollectionViewSource _contasViewSource;

    private ObservableCollection<ContaModel> _contas;

    public GestaoContasWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<RealDbContext>();

        _contasRepositoryInterface = _scope.ServiceProvider.GetRequiredService<ContasRepositoryInterface>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        _contasViewSource = ((CollectionViewSource)(this.FindResource("contasViewSource")));

        var contas = await _db.Contas
            .ToArrayAsync();

        //_contas = _db.Contas.Local.ToObservableCollection();

        var x = contas
            .Select(x => MapFrom(x));

        _contas = new ObservableCollection<ContaModel>(x);

        _contas.CollectionChanged += Contas_CollectionChanged;

        _contasViewSource.Source = _contas;

        Cursor = null;
    }

    private ContaModel MapFrom(Conta conta)
    {
        var contaModel = new ContaModel(conta, _contasRepositoryInterface);

        return contaModel;
    }

    private void Contas_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var conta = e.NewItems[0] as Conta;
        }
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource contasViewSource = ((CollectionViewSource)(this.FindResource("contasViewSource")));

        //var observableCollection = (ContasCollection)contasViewSource.Source;

        contaViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Contas salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoContaButton_Click(object sender, RoutedEventArgs e)
    {
        var conta = new Conta
        {
            Nome = "",
            //CreationDate = DateTime.Now,
        };

        var model = new ContaModel(conta, _contasRepositoryInterface);

        //var daysOfWeek = Enum.GetValues<DayOfWeek>();

        //foreach (var dayOfWeek in daysOfWeek)
        //{
        //    var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
        //    {
        //        DiaSemana = dayOfWeek,
        //        Tempo = new TimeSpan(8, 0, 0)
        //    };

        //    conta.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _contas.Add(model);
    }

    private void transferirButton_Click(object sender, RoutedEventArgs e)
    {
        var contaA = new Conta();

        var contaB = new Conta();

        contaA.Creditar(contaB, 100, null);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

