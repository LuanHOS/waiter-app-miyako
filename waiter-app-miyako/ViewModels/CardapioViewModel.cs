using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services;
using System.Collections.Specialized;

namespace waiter_app_miyako.ViewModels
{
    public class GrupoDeProdutos : List<Produtos>
    {
        public string NomeDoGrupo { get; private set; }
        public GrupoDeProdutos(string nomeDoGrupo, List<Produtos> produtos) : base(produtos) { NomeDoGrupo = nomeDoGrupo; }
    }

    public class CardapioViewModel : INotifyPropertyChanged
    {
        private readonly MockApiService _apiService = new MockApiService();
        public ObservableCollection<GrupoDeProdutos> CardapioAgrupado { get; } = new();
        public ObservableCollection<Grupos> Categorias { get; } = new();

        // Propriedades para os Pickers
        public ObservableCollection<Mesas> Mesas { get; } = new();
        public List<int> NumeroClientes { get; } = new();

        public ObservableCollection<ItemPedidoViewModel> ItensDoPedido { get; } = new();

        // Propriedade para o total do pedido
        public decimal Total { get; private set; }

        public CardapioViewModel()
        {
            for (int i = 1; i <= 20; i++) { NumeroClientes.Add(i); }

            // Observa alterações na lista de itens (itens adicionados/removidos)
            ItensDoPedido.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.NewItems)
                        item.PropertyChanged += Item_PropertyChanged;
                }
                if (e.OldItems != null)
                {
                    foreach (INotifyPropertyChanged item in e.OldItems)
                        item.PropertyChanged -= Item_PropertyChanged;
                }
                RecalcularTotal();
            };
        }

        // Observa alterações na quantidade de um item específico
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemPedidoViewModel.Quantidade))
            {
                RecalcularTotal();
            }
        }

        // Método que calcula o total
        private void RecalcularTotal()
        {
            Total = ItensDoPedido.Sum(item => item.ValorTotal);
            OnPropertyChanged(nameof(Total));
        }

        public async Task CarregarDadosIniciais()
        {
            var todosProdutos = await _apiService.FetchProdutos();
            var todasCategorias = await _apiService.FetchCategorias();
            CardapioAgrupado.Clear();
            Categorias.Clear();
            foreach (var categoria in todasCategorias)
            {
                Categorias.Add(categoria);
                var produtosDoGrupo = todosProdutos.Where(p => p.grupoId == categoria.id).ToList();
                if (produtosDoGrupo.Any())
                {
                    CardapioAgrupado.Add(new GrupoDeProdutos(categoria.descricao, produtosDoGrupo));
                }
            }

            var mesasDaApi = await _apiService.FetchMesas();
            Mesas.Clear();
            foreach (var mesa in mesasDaApi)
            {
                Mesas.Add(mesa);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}