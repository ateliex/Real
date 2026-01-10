using Microsoft.Extensions.DependencyInjection;
using Real.Windows;
using System;
using System.Windows;

namespace Real;

public partial class MainWindow : Window
{
    public IServiceProvider ServiceProvider { get; }

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        ServiceProvider = serviceProvider;
    }

    private void CadastroCategoriasMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var cadastroCategoriasWindow = ServiceProvider.GetRequiredService<CadastroCategoriasWindow>();

        cadastroCategoriasWindow.Show();
    }

    private void GestaoContasMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var gestaoContasWindow = ServiceProvider.GetRequiredService<GestaoContasWindow>();

        gestaoContasWindow.Show();
    }

    private void ControleFinancasMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var controleFinancasWindow = ServiceProvider.GetRequiredService<ApuracaoAnualFinancasPorCategoriaWindow>();

        controleFinancasWindow.Show();
    }

    private void configuracoesMenuItem_Click(object sender, RoutedEventArgs e)
    {

    }
}
