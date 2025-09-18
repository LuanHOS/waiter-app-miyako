using System.Collections.ObjectModel;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<Mesa> Mesas { get; set; }

        public HomePageViewModel()
        {
            Mesas = new ObservableCollection<Mesa>();
            CarregarMesas();
        }

        private void CarregarMesas()
        {
            // Dados de exemplo que virão da sua API
            for (int i = 1; i <= 20; i++)
            {
                string status = "livre";
                if (i % 4 == 0) status = "ocupada";
                if (i % 7 == 0) status = "atencao";

                Mesas.Add(new Mesa { Numero = i, Status = status });
            }
        }
    }
}