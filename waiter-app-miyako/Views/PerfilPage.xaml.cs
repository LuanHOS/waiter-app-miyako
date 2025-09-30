using waiter_app_miyako.ViewModels;

namespace waiter_app_miyako.Views;

public partial class PerfilPage : ContentPage
{
    private readonly PerfilViewModel _viewModel;

    public PerfilPage()
    {
        InitializeComponent();
        _viewModel = new PerfilViewModel();
        this.BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CarregarFuncionario();
    }
}