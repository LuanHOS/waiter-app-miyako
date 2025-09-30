using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.ViewModels
{
    public class ItemPedidoViewModel : INotifyPropertyChanged
    {
        public Produtos Produto { get; set; }

        private int _quantidade;
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (_quantidade != value)
                {
                    _quantidade = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AumentarQuantidadeCommand { get; }
        public ICommand DiminuirQuantidadeCommand { get; }

        public ItemPedidoViewModel(Produtos produto, int quantidade)
        {
            Produto = produto;
            _quantidade = quantidade;

            AumentarQuantidadeCommand = new Command(() => Quantidade++);
            DiminuirQuantidadeCommand = new Command(() =>
            {
                if (Quantidade > 1)
                {
                    Quantidade--;
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}