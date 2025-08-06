using waiter_app_miyako.Views; // Verifique se esta linha existe

namespace waiter_app_miyako
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrando as rotas para cada página da TabBar
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(PedidosPage), typeof(PedidosPage));
            Routing.RegisterRoute(nameof(CardapioPage), typeof(CardapioPage));
            Routing.RegisterRoute(nameof(PerfilPage), typeof(PerfilPage));
            Routing.RegisterRoute(nameof(ConfiguracoesPage), typeof(ConfiguracoesPage));
        }
    }
}