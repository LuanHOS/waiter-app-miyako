namespace waiter_app_miyako.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        CarregarMesas();
    }

    private void CarregarMesas()
    {
        // Valor provis�rio, j� que a API n�o existe ainda.
        int numeroDeMesas = 20;

        for (int i = 1; i <= numeroDeMesas; i++)
        {
            var button = new Button
            {
                Text = i.ToString(),
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                HeightRequest = 110, // Altura para parecer um "cubo"
                WidthRequest = 110,  // Largura para parecer um "cubo"
                CornerRadius = 10
            };

            // Define a cor de fundo com base nos recursos de tema do App
            button.SetAppThemeColor(Button.BackgroundColorProperty,
                (Color)Application.Current.Resources["Gray200"],
                (Color)Application.Current.Resources["Gray600"]);

            // Define a cor do texto com base nos recursos de tema do App
            button.SetAppThemeColor(Button.TextColorProperty,
               (Color)Application.Current.Resources["Black"],
               (Color)Application.Current.Resources["White"]);


            // Calcula a posi��o do bot�o no grid
            int row = (i - 1) / 3;
            int column = (i - 1) % 3;

            // Adiciona uma nova linha ao grid quando necess�rio
            if (column == 0)
            {
                MesasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            // Define a linha e a coluna do bot�o
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);

            // Adiciona o bot�o ao grid
            MesasGrid.Children.Add(button);
        }
    }
}