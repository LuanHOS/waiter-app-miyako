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

    private void OnItemTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is ItemPedidoDetalheViewModel item)
        {
            // Inverte o estado de sele��o do item clicado
            item.IsSelecionado = !item.IsSelecionado;
        }
    }

    private async void MarcarEntregue_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar Entrega",
            "Deseja marcar os itens selecionados como entregues? Esta a��o n�o pode ser desfeita.",
            "Sim", "N�o");

        if (confirmar)
        {
            // A l�gica para atualizar o status dos itens no backend viria aqui
        }
    }
}