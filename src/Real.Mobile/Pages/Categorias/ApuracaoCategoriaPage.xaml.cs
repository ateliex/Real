using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Handlers;
using Real.Data;
using Real.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
//using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
//using static Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.RefreshView;

namespace Real.Pages.Categorias;

public partial class ApuracaoCategoriaPage : ContentPage, IQueryAttributable
{
    public DateTime? Competencia { get; set; }

    public ModoVisualizacaoEnum ModoVisualizacao { get; set; } = ModoVisualizacaoEnum.Tabela;

    public OrdemEnum Ordem { get; set; } = OrdemEnum.Padrao;

    public RegimeApuracaoEnum RegimeApuracao { get; set; } = RegimeApuracaoEnum.Caixa;

    public bool ExibirTodasCategorias { get; set; } = false;

    //public ApuracaoCategoriasModel Apuracao { get; set; }

    public ICommand RefreshViewCommand { get; set; }

    public ICommand RefreshCommand { get; set; }

    public ICommand EditCommand { get; set; }

    public ICommand ModoVisualizacaoCommand { get; set; }

    public ICommand OrdemCommand { get; set; }

    public ICommand RegimeApuracaoCommand { get; set; }

    public ICommand CriarNovoCommand { get; set; }

    public ICommand SincronizarTudoCommand { get; set; }

    public ApuracaoCategoriaModel ApuracaoCategoria { get; private set; }

    public ApuracaoCategoriaPage()
    {
        InitializeComponent();

        //Categorias = _db.Categorias.Local.ToObservableCollection();

        //categoriasSearchHandler.Categorias = Categorias;

        RefreshViewCommand = new Command(OnRefreshView);

        RefreshCommand = new Command(OnRefresh);

        ModoVisualizacaoCommand = new Command<ModoVisualizacaoEnum>(ModoVisualizacaoAction);

        OrdemCommand = new Command<OrdemEnum>(OrdemAction);

        RegimeApuracaoCommand = new Command<RegimeApuracaoEnum>(RegimeApuracaoAction);

        CriarNovoCommand = new Command(CriarNovo);

        SincronizarTudoCommand = new Command(SincronizarTudo);

        //refreshView.On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>().SetRefreshPullDirection(RefreshPullDirection.TopToBottom);

        var hoje = DateTime.Today;

        Competencia = hoje;

        BindingContext = this;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ThreadPool.QueueUserWorkItem(async (state) =>
        {
            await Task.Delay(100);

            Competencia = query[nameof(Competencia)] as DateTime?;

            OnPropertyChanged(nameof(Competencia));

            ApuracaoCategoria = query[nameof(ApuracaoCategoria)] as ApuracaoCategoriaModel;

            OnPropertyChanged(nameof(ApuracaoCategoria));
        });
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }

    private async void OnEdit()
    {
        var categoriaId = ApuracaoCategoria.CategoriaId;

        var parameters = new ShellNavigationQueryParameters {
            { "CategoriaId", categoriaId }
        };

        await Shell.Current.GoToAsync("Categoria", parameters);
    }

    private void ModoVisualizacaoAction(ModoVisualizacaoEnum modoVisualizacao)
    {
        ModoVisualizacao = modoVisualizacao;

        //refreshView.IsRefreshing = true;
    }

    private void OrdemAction(OrdemEnum ordem)
    {
        Ordem = ordem;

        //refreshView.IsRefreshing = true;
    }

    private void RegimeApuracaoAction(RegimeApuracaoEnum regimeApuracao)
    {
        RegimeApuracao = regimeApuracao;

        //refreshView.IsRefreshing = true;
    }

    private void OnRefresh()
    {
        //refreshView.IsRefreshing = true;
    }

    private void OnRefreshView()
    {
        ThreadPool.QueueUserWorkItem(async (state) =>
        {
            await Task.Delay(1000);

            Dispatcher.Dispatch(() =>
            {
                //refreshView.IsRefreshing = false;
            });
        }, null);
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Categoria");
    }

    private void SincronizarTudo()
    {

    }
}