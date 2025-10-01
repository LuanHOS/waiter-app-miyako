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
            // Permite alterar a sele��o apenas se o item n�o estiver entregue OU cancelado
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
            "Deseja marcar os itens selecionados como entregues? Esta a��o n�o pode ser desfeita.",
            "Sim", "N�o");

        if (confirmar)
        {
            // L�gica para marcar itens como entregues
        }
    }

    private async void FecharConta_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Fechar Conta",
            "Deseja marcar todos os itens como entregues e fechar a conta? Esta a��o n�o pode ser desfeita.",
            "Sim", "N�o");

        if (confirmar)
        {
            // L�gica para fechar a conta
        }
    }

    // Novo m�todo para o bot�o de cancelar
    private async void MarcarCancelado_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar Cancelamento",
            "Deseja marcar os itens selecionados como Cancelados? Esta a��o � irrevers�vel.",
            "Sim", "N�o");

        if (confirmar)
        {
            // L�gica para marcar itens como cancelados
        }
    }
}