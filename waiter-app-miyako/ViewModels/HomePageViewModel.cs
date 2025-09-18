using System.Collections.ObjectModel;
using System.Threading.Tasks;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services; // Importar o serviço

namespace waiter_app_miyako.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<MesaViewModel> Mesas { get; set; }
        private readonly MockApiService _apiService;

        public HomePageViewModel()
        {
            Mesas = new ObservableCollection<MesaViewModel>();
            _apiService = new MockApiService(); // Instancia o serviço
        }

        public async Task CarregarMesas()
        {
            var mesasDaApi = await _apiService.FetchMesas();

            Mesas.Clear();
            foreach (var mesaModel in mesasDaApi)
            {
                Mesas.Add(new MesaViewModel(mesaModel));
            }
        }
    }
}