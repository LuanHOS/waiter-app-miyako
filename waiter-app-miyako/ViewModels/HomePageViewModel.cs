using System.Collections.ObjectModel;
using System.Threading.Tasks;
using waiter_app_miyako.Models;
using waiter_app_miyako.Services; // Certifique-se que está importando o MesaService

namespace waiter_app_miyako.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<MesaViewModel> Mesas { get; set; }

        private readonly MesaService _mesaService;

        public HomePageViewModel()
        {
            Mesas = new ObservableCollection<MesaViewModel>();
            _mesaService = new MesaService();
            _ = CarregarMesasAsync();
        }

        public async Task CarregarMesasAsync()
        {
            try
            {
                var mesas = await _mesaService.GetMesasAsync();

                Console.WriteLine("===============");
                Console.WriteLine($"Mesas carregadas: {mesas.Count}");

                foreach (var mesa in mesas)
                {
                    Console.WriteLine($"Mesa {mesa.numeroMesa} - Status: {mesa.statusMesa}");
                }
                Console.WriteLine("===============");

                Mesas.Clear();

                foreach (var mesa in mesas)
                {
                    Mesas.Add(new MesaViewModel(mesa));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar mesas: {ex.Message}");
            }
        }
    }
}
