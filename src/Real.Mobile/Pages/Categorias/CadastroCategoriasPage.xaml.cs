using Real.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Real.Models;

namespace Real.Pages.Categorias;

public partial class CadastroCategoriasPage : ContentPage
{
    private readonly RealDbContext _db;

    public ICommand CreateNewCommand { get; set; }

    public ICommand RefreshCommand { get; set; }

    public ObservableCollection<Categoria> Categorias { get; set; }

    public CadastroCategoriasPage(RealDbContext db)
    {
        InitializeComponent();

        _db = db;

        Categorias = _db.Categorias.Local.ToObservableCollection();

        searchHandler.Categorias = Categorias;

        CreateNewCommand = new Command(CreateNew);

        RefreshCommand = new Command(OnRefresh);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (Categorias.Count == 0)
        {
            //refreshView.IsRefreshing = true;
        }

        //await _db.Categorias
        //    .LoadAsync();

        //Categorias = _db.Categorias.Local.ToObservableCollection();
    }

    private async void CreateNew()
    {
        await Shell.Current.GoToAsync($"Categoria");
    }

    private void OnRefresh()
    {

    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var id = (e.SelectedItem as Categoria).Id;

        var categoriaId = id;

        //var categoria = await _db.Categorias.FirstOrDefaultAsync(x => x.Id == id);

        await Shell.Current.GoToAsync("Categoria", new Dictionary<string, object> { { "CategoriaId", categoriaId } });
    }
}