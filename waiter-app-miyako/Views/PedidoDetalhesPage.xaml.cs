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
            // Permite alterar a seleção apenas se o item não estiver entregue OU cancelado
            if (!item.IsEntregue && !item.IsCancelado)
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

    // Novo método para o botão de cancelar
    private async void MarcarCancelado_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar Cancelamento",
            "Deseja marcar os itens selecionados como Cancelados? Esta ação é irreversível.",
            "Sim", "Não");

        if (confirmar)
        {
            // Lógica para marcar itens como cancelados
        }
    }
}