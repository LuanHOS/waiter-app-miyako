namespace waiter_app_miyako.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnAcessarClicked(object sender, EventArgs e)
    {
        // Por enquanto, apenas navega para a tela principal do app.
        Application.Current.MainPage = new AppShell();
    }
}