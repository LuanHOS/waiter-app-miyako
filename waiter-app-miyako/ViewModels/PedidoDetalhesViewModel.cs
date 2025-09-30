using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using waiter_app_miyako.Models;

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
                OnPropertyChanged(nameof(QuantidadeTotalItens)); // Notifica a nova propriedade
            }
        }

        public bool IsNotFinalizado => Pedido?.finalizado == false;

        public decimal ValorTotal => Pedido?.itens?.Sum(item => (item.produto?.preco ?? 0) * item.quantidade) ?? 0;

        // Nova propriedade para contar a quantidade total de itens
        public int QuantidadeTotalItens => Pedido?.itens?.Sum(item => item.quantidade) ?? 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}