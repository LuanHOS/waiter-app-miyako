using waiter_app_miyako.ViewModels;
using waiter_app_miyako.Views; // Adicionar esta using para ter acesso a CardapioPage

namespace waiter_app_miyako.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly HomePageViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();
            _viewModel = new HomePageViewModel();
            this.BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CarregarMesas();
        }

        private async void OnMesaClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is MesaViewModel mesa)
            {
                if (mesa.Status == "reservada")
                {
                    bool abrirPedido = await DisplayAlert(
                        "Mesa Reservada",
                        "Esta mesa está Reservada! Deseja abrir um pedido para ela?",
                        "Sim", "Não");

                    if (abrirPedido)
                    {

                    }
                }
                // Adicione aqui a lógica para outros status de mesa se necessário
            }
        }
    }
}