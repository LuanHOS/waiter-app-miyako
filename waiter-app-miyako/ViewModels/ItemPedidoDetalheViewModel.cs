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

        // Nova propriedade para verificar se o item já foi entregue
        public bool IsEntregue => Item.Status == "entregue";

        public ItemPedidoDetalheViewModel(ItensPedidos item)
        {
            Item = item;
            // Itens com status "entregue" já vêm marcados e serão desabilitados na UI
            IsSelecionado = IsEntregue;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}