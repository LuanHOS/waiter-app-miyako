using System.ComponentModel;
using System.Runtime.CompilerServices;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.ViewModels
{
    public class ItemPedidoDetalheViewModel : INotifyPropertyChanged
    {
        public ItensPedidos Item { get; }

        private bool _isSelecionado;
        public bool IsSelecionado
        {
            get => _isSelecionado;
            set
            {
                if (_isSelecionado != value)
                {
                    _isSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        // Propriedades para verificar o status do item
        public bool IsEntregue => Item.Status == "entregue";
        public bool IsCancelado => Item.Status == "cancelado"; // Propriedade adicionada

        public ItemPedidoDetalheViewModel(ItensPedidos item)
        {
            Item = item;
            // Itens entregues ou cancelados já vêm marcados
            IsSelecionado = IsEntregue || IsCancelado;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}