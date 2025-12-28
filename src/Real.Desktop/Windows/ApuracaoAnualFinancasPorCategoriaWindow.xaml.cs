using Microsoft.Extensions.DependencyInjection;
using Real.Data;
using Real.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Real.Windows;

public partial class ApuracaoAnualFinancasPorCategoriaWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly ApuracaoService _apuracaoService;

    private readonly RealDbContext _db;

    private CollectionViewSource _categoriasViewSource;

    private ObservableCollection<ApuracaoCategoriaModel> _categoriasCollection;

    public ModoVisualizacaoEnum ModoVisualizacao { get; set; } = ModoVisualizacaoEnum.Tabela;

    public OrdemEnum Ordem { get; set; } = OrdemEnum.Padrao;

    public RegimeApuracaoEnum RegimeApuracao { get; set; } = RegimeApuracaoEnum.Competencia;

    public bool ExibirTodasCategorias { get; set; } = false;

    public ApuracaoAnualFinancasPorCategoriaWindow(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();

        _apuracaoService = _scope.ServiceProvider.GetRequiredService<ApuracaoService>();

        _db = _scope.ServiceProvider.GetRequiredService<RealDbContext>();

        InitializeComponent();
    }

    public int? Ano
    {
        get { return (int?)GetValue(AnoProperty); }
        set { SetValue(AnoProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Ano.  
    // This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AnoProperty = DependencyProperty.Register(
        "Ano",
        typeof(int?),
        typeof(ApuracaoAnualFinancasPorCategoriaWindow),
        new PropertyMetadata(DateTime.Now.Year,
        new PropertyChangedCallback(OnAnoChanged)));

    static void OnAnoChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        // Get reference to self
        ApuracaoAnualFinancasPorCategoriaWindow source = (ApuracaoAnualFinancasPorCategoriaWindow)sender;

        // Add Handling Code
        int? newValue = (int?)args.NewValue;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Buscar();
    }

    private void anoTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Buscar();
        }
    }

    private void BuscarButton_Click(object sender, RoutedEventArgs e)
    {
        Buscar();
    }

    private async void Buscar()
    {
        if (Cursor == Cursors.Wait)
        {
            return;
        }

        Cursor = Cursors.Wait;

        _categoriasViewSource = ((CollectionViewSource)(this.FindResource("categoriasViewSource")));

        anoTextBox.DataContext = this;
        anoTextBox.SetBinding(TextBox.TextProperty, new System.Windows.Data.Binding("Ano"));

        var hoje = DateTime.Today;

        if (Ano == null)
        {
            Ano = hoje.Year;
        }

        var competencia = new DateOnly(Ano.Value, 1, 1);

        var apuracaoAnualCategorias = await _apuracaoService.ApurarCategoriasPorCompetenciaAnual(competencia, RegimeApuracao);

        //var apuracaoAnualCategoriasModel = await MapFrom(apuracaoAnualCategorias);

        //var receitas = ApuraCategorias(apuracaoAnualCategorias.Receitas);

        var despesas = MapFrom(apuracaoAnualCategorias.Despesas);

        //await _db.Financas
        //    .LoadAsync();

        //var controlesCategoria = await _db.ControlesCategoria
        //    .Include(x => x.Categoria)
        //    .OrderBy(x => x.Categoria.Ordem)
        //    .ToListAsync();

        //var financas = await _db.Lancamentos
        //    .Where(x => x.Data.Year == 2025)
        //    .ToListAsync();

        //var categoriasList = new List<CategoriaModel>();

        //foreach (var controleCategoria in controlesCategoria)
        //{
        //    var financasPorCategoria = financas.Where(x => x.CategoriaId == controleCategoria.CategoriaId);

        //    var valorJaneiro = financasPorCategoria.Where(x => x.Data.Month == 1).Sum(y => y.Valor);
        //    var valorFevereiro = financasPorCategoria.Where(x => x.Data.Month == 2).Sum(y => y.Valor);
        //    var valorMarco = financasPorCategoria.Where(x => x.Data.Month == 3).Sum(y => y.Valor);
        //    var valorAbril = financasPorCategoria.Where(x => x.Data.Month == 4).Sum(y => y.Valor);
        //    var valorMaio = financasPorCategoria.Where(x => x.Data.Month == 5).Sum(y => y.Valor);
        //    var valorJunho = financasPorCategoria.Where(x => x.Data.Month == 6).Sum(y => y.Valor);
        //    var valorJulho = financasPorCategoria.Where(x => x.Data.Month == 7).Sum(y => y.Valor);
        //    var valorAgosto = financasPorCategoria.Where(x => x.Data.Month == 8).Sum(y => y.Valor);
        //    var valorSetembro = financasPorCategoria.Where(x => x.Data.Month == 9).Sum(y => y.Valor);
        //    var valorOutubro = financasPorCategoria.Where(x => x.Data.Month == 10).Sum(y => y.Valor);
        //    var valorNovembro = financasPorCategoria.Where(x => x.Data.Month == 11).Sum(y => y.Valor);
        //    var valorDezembro = financasPorCategoria.Where(x => x.Data.Month == 12).Sum(y => y.Valor);

        //    var item = new CategoriaModel
        //    {
        //        Nome = controleCategoria.Categoria.Nome,
        //        //ValorMetaAnual = categoria.ValorMetaAnual,
        //        //ValorMeta = controleCategoria.ValorMeta,
        //        ValorJaneiro = (valorJaneiro > controleCategoria.ValorMeta ? valorJaneiro : controleCategoria.ValorMeta),
        //        ValorFevereiro = (valorFevereiro > controleCategoria.ValorMeta ? valorFevereiro : controleCategoria.ValorMeta),
        //        ValorMarco = (valorMarco > controleCategoria.ValorMeta ? valorMarco : controleCategoria.ValorMeta),
        //        ValorAbril = (valorAbril > controleCategoria.ValorMeta ? valorAbril : controleCategoria.ValorMeta),
        //        ValorMaio = (valorMaio > controleCategoria.ValorMeta ? valorMaio : controleCategoria.ValorMeta),
        //        ValorJunho = (valorJunho > controleCategoria.ValorMeta ? valorJunho : controleCategoria.ValorMeta),
        //        ValorJulho = (valorJulho > controleCategoria.ValorMeta ? valorJulho : controleCategoria.ValorMeta),
        //        ValorAgosto = (valorAgosto > controleCategoria.ValorMeta ? valorAgosto : controleCategoria.ValorMeta),
        //        ValorSetembro = (valorSetembro > controleCategoria.ValorMeta ? valorSetembro : controleCategoria.ValorMeta),
        //        ValorOutubro = (valorOutubro > controleCategoria.ValorMeta ? valorOutubro : controleCategoria.ValorMeta),
        //        ValorNovembro = (valorNovembro > controleCategoria.ValorMeta ? valorNovembro : controleCategoria.ValorMeta),
        //        ValorDezembro = (valorDezembro > controleCategoria.ValorMeta ? valorDezembro : controleCategoria.ValorMeta),
        //        Financas = new ObservableCollection<FinancaPorCategoriaModel>(financasPorCategoria.Select(x => new FinancaPorCategoriaModel
        //        {
        //            Id = x.Id,
        //            Nome = x.Descricao,
        //            Valor = x.Valor,
        //            TipoId = x.TipoId
        //        }).ToList())
        //    };

        //    categoriasList.Add(item);
        //}

        _categoriasCollection = new ObservableCollection<ApuracaoCategoriaModel>(despesas);

        _categoriasCollection.CollectionChanged += Contas_CollectionChanged;

        _categoriasViewSource.Source = _categoriasCollection;

        Cursor = null;
    }

    private List<ApuracaoCategoriaModel> MapFrom(IEnumerable<ApuracaoCategoria> apuracoesMensaisCategoria)
    {
        var apuracaoAnualCategoriaList = new List<ApuracaoCategoriaModel>();

        var apuracoesCategoria = apuracoesMensaisCategoria
            .GroupBy(x => new
            {
                x.CategoriaId,
                CategoriaNome = x.Categoria.Nome,
                x.Categoria.AplicaReceita,
                x.Categoria.AplicaDespesa,
                x.Categoria.Ordem,
                x.Categoria.IconId,
                x.Categoria.Icon.FaUnicode
            });

        foreach (var apuracaoCategoria in apuracoesCategoria)
        {
            //var financasPorCategoria = apuracaoCategoria.Financas
            //    //.OrderBy(x => x.Competencia)
            //    .Where(x => x.CategoriaId == apuracaoCategoria.CategoriaId);

            var valorJaneiro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 1).Sum(y => y.Valor);
            var valorFevereiro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 2).Sum(y => y.Valor);
            var valorMarco = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 3).Sum(y => y.Valor);
            var valorAbril = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 4).Sum(y => y.Valor);
            var valorMaio = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 5).Sum(y => y.Valor);
            var valorJunho = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 6).Sum(y => y.Valor);
            var valorJulho = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 7).Sum(y => y.Valor);
            var valorAgosto = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 8).Sum(y => y.Valor);
            var valorSetembro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 9).Sum(y => y.Valor);
            var valorOutubro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 10).Sum(y => y.Valor);
            var valorNovembro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 11).Sum(y => y.Valor);
            var valorDezembro = apuracoesMensaisCategoria.Where(x => x.CategoriaId == apuracaoCategoria.Key.CategoriaId && x.Competencia.Month == 12).Sum(y => y.Valor);

            var valorTotal = valorJaneiro + valorFevereiro + valorMarco + valorAbril + valorMaio + valorJunho + valorJulho + valorAgosto + valorSetembro + valorOutubro + valorNovembro + valorDezembro;

            //var valorJaneiro = apuracoesCategoria.Where(x => x.Competencia.Month == 1 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorFevereiro = apuracoesCategoria.Where(x => x.Competencia.Month == 2 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorMarco = apuracoesCategoria.Where(x => x.Competencia.Month == 3 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorAbril = apuracoesCategoria.Where(x => x.Competencia.Month == 4 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorMaio = apuracoesCategoria.Where(x => x.Competencia.Month == 5 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorJunho = apuracoesCategoria.Where(x => x.Competencia.Month == 6 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorJulho = apuracoesCategoria.Where(x => x.Competencia.Month == 7 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorAgosto = apuracoesCategoria.Where(x => x.Competencia.Month == 8 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorSetembro = apuracoesCategoria.Where(x => x.Competencia.Month == 9 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorOutubro = apuracoesCategoria.Where(x => x.Competencia.Month == 10 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorNovembro = apuracoesCategoria.Where(x => x.Competencia.Month == 11 && !x.EhPrevisao).Sum(y => y.Valor);
            //var valorDezembro = apuracoesCategoria.Where(x => x.Competencia.Month == 12 && !x.EhPrevisao).Sum(y => y.Valor);

            //var valorPrevistoJaneiro = apuracoesCategoria.Where(x => x.Competencia.Month == 1 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoFevereiro = apuracoesCategoria.Where(x => x.Competencia.Month == 2 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoMarco = apuracoesCategoria.Where(x => x.Competencia.Month == 3 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoAbril = apuracoesCategoria.Where(x => x.Competencia.Month == 4 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoMaio = apuracoesCategoria.Where(x => x.Competencia.Month == 5 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoJunho = apuracoesCategoria.Where(x => x.Competencia.Month == 6 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoJulho = apuracoesCategoria.Where(x => x.Competencia.Month == 7 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoAgosto = apuracoesCategoria.Where(x => x.Competencia.Month == 8 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoSetembro = apuracoesCategoria.Where(x => x.Competencia.Month == 9 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoOutubro = apuracoesCategoria.Where(x => x.Competencia.Month == 10 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoNovembro = apuracoesCategoria.Where(x => x.Competencia.Month == 11 && x.EhPrevisao).Sum(y => y.Valor);
            //var valorPrevistoDezembro = apuracoesCategoria.Where(x => x.Competencia.Month == 12 && x.EhPrevisao).Sum(y => y.Valor);

            //var valorTotal = apuracoesCategoria.Sum(y => y.Valor);

            var valorTotalAbsoluto = Math.Abs(valorTotal);

            if (Math.Abs(valorTotalAbsoluto) > 0 || ExibirTodasCategorias)
            {
                //var valorMeta = controleCategoria.Sum(y => y.ValorMeta);

                //var calculaValor = (decimal valor, decimal valorPrevisto) =>
                //{
                //    var valorAbsoluto = Math.Abs(valor);
                //    var valorPrevistoAbsoluto = Math.Abs(valorPrevisto);

                //    if (valorAbsoluto > valorPrevistoAbsoluto)
                //    {
                //        return valor;
                //    }
                //    else
                //    {
                //        return valorPrevisto;
                //    }
                //};

                var item = new ApuracaoCategoriaModel
                {
                    Nome = apuracaoCategoria.Key.CategoriaNome,
                    CategoriaId = apuracaoCategoria.Key.CategoriaId,
                    CategoriaNome = apuracaoCategoria.Key.CategoriaNome,
                    AplicaReceita = apuracaoCategoria.Key.AplicaReceita,
                    AplicaDespesa = apuracaoCategoria.Key.AplicaDespesa,
                    Ordem = apuracaoCategoria.Key.Ordem.Value,
                    IconId = apuracaoCategoria.Key.IconId,
                    IconFaUnicode = apuracaoCategoria.Key.FaUnicode,
                    //ValorPrevistoAnual = categoria.ValorPrevistoAnual,
                    //ValorPrevistoMensal = categoria.ValorPrevistoMensal,
                    //ValorJaneiro = calculaValor(valorJaneiro, valorPrevistoJaneiro),
                    //ValorFevereiro = calculaValor(valorFevereiro, valorPrevistoFevereiro),
                    //ValorMarco = calculaValor(valorMarco, valorPrevistoMarco),
                    //ValorAbril = calculaValor(valorAbril, valorPrevistoAbril),
                    //ValorMaio = calculaValor(valorMaio, valorPrevistoMaio),
                    //ValorJunho = calculaValor(valorJunho, valorPrevistoJunho),
                    //ValorJulho = calculaValor(valorJulho, valorPrevistoJulho),
                    //ValorAgosto = calculaValor(valorAgosto, valorPrevistoAgosto),
                    //ValorSetembro = calculaValor(valorSetembro, valorPrevistoSetembro),
                    //ValorOutubro = calculaValor(valorOutubro, valorPrevistoOutubro),
                    //ValorNovembro = calculaValor(valorNovembro, valorPrevistoOutubro),
                    //ValorDezembro = calculaValor(valorDezembro, valorPrevistoDezembro),
                    ValorJaneiro = valorJaneiro,
                    ValorFevereiro = valorFevereiro,
                    ValorMarco = valorMarco,
                    ValorAbril = valorAbril,
                    ValorMaio = valorMaio,
                    ValorJunho = valorJunho,
                    ValorJulho = valorJulho,
                    ValorAgosto = valorAgosto,
                    ValorSetembro = valorSetembro,
                    ValorOutubro = valorOutubro,
                    ValorNovembro = valorNovembro,
                    ValorDezembro = valorDezembro,
                    //ValorJaneiro = valorPrevistoJaneiro,
                    //ValorFevereiro = valorPrevistoFevereiro,
                    //ValorMarco = valorPrevistoMarco,
                    //ValorAbril = valorPrevistoAbril,
                    //ValorMaio = valorPrevistoMaio,
                    //ValorJunho = valorPrevistoJunho,
                    //ValorJulho = valorPrevistoJulho,
                    //ValorAgosto = valorPrevistoAgosto,
                    //ValorSetembro = valorPrevistoSetembro,
                    //ValorOutubro = valorPrevistoOutubro,
                    //ValorNovembro = valorPrevistoNovembro,
                    //ValorDezembro = valorPrevistoDezembro,
                    ValorTotal = valorTotal,
                    Financas = new ObservableCollection<FinancaPorCategoriaModel>(apuracaoCategoria.SelectMany(y => y.Financas.Select(x => new FinancaPorCategoriaModel
                    {
                        FinancaId = x.Id,
                        TipoFinancaId = x.TipoFinancaId,
                        ContaTipoId = x.Conta.TipoContaId,
                        ContaNome = x.Conta.Nome,
                        Competencia = x.Competencia,
                        Data = x.Data,
                        Descricao = x.Descricao,
                        Valor = x.Valor,
                        ValorOriginal = (x as PrevisaoInteligente)?.ValorOriginal,
                        ValorExcedente = (x as PrevisaoInteligente)?.ValorExcedente,
                        EhPrevisao = x.EhPrevisao
                    })).ToList())
                };

                apuracaoAnualCategoriaList.Add(item);
            }
        }

        return apuracaoAnualCategoriaList;
    }

    private void Contas_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var model = e.NewItems[0] as ApuracaoCategoriaModel;
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

    private void novaCategoriaButton_Click(object sender, RoutedEventArgs e)
    {
        var model = new ApuracaoCategoriaModel
        {
            CategoriaId = "",
            CategoriaNome = "",
            IconId = "",
            IconFaUnicode = "",
            //CreationDate = DateTime.Now,
        };

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

        _categoriasCollection.Add(model);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }

    private void resumoRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ModoVisualizacao = ModoVisualizacaoEnum.Tabela;

        Buscar();
    }

    private void detalheRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ModoVisualizacao = ModoVisualizacaoEnum.Lista;

        Buscar();
    }

    private void padraoRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        Ordem = OrdemEnum.Padrao;

        Buscar();
    }

    private void decrescenteRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        Ordem = OrdemEnum.Decrescente;

        Buscar();
    }

    private void competenciaRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        RegimeApuracao = RegimeApuracaoEnum.Competencia;

        Buscar();
    }

    private void caixaRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        RegimeApuracao = RegimeApuracaoEnum.Caixa;

        Buscar();
    }
}

