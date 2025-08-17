using Microsoft.Maui.Controls.Shapes;

namespace waiter_app_miyako.Views;

public partial class PedidosPage : ContentPage
{
    public PedidosPage()
    {
        InitializeComponent();
        CarregarPedidosProvisorios();
    }

    private void CarregarPedidosProvisorios()
    {
        // Dados provisórios para pedidos EM ANDAMENTO
        var pedidosEmAndamento = new List<Tuple<string, string, string>>
        {
            new("05", "15:00", "x3"),
            new("05", "15:00", "x3"),
            new("03", "14:18", "x5"),
            new("08", "17:25", "x2")
        };

        // Dados provisórios para pedidos FINALIZADOS
        var pedidosFinalizados = new List<Tuple<string, string, string>>
        {
            new("01", "10/05/2025", "x4"),
            new("02", "10/05/2025", "x7"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),
            new("09", "10/05/2025", "x3"),


        };

        // Popula a lista de pedidos em andamento
        foreach (var pedido in pedidosEmAndamento)
        {
            EmAndamentoList.Add(CriarLinhaPedido(pedido.Item1, pedido.Item2, pedido.Item3));
        }

        // Popula a lista de pedidos finalizados
        foreach (var pedido in pedidosFinalizados)
        {
            FinalizadosList.Add(CriarLinhaPedido(pedido.Item1, pedido.Item2, pedido.Item3));
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

        // Adiciona o gesto de toque para tornar a linha clicável
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnPedidoTapped;
        border.GestureRecognizers.Add(tapGesture);

        return border;
    }

    private async void OnPedidoTapped(object sender, TappedEventArgs e)
    {
        // Lógica futura para navegar para os detalhes do pedido clicado
        await DisplayAlert("Detalhes do Pedido", "Navegando para os detalhes do pedido...", "OK");
    }
}