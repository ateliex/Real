using Real.Data;
using System.Windows.Input;
using Real.Models;

namespace Real.Pages.Categorias;

public partial class CategoriaPage : ContentPage, IQueryAttributable
{
    private readonly RealDbContext _db;

    private bool _isNew;

    public ICommand SaveCommand { get; set; }

    public ICommand DeleteCommand { get; set; }

    public string CategoriaId { get; set; }

    public Categoria Categoria { get; private set; }

    public CategoriaPage(RealDbContext db)
    {
        InitializeComponent();

        _db = db;

        Categoria = new Categoria
        {

        };

        SaveCommand = new Command(Save);

        DeleteCommand = new Command(Delete);

        BindingContext = this;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ThreadPool.QueueUserWorkItem(async (state) =>
        {
            if (query.ContainsKey(nameof(CategoriaId)))
            {
                CategoriaId = query[nameof(CategoriaId)].ToString();

                OnPropertyChanged(nameof(CategoriaId));

                var categoria = await _db.Categorias.FindAsync(Categoria.Id);

                Categoria = categoria;

                OnPropertyChanged(nameof(Categoria));

                _isNew = false;
            }
            else
            {
                var categoria = new Categoria();

                Categoria = categoria;

                OnPropertyChanged(nameof(Categoria));

                _isNew = true;
            }
        });
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }

    private async void Save()
    {
        try
        {
            if (_isNew)
            {
                _db.Categorias.Add(Categoria);
            }

            await _db.SaveChangesAsync();

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception _)
        {
            throw;
        }
    }

    private async void Delete()
    {
        var yes = await DisplayAlert("Excluir Categoria", "Tem certeza que deseja excluir isso?", "Sim", "Não");

        if (yes)
        {
            try
            {
                var categoria = await _db.Categorias.FindAsync(Categoria.Id);

                if (categoria != null)
                {
                    _db.Categorias.Remove(categoria);

                    await _db.SaveChangesAsync();
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception _)
            {
                throw;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}