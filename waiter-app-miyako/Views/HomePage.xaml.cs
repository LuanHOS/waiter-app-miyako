using waiter_app_miyako.ViewModels;
using waiter_app_miyako.Views; // Adicionar esta using para ter acesso a CardapioPage

namespace waiter_app_miyako.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly HomePageViewModel _viewModel;

        private readonly PedidoService _pedidoService = new PedidoService();
        public HomePage()
        {
            InitializeComponent();
            _viewModel = new HomePageViewModel();
            this.BindingContext = _viewModel;
            Routing.RegisterRoute(nameof(CardapioPage), typeof(CardapioPage));
            Routing.RegisterRoute(nameof(PedidoDetalhesPage), typeof(PedidoDetalhesPage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CarregarMesasAsync(); // Certifique-se que o nome do método esteja assim
        }
        private async void OnMesaClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is MesaViewModel mesa)
            {
                try
                {
                    btn.IsEnabled = false; // Evita duplo clique

                    if (mesa.Status == "Livre")
                    {
                        await Shell.Current.GoToAsync($"CardapioPage?MesaNumero={mesa.Numero}");
                    }
                    else if (mesa.Status == "ComPedidoPendente")
                    {
                        var pedido = await _pedidoService.ObterPedidoPorNumeroMesaAsync(mesa.Numero);
                        if (pedido != null)
                        {
                            var navParams = new Dictionary<string, object>
        {
            { "Pedido", pedido },
            { "MesaNumero", mesa.Numero } // ✅ ESSA LINHA FAZ A DIFERENÇA
        };

                            await Shell.Current.GoToAsync(nameof(PedidoDetalhesPage), navParams);
                        }
                        else
                        {
                            await DisplayAlert("Erro", "Não foi possível carregar o pedido.", "OK");
                        }
                    }
                }
                finally
                {
                    btn.IsEnabled = true;
                }
            }
        }
    }
}