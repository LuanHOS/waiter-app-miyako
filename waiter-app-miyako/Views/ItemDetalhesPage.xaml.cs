namespace waiter_app_miyako.Views;

public partial class ItemDetalhesPage : ContentPage
{
    private int quantidade = 1;

    public ItemDetalhesPage()
    {
        InitializeComponent();
        AtualizarLabelQuantidade();
    }

    // O método OnVoltarClicked foi REMOVIDO daqui.

    private void OnDiminuirClicked(object sender, EventArgs e)
    {
        if (quantidade > 1)
        {
            quantidade--;
            AtualizarLabelQuantidade();
        }
    }

    private void OnAumentarClicked(object sender, EventArgs e)
    {
        quantidade++;
        AtualizarLabelQuantidade();
    }

    private void AtualizarLabelQuantidade()
    {
        QuantidadeLabel.Text = quantidade.ToString();
    }

    private async void OnAdicionarClicked(object sender, EventArgs e)
    {
        // Lógica futura para adicionar o item ao pedido
        await DisplayAlert("Adicionado", $"{quantidade} item(s) adicionado(s) ao pedido.", "OK");
    }
}