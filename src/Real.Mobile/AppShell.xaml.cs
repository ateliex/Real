using Real.Pages.Categorias;
using Real.Pages.Contas;
using System.Diagnostics;

namespace Real;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Contas/Conta", typeof(ContaPage));
        Routing.RegisterRoute("Categorias/Categoria", typeof(CategoriaPage));
        Routing.RegisterRoute("Categorias/Cadastro", typeof(CadastroCategoriasPage));
        Routing.RegisterRoute("Categorias/ApuracaoCategoria", typeof(ApuracaoCategoriaPage));
    }

    private void SynchMenuItem_Clicked(object sender, EventArgs e)
    {

    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    SetFlyoutBehavior();
    //    DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
    //}

    //private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    //{
    //    SetFlyoutBehavior();
    //}

    //protected override void OnDisappearing()
    //{
    //    base.OnDisappearing();
    //    DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
    //}

    //private void SetFlyoutBehavior()
    //{
    //    // Get the screen points 
    //    double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

    //    Debug.WriteLine(screenWidth);
    //    // sizes obtained from the official bootstrap CSS 
    //    switch (screenWidth)
    //    {
    //        case <= 576:
    //            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
    //            Shell.Current.FlyoutIsPresented = false;
    //            break;
    //        case > 576:
    //            Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
    //            Shell.Current.FlyoutIsPresented = true;
    //            break;
    //    }
    //}
}
