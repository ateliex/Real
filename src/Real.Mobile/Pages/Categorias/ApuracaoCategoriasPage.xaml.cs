using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Handlers;
using Real.Data;
using Real.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
//using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
//using static Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.RefreshView;

namespace Real.Pages.Categorias;

public partial class ApuracaoCategoriasPage : ContentPage
{
    private readonly ApuracaoService _apuracaoService;

    private readonly RealDbContext _db;

    public DateTime Competencia { get; set; }

    public ModoVisualizacaoEnum ModoVisualizacao { get; set; } = ModoVisualizacaoEnum.Tabela;

    public OrdemEnum Ordem { get; set; } = OrdemEnum.Padrao;

    public RegimeApuracaoEnum RegimeApuracao { get; set; } = RegimeApuracaoEnum.Caixa;

    public bool ExibirTodasCategorias { get; set; } = false;

    //public ApuracaoCategoriasModel Apuracao { get; set; }

    public ICommand RefreshViewCommand { get; set; }

    public ICommand RefreshCommand { get; set; }

    public ICommand ModoVisualizacaoCommand { get; set; }

    public ICommand OrdemCommand { get; set; }

    public ICommand RegimeApuracaoCommand { get; set; }

    public ICommand AddNewCommand { get; set; }

    public ICommand SearchCommand { get; set; }

    public ICommand SyncAllCommand { get; set; }

    public ApuracaoFinancasPorCategoriaModel Apuracao { get; set; }


    public ObservableRangeCollection<ApuracaoCategoriasModel> Categorias { get; set; }

    public ApuracaoCategoriasPage(
        ApuracaoService apuracaoService,
        RealDbContext db)
    {
        InitializeComponent();

        _apuracaoService = apuracaoService;
        _db = db;

        //Categorias = _db.Categorias.Local.ToObservableCollection();

        Categorias = new ObservableRangeCollection<ApuracaoCategoriasModel>();

        //categoriasSearchHandler.Categorias = Categorias;

        RefreshViewCommand = new Command(OnRefreshView);

        RefreshCommand = new Command(OnRefresh);

        ModoVisualizacaoCommand = new Command<ModoVisualizacaoEnum>(ModoVisualizacaoAction);

        OrdemCommand = new Command<OrdemEnum>(OrdemAction);

        RegimeApuracaoCommand = new Command<RegimeApuracaoEnum>(RegimeApuracaoAction);

        AddNewCommand = new Command(AddNew);

        SearchCommand = new Command(Search);

        SyncAllCommand = new Command(SyncAll);

        //refreshView.On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>().SetRefreshPullDirection(RefreshPullDirection.TopToBottom);

        var hoje = DateTime.Today;

        Competencia = hoje;

        var apuracao = new ApuracaoFinancasPorCategoriaModel
        {
            Competencia = DateOnly.FromDateTime(Competencia),
            StatusId = StatusApuracaoEnum.Aberta,
            ValorAcumuladoAnterior = 0,
            ValorAcumuladoTotal = 0,
            ValorSaldoTotal = 0,
            ValorReceitasTotal = 0,
            ValorDespesasTotal = 0,
        };

        Apuracao = apuracao;

        BindingContext = this;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (Categorias.Count == 0)
        {
            refreshView.IsRefreshing = true;
        }
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        refreshView.IsRefreshing = true;
    }

    private void ModoVisualizacaoAction(ModoVisualizacaoEnum modoVisualizacao)
    {
        ModoVisualizacao = modoVisualizacao;

        refreshView.IsRefreshing = true;
    }

    private void OrdemAction(OrdemEnum ordem)
    {
        Ordem = ordem;

        refreshView.IsRefreshing = true;
    }

    private void RegimeApuracaoAction(RegimeApuracaoEnum regimeApuracao)
    {
        RegimeApuracao = regimeApuracao;

        refreshView.IsRefreshing = true;
    }

    private void OnRefresh()
    {
        refreshView.IsRefreshing = true;
    }

    private void OnRefreshView()
    {
        ThreadPool.QueueUserWorkItem(async (state) =>
        {
            await Task.Delay(1000);

            var competencia = DateOnly.FromDateTime(Competencia);

            var apuracaoCategorias = await _apuracaoService.ApurarCategoriasPorCompetencia(competencia, RegimeApuracao);

            var categorias = new List<ApuracaoCategoriasModel>();

            var receitas = MapFrom("Receitas", apuracaoCategorias.Receitas);

            categorias.AddRange(receitas);

            var despesas = MapFrom("Despesas", apuracaoCategorias.Despesas);

            categorias.AddRange(despesas);

            Categorias.Clear();

            Categorias.AddRange(categorias);

            Apuracao.Competencia = apuracaoCategorias.Competencia;
            Apuracao.StatusId = StatusApuracaoEnum.Aberta;
            Apuracao.ValorAcumuladoAnterior = apuracaoCategorias.ValorAcumuladoAnterior;
            Apuracao.ValorAcumuladoTotal = apuracaoCategorias.ValorAcumulado;
            Apuracao.ValorSaldoTotal = apuracaoCategorias.ValorSaldo;
            Apuracao.ValorReceitasTotal = apuracaoCategorias.ValorReceitas;
            Apuracao.ValorDespesasTotal = apuracaoCategorias.ValorDespesas;

            Dispatcher.Dispatch(() =>
            {
                refreshView.IsRefreshing = false;
            });
        }, null);
    }

    private ApuracaoCategoriasModel MapFrom(string groupName, IEnumerable<ApuracaoCategoria> apuracoesCategoria)
    {
        var apuracaoCategoriaList = new List<ApuracaoCategoriaModel>();

        var apuracoesCategoriaOrdered = new List<ApuracaoCategoria>();

        switch (Ordem)
        {
            case OrdemEnum.Padrao:
                apuracoesCategoriaOrdered = apuracoesCategoria
                    .OrderBy(x => x.Categoria.Ordem)
                    .ToList();

                break;
            case OrdemEnum.Decrescente:
                apuracoesCategoriaOrdered = apuracoesCategoria
                    .OrderBy(x => x.Valor)
                    .ToList();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var valorGrupo = 0m;

        foreach (var apuracaoCategoria in apuracoesCategoriaOrdered)
        {
            if (Math.Abs(apuracaoCategoria.Valor) > 0 || ExibirTodasCategorias)
            {
                var item = new ApuracaoCategoriaModel
                {
                    GroupName = groupName,
                    CategoriaId = apuracaoCategoria.CategoriaId,
                    Nome = apuracaoCategoria.Categoria.Nome,
                    CategoriaNome = apuracaoCategoria.Categoria.Nome,
                    AplicaReceita = apuracaoCategoria.Categoria.AplicaReceita,
                    AplicaDespesa = apuracaoCategoria.Categoria.AplicaDespesa,
                    Ordem = apuracaoCategoria.Categoria.Ordem.Value,
                    IconFaUnicode = apuracaoCategoria.Categoria.Icon.FaUnicode,
                    Valor = apuracaoCategoria.Valor,
                    Financas = new ObservableCollection<FinancaPorCategoriaModel>(apuracaoCategoria.Financas.Select(x => new FinancaPorCategoriaModel
                    {
                        FinancaId = x.Id,
                        TipoFinancaId = x.TipoFinancaId,
                        //ContaTipoId = x.FormaRegistroId,
                        //ContaNome = x.Conta.Nome,
                        Competencia = x.Competencia,
                        Data = x.Data,
                        Descricao = x.Descricao,
                        Valor = x.Valor,
                        ValorOriginal = (x as PrevisaoInteligente)?.ValorOriginal,
                        ValorExcedente = (x as PrevisaoInteligente)?.ValorExcedente,
                        EhPrevisao = x.EhPrevisao
                    }))
                };

                apuracaoCategoriaList.Add(item);

                valorGrupo += item.Valor;
            }
        }

        var apuracaoCategorias = new ApuracaoCategoriasModel(groupName, valorGrupo, apuracaoCategoriaList);

        return apuracaoCategorias;
    }

    private async void AddNew()
    {
        await Shell.Current.GoToAsync($"Categoria");
    }

    private async void Search()
    {
        await Shell.Current.GoToAsync($"Cadastro");
    }

    private void SyncAll()
    {
        Categorias.Clear();
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            if (e.CurrentSelection[0] is ApuracaoCategoriaModel)
            {
                var apuracaoCategoria = e.CurrentSelection[0] as ApuracaoCategoriaModel;

                var parameters = new ShellNavigationQueryParameters {
                    { "Competencia", Competencia },
                    { "ApuracaoCategoria", apuracaoCategoria }
                };

                await Shell.Current.GoToAsync("ApuracaoCategoria", parameters);

                collectionView.SelectedItem = null;
            }
        }
    }
}
