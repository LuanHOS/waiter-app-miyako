using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services;
using System.Collections.ObjectModel; // Adicionar esta using


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


        public CardapioViewModel()
        {
            for (int i = 1; i <= 20; i++) { NumeroClientes.Add(i); }
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