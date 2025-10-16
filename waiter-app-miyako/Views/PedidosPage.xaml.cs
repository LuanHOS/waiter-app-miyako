using Microsoft.Maui.Controls.Shapes;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services;

namespace waiter_app_miyako.Views;

public partial class PedidosPage : ContentPage
{
    private readonly PedidoService _pedidoService;
    private List<Pedidos> _pedidosDeHoje;

    public PedidosPage()
    {
        InitializeComponent();
        _pedidoService = new PedidoService();
        _pedidosDeHoje = new List<Pedidos>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarPedidos();
    }

    private async Task CarregarPedidos()
    {
        var todosPedidosDaApi = await _pedidoService.ObterPedidos();

        //var hoje = DateTime.Today;
        //_pedidosDeHoje = todosPedidosDaApi.Where(p => p.dataAberturaPedido.HasValue && p.dataAberturaPedido.Value.Date == hoje).ToList();

        //var pedidosEmAndamento = _pedidosDeHoje.Where(p => p.finalizado == false);
        //var pedidosFinalizados = _pedidosDeHoje.Where(p => p.finalizado == true);

        //EmAndamentoList.Children.Clear();
        //FinalizadosList.Children.Clear();

        //EmAndamentoList.Add(CriarCabecalho());
        //FinalizadosList.Add(CriarCabecalho());

        //foreach (var pedido in pedidosEmAndamento)
        //{
        //    EmAndamentoList.Add(CriarLinhaPedido(pedido));
        //}

        //foreach (var pedido in pedidosFinalizados)
        //{
        //    FinalizadosList.Add(CriarLinhaPedido(pedido));
        //}
    }

    private Grid CriarCabecalho()
    {
        var cabecalhoGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            },
            Padding = new Thickness(10, 5)
        };

        cabecalhoGrid.Add(new Label { Text = "MESA", FontAttributes = FontAttributes.Bold }, 0);
        cabecalhoGrid.Add(new Label { Text = "HORÁRIO", FontAttributes = FontAttributes.Bold, HorizontalTextAlignment = TextAlignment.Center }, 1);
        cabecalhoGrid.Add(new Label { Text = "QUANT.", FontAttributes = FontAttributes.Bold, HorizontalTextAlignment = TextAlignment.Center }, 2);

        return cabecalhoGrid;
    }

    private Border CriarLinhaPedido(Pedidos pedido)
    {
        var linhaGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            },
            Padding = new Thickness(10, 15)
        };

        linhaGrid.Add(new Label { Text = $"{pedido.mesaNumero:00}", VerticalOptions = LayoutOptions.Center }, 0);
        linhaGrid.Add(new Label { Text = pedido.dataAberturaPedido?.ToString("HH:mm") ?? "N/A", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }, 1);
        linhaGrid.Add(new Label { Text = $"x{pedido.itens?.Sum(i => i.quantidade) ?? 0}", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }, 2);
        linhaGrid.Add(new Image { Source = "arrow_right_icon.png", HeightRequest = 15, WidthRequest = 15, VerticalOptions = LayoutOptions.Center }, 3);

        var border = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 8 },
            Content = linhaGrid,
            BindingContext = pedido
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
        if (sender is Border tappedBorder && tappedBorder.BindingContext is Pedidos pedidoSelecionado)
        {
            await Shell.Current.GoToAsync(nameof(PedidoDetalhesPage), new Dictionary<string, object>
            {
                { "Pedido", pedidoSelecionado }
            });
        }
    }
}