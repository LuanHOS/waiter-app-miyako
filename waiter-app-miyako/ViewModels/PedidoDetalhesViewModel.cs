using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using waiter_app_miyako.Models;
using System.Collections.ObjectModel;

namespace waiter_app_miyako.ViewModels
{
    public class PedidoDetalhesViewModel : INotifyPropertyChanged
    {
        private Pedidos _pedido;
        public Pedidos Pedido
        {
            get => _pedido;
            set
            {
                _pedido = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotFinalizado));
                OnPropertyChanged(nameof(ValorTotal));
                OnPropertyChanged(nameof(QuantidadeTotalItens));

                // Popula a lista de itens com o ViewModel correspondente
                ItensDoPedido.Clear();
                if (_pedido?.itens != null)
                {
                    foreach (var item in _pedido.itens)
                    {
                        ItensDoPedido.Add(new ItemPedidoDetalheViewModel(item));
                    }
                }
            }
        }

        public ObservableCollection<ItemPedidoDetalheViewModel> ItensDoPedido { get; } = new();

        public bool IsNotFinalizado => Pedido?.finalizado == false;

        public decimal ValorTotal => Pedido?.itens?.Sum(item => (item.produto?.preco ?? 0) * item.quantidade) ?? 0;

        public int QuantidadeTotalItens => Pedido?.itens?.Sum(item => item.quantidade) ?? 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}