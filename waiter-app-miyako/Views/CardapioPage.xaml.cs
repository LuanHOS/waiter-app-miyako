namespace waiter_app_miyako.Views;

public partial class CardapioPage : ContentPage
{
    public CardapioPage()
    {
        InitializeComponent();
    }

    private async void OnNavegarParaSessaoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var targetElement = button.CommandParameter as VisualElement;
        if (targetElement == null) return;

        await MenuScrollView.ScrollToAsync(targetElement, ScrollToPosition.Start, true);
    }

    private async void OnItemTapped(object sender, TappedEventArgs e)
    {
        // Agora, em vez de um alerta, navegamos para a página de detalhes.
        // Futuramente, você poderá passar os dados do item clicado para o construtor da ItemDetalhesPage.
        await Navigation.PushAsync(new ItemDetalhesPage());
    }
}