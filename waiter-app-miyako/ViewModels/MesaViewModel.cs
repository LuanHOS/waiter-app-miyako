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

        // A View (HomePage.xaml) se conectará a estas propriedades.
        // Elas apenas repassam os valores do Model.
        public int Numero => _mesaModel.numeroMesa;
        public string Status => _mesaModel.statusMesa;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}