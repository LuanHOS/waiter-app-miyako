using waiter_app_miyako.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace waiter_app_miyako.Views
{
    [QueryProperty(nameof(Produto), "Produto")]
    [QueryProperty(nameof(AdicionarCallback), "AdicionarCallback")]
    [QueryProperty(nameof(MesaNumero), "MesaNumero")]
    public partial class ItemDetalhesPage : ContentPage
    {
        public Produtos Produto
        {
            get => BindingContext as Produtos;
            set => BindingContext = value;
        }

        public Action<Produtos, int>? AdicionarCallback { get; set; }

        public int MesaNumero { get; set; }

        private int _quantidade = 1;

        public ItemDetalhesPage()
        {
            InitializeComponent();
        }

        private void OnDiminuirClicked(object sender, EventArgs e)
        {
            if (_quantidade > 1)
            {
                _quantidade--;
                QuantidadeLabel.Text = _quantidade.ToString();
            }
        }

        private void OnAumentarClicked(object sender, EventArgs e)
        {
            _quantidade++;
            QuantidadeLabel.Text = _quantidade.ToString();
        }

        private async void OnAdicionarClicked(object sender, EventArgs e)
        {
            try
            {
                if (Produto == null)
                {
                    await DisplayAlert("Erro", "Produto inválido.", "OK");
                    return;
                }

                AdicionarCallback?.Invoke(Produto, _quantidade);

                await Toast.Make($"{_quantidade}x {Produto.descricao} adicionado ao pedido!", ToastDuration.Short).Show();

                // ✅ Volta para o Cardápio
                await VoltarParaCardapioAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao adicionar item: {ex.Message}", "OK");
            }
        }

        private async Task VoltarParaCardapioAsync()
        {
            try
            {
                // ✅ Volta para a página anterior (Cardápio) dentro do Shell
                await Shell.Current.GoToAsync("..", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao voltar: {ex.Message}");
            }
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await VoltarParaCardapioAsync();
        }

        // ✅ Captura o botão físico de voltar (Android)
        protected override bool OnBackButtonPressed()
        {
            MainThread.BeginInvokeOnMainThread(async () => await VoltarParaCardapioAsync());
            return true;
        }
    }
}
