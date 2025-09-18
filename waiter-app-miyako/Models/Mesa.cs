using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace waiter_app_miyako.Models
{
    public class Mesa : INotifyPropertyChanged
    {
        public int Numero { get; set; }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(); // Notifica a UI sobre a mudança de status
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}