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
            if (!item.IsEntregue)
            {
                item.IsSelecionado = !item.IsSelecionado;
            }
        }
    }

    private async void MarcarEntregue_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar Entrega",
            "Deseja marcar os itens selecionados como entregues? Esta ação não pode ser desfeita.",
            "Sim", "Não");

        if (confirmar)
        {
            // Lógica para marcar itens como entregues
        }
    }

    // Novo método para o pop-up de fechar a conta
    private async void FecharConta_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Fechar Conta",
            "Deseja marcar todos os itens como entregues e fechar a conta? Esta ação não pode ser desfeita.",
            "Sim", "Não");

        if (confirmar)
        {
            // Lógica para fechar a conta
        }
    }
}