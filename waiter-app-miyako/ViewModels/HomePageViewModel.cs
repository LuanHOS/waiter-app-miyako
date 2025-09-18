using System.Collections.ObjectModel;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<MesaViewModel> Mesas { get; set; }

        public HomePageViewModel()
        {
            Mesas = new ObservableCollection<MesaViewModel>();
            CarregarMesas();
        }

        private void CarregarMesas()
        {
            // Simulação de dados recebidos da API (lista de Models 'Mesas')
            var mesasDaApi = new List<Mesas>();
            for (int i = 1; i <= 20; i++)
            {
                string status = "livre";
                if (i % 4 == 0) status = "ocupada";
                if (i % 7 == 0) status = "atencao";

                mesasDaApi.Add(new Mesas { numeroMesa = i, statusMesa = status });
            }

            // Transforma cada Model da API em uma ViewModel para a tela
            foreach (var mesaModel in mesasDaApi)
            {
                Mesas.Add(new MesaViewModel(mesaModel));
            }
        }
    }
}