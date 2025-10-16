using System.ComponentModel;
using System.Runtime.CompilerServices;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.ViewModels
{
    public class MesaViewModel : INotifyPropertyChanged
    {
        private readonly Mesas _mesaModel;

        public MesaViewModel(Mesas mesaModel)
        {
            _mesaModel = mesaModel;
        }

        public int Id => _mesaModel.numeroMesa;
        public int Numero => _mesaModel.numeroMesa;

        // 🔄 Status legível para exibição
        public string Status
        {
            get => _mesaModel.statusMesa switch
            {
                "L" => "Livre",
                "R" => "Reservada",
                "A" => "ComPedidoPendente",  // <- sem espaço
                "F" => "PedidoEntregue",     // <- sem espaço
                _ => "Desconhecido"
            };
        }

        // ✅ Cor associada ao status (usada no Grid de Mesas)
        public Color Cor
        {
            get => Status switch
            {
                "Livre" => Colors.White,
                "Reservada" => Colors.LightGray,
                "Com Pedido Pendente" => Colors.Yellow,
                "Pedido Entregue" => Colors.Green,
                _ => Colors.White
            };
        }

        // 🔁 Atualiza o status dinamicamente (caso você precise mudar a cor ao vivo)
        public void AtualizarStatus(string novoStatus)
        {
            _mesaModel.statusMesa = novoStatus;
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(Cor));
        }

        // 🔔 Notificação de mudança
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
