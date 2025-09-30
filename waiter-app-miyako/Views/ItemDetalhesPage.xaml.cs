using waiter_app_miyako.Models;
using waiter_app_miyako.ViewModels; // Adicionar esta using

namespace waiter_app_miyako.Views;

public partial class ItemDetalhesPage : ContentPage
{
    private int quantidade = 1;
    private readonly Produtos _produto;
    private readonly Action<Produtos, int> _onAdicionarCallback; // Ação de retorno

    // Construtor modificado para receber a ação
    public ItemDetalhesPage(Produtos produto, Action<Produtos, int> onAdicionarCallback)
    {
        InitializeComponent();
        _produto = produto;
        _onAdicionarCallback = onAdicionarCallback;
        this.BindingContext = _produto;
        AtualizarLabelQuantidade();
    }

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
        // Executa a ação de retorno, enviando o produto e a quantidade
        _onAdicionarCallback?.Invoke(_produto, quantidade);

        // Volta para a página anterior (CardapioPage)
        await Navigation.PopAsync();
    }
}