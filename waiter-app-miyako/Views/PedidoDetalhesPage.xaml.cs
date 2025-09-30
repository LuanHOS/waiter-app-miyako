using waiter_app_miyako.Models;
using waiter_app_miyako.ViewModels;

namespace waiter_app_miyako.Views;

[QueryProperty(nameof(Pedido), "Pedido")]
public partial class PedidoDetalhesPage : ContentPage
{
    public Pedidos Pedido
    {
        set
        {
            var viewModel = (PedidoDetalhesViewModel)BindingContext;
            viewModel.Pedido = value;
        }
    }

    public PedidoDetalhesPage()
    {
        InitializeComponent();
        BindingContext = new PedidoDetalhesViewModel();
    }
}