using Microsoft.Maui.Controls.Shapes;
using System.Linq;
using System.Threading.Tasks;
using waiter_app_miyako.Services;
using System;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.Views;

public partial class PedidosPage : ContentPage
{
    private readonly MockApiService _apiService;

    public PedidosPage()
    {
        InitializeComponent();
        _apiService = new MockApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarPedidos();
    }

    private async Task CarregarPedidos()
    {
        var todosPedidosDaApi = await _apiService.FetchPedidos();

        var hoje = DateTime.Today;
        var pedidosDeHoje = todosPedidosDaApi.Where(p => p.dataPedido.HasValue && p.dataPedido.Value.Date == hoje);

        var pedidosEmAndamento = pedidosDeHoje.Where(p => p.itens != null && p.itens.Any(i => i.Status == "aberto"));
        var pedidosFinalizados = pedidosDeHoje.Where(p => p.itens != null && p.itens.All(i => i.Status == "entregue"));

        while (EmAndamentoList.Children.Count > 1)
        {
            EmAndamentoList.Children.RemoveAt(1);
        }
        while (FinalizadosList.Children.Count > 1)
        {
            FinalizadosList.Children.RemoveAt(1);
        }

        foreach (var pedido in pedidosEmAndamento)
        {
            var mesaNome = $"{pedido.mesaNumero:00}";
            var horario = pedido.dataPedido?.ToString("HH:mm") ?? "N/A";
            var quantidade = $"x{pedido.itens?.Sum(i => i.quantidade) ?? 0}";
            EmAndamentoList.Add(CriarLinhaPedido(mesaNome, horario, quantidade));
        }

        foreach (var pedido in pedidosFinalizados)
        {
            var mesaNome = $"{pedido.mesaNumero:00}";
            var horario = pedido.dataPedido?.ToString("HH:mm") ?? "N/A";
            var quantidade = $"x{pedido.itens?.Sum(i => i.quantidade) ?? 0}";
            FinalizadosList.Add(CriarLinhaPedido(mesaNome, horario, quantidade));
        }
    }

    private Border CriarLinhaPedido(string coluna1, string coluna2, string coluna3)
    {
        var linhaGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            },
            Padding = new Thickness(10, 15)
        };

        linhaGrid.Add(new Label { Text = coluna1, VerticalOptions = LayoutOptions.Center }, 0);
        linhaGrid.Add(new Label { Text = coluna2, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }, 1);
        linhaGrid.Add(new Label { Text = coluna3, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }, 2);
        linhaGrid.Add(new Image { Source = "arrow_right_icon.png", HeightRequest = 15, WidthRequest = 15, VerticalOptions = LayoutOptions.Center }, 3);

        var border = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 8 },
            Content = linhaGrid
        };

        border.SetAppThemeColor(Border.StrokeProperty,
            (Color)Application.Current.Resources["Gray300"],
            (Color)Application.Current.Resources["Gray500"]);

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnPedidoTapped;
        border.GestureRecognizers.Add(tapGesture);

        return border;
    }

    private async void OnPedidoTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Detalhes do Pedido", "Ainda não criado", "OK");
    }
}