using waiter_app_miyako.Models;
using waiter_app_miyako.ViewModels;

namespace waiter_app_miyako.Views;

public partial class PedidoDetalhesPage : ContentPage, IQueryAttributable
{
    private PedidoDetalhesViewModel _viewModel;

    public PedidoDetalhesPage()
    {
        InitializeComponent();
        _viewModel = new PedidoDetalhesViewModel();
        BindingContext = _viewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Console.WriteLine("========== PARÂMETROS RECEBIDOS ==========");
        foreach (var kv in query)
            Console.WriteLine($"🔹 {kv.Key} = {kv.Value}");
        Console.WriteLine("=========================================");

        // Recebe o número da mesa
        if (query.TryGetValue("MesaNumero", out var mesaParam)
            && int.TryParse(mesaParam?.ToString(), out int numeroMesa))
        {
            Console.WriteLine($"✅ Mesa recebida via parâmetro: {numeroMesa}");
            await _viewModel.CarregarPedidoDaMesaAsync(numeroMesa);
        }
        else
        {
            Console.WriteLine("⚠️ Nenhum parâmetro MesaNumero recebido ou inválido!");
        }

        // Recebe o pedido
        if (query.TryGetValue("Pedido", out var pedidoObj) && pedidoObj is Pedidos pedido)
        {
            Console.WriteLine("✅ Pedido recebido via parâmetro");
            _viewModel.Pedido = pedido;
        }

        // Força atualização visual
        Dispatcher.Dispatch(() =>
        {
            BindingContext = null;
            BindingContext = _viewModel;
        });
    }

    private void OnItemTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is ItemPedidoDetalheViewModel item)
        {
            if (!item.IsEntregue && !item.IsCancelado)
                item.IsSelecionado = !item.IsSelecionado;
        }
    }

    private async void MarcarEntregue_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmar Entrega",
            "Deseja marcar os itens selecionados como entregues? Esta ação não pode ser desfeita.",
            "Sim", "Não");
        if (confirmar)
        {
            // lógica
        }
    }

    private async void FecharConta_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Fechar Conta",
            "Deseja marcar todos os itens como entregues e fechar a conta? Esta ação não pode ser desfeita.",
            "Sim", "Não");
        if (confirmar)
        {
            // lógica
        }
    }

    private async void MarcarCancelado_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmar Cancelamento",
            "Deseja marcar os itens selecionados como cancelados? Esta ação é irreversível.",
            "Sim", "Não");
        if (confirmar)
        {
            // lógica
        }
    }
}
