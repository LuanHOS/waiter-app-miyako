namespace waiter_app_miyako.Views;

public partial class ConfiguracoesPage : ContentPage
{
    public ConfiguracoesPage()
    {
        InitializeComponent();
    }

    private async void OnDeslogarClicked(object sender, EventArgs e)
    {
        // Exibe um pop-up de alerta para confirma��o do usu�rio
        bool querDeslogar = await DisplayAlert(
            "Confirmar Logout",
            "Voc� tem certeza que deseja deslogar da conta atual?",
            "SIM",
            "N�O");

        // Se o usu�rio clicou em "SIM" (que retorna true)
        if (querDeslogar)
        {
            // Redireciona o usu�rio de volta para a tela de Login
            Application.Current.MainPage = new LoginPage();
        }
    }
}
