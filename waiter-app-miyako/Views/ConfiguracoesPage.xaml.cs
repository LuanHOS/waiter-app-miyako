namespace waiter_app_miyako.Views;

public partial class ConfiguracoesPage : ContentPage
{
    public ConfiguracoesPage()
    {
        InitializeComponent();
    }

    private async void OnDeslogarClicked(object sender, EventArgs e)
    {
        // Exibe um pop-up de alerta para confirmação do usuário
        bool querDeslogar = await DisplayAlert(
            "Confirmar Logout",
            "Você tem certeza que deseja deslogar da conta atual?",
            "SIM",
            "NÃO");

        // Se o usuário clicou em "SIM" (que retorna true)
        if (querDeslogar)
        {
            // Redireciona o usuário de volta para a tela de Login
            Application.Current.MainPage = new LoginPage();
        }
    }
}
