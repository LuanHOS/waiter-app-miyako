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
        // Valor provisório, já que a API não existe ainda.
        int numeroDeMesas = 30;

        for (int i = 1; i <= numeroDeMesas; i++)
        {
            var button = new Button
            {
                Text = i.ToString(),
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                HeightRequest = 90, 
                WidthRequest = 90, 
                CornerRadius = 10
            };

            // Define a cor de fundo
            button.SetAppThemeColor(Button.BackgroundColorProperty,
                (Color)Application.Current.Resources["Gray200"],
                (Color)Application.Current.Resources["Gray600"]);

            // Define a cor do texto
            button.SetAppThemeColor(Button.TextColorProperty,
               (Color)Application.Current.Resources["Black"],
               (Color)Application.Current.Resources["White"]);


            // Calcula a posição do botão no grid
            int row = (i - 1) / 3;
            int column = (i - 1) % 3;

            // Adiciona uma nova linha ao grid quando necessário
            if (column == 0)
            {
                MesasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            // Define a linha e a coluna do botão
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);

            // Adiciona o botão ao grid
            MesasGrid.Children.Add(button);
        }
    }
}