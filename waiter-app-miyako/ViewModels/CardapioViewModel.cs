using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services;
using System.Linq;

namespace waiter_app_miyako.ViewModels
{
    // 🔹 Classe para representar um grupo de produtos (usada no CollectionView)
    public class GrupoDeProdutos : ObservableCollection<Produtos>
    {
        public string NomeDoGrupo { get; }

        public GrupoDeProdutos(string nomeDoGrupo, IEnumerable<Produtos> produtos) : base(produtos)
        {
            NomeDoGrupo = nomeDoGrupo;
        }
    }

    public class CardapioViewModel : INotifyPropertyChanged
    {
        // ✅ Troque o MockApiService pelo serviço real
        private readonly ProdutoService _produtoService = new ProdutoService();
        private readonly GrupoService _grupoService = new GrupoService();

        public ObservableCollection<GrupoDeProdutos> CardapioAgrupado { get; } = new();
        public ObservableCollection<Grupos> Categorias { get; } = new();

        // 🔹 Propriedades auxiliares
        public ObservableCollection<Mesas> Mesas { get; } = new();
        public List<int> NumeroClientes { get; } = new();

        private int _mesaNumero;
        public int MesaNumero
        {
            get => _mesaNumero;
            set
            {
                if (_mesaNumero != value)
                {
                    _mesaNumero = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ItemPedidoViewModel> ItensDoPedido { get; } = new();

        private decimal _total;
        public decimal Total
        {
            get => _total;
            private set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged();
                }
            }
        }

        public CardapioViewModel()
        {
            // Gera opções de 1 a 20 clientes
            for (int i = 1; i <= 20; i++) NumeroClientes.Add(i);

            // Observa alterações na coleção de itens
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

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemPedidoViewModel.Quantidade))
                RecalcularTotal();
        }

        private void RecalcularTotal()
        {
            Total = ItensDoPedido.Sum(item => item.ValorTotal);
        }

        // 🔹 Carrega produtos e grupos do backend
        public async Task CarregarDadosIniciais()
        {
            try
            {
                // 1️⃣ Busca grupos ativos
                var grupos = await _grupoService.ObterGruposAsync();
                Categorias.Clear();
                foreach (var grupo in grupos)
                    Categorias.Add(grupo);

                // 2️⃣ Busca produtos
                var produtos = await _produtoService.ObterProdutosAsync();
                CardapioAgrupado.Clear();

                // 3️⃣ Agrupa produtos pelo grupoId
                foreach (var categoria in grupos)
                {
                    var produtosDoGrupo = produtos.Where(p => p.grupoId == categoria.id).ToList();
                    if (produtosDoGrupo.Any())
                    {
                        // Usa o nome do grupo (ex: "Barcas", "Sushis", etc.)
                        CardapioAgrupado.Add(new GrupoDeProdutos(categoria.grupo ?? categoria.descricao ?? "Sem Grupo", produtosDoGrupo));
                    }
                }

                Console.WriteLine($"✅ {CardapioAgrupado.Count} grupos carregados do cardápio.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao carregar dados iniciais: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
