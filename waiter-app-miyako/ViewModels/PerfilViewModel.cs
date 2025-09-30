using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services;

namespace waiter_app_miyako.ViewModels
{
    public class PerfilViewModel : INotifyPropertyChanged
    {
        private readonly MockApiService _apiService;

        private Funcionarios _funcionario;
        public Funcionarios Funcionario
        {
            get => _funcionario;
            set
            {
                _funcionario = value;
                OnPropertyChanged();
            }
        }

        public PerfilViewModel()
        {
            _apiService = new MockApiService();
            Funcionario = new Funcionarios(); // Inicializa para evitar erros de nulo na UI
        }

        public async Task CarregarFuncionario()
        {
            Funcionario = await _apiService.FetchFuncionarioLogado();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}