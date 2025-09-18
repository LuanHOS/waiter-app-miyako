using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services;

namespace waiter_app_miyako.ViewModels
{
    // Esta classe agora herda de List<Produtos> e tem uma propriedade para o nome
    public class GrupoDeProdutos : List<Produtos>
    {
        public string NomeDoGrupo { get; private set; }

        public GrupoDeProdutos(string nomeDoGrupo, List<Produtos> produtos) : base(produtos)
        {
            NomeDoGrupo = nomeDoGrupo;
        }
    }

    public class CardapioViewModel : INotifyPropertyChanged
    {
        private readonly MockApiService _apiService = new MockApiService();
        public ObservableCollection<GrupoDeProdutos> CardapioAgrupado { get; } = new();
        public ObservableCollection<Grupos> Categorias { get; } = new();

        public async Task CarregarCardapio()
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
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}