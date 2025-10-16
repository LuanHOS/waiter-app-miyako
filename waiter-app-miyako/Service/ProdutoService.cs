using System.Net.Http.Json;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.Services
{
    public class ProdutoService
    {
        private readonly HttpClient _httpClient;

        public ProdutoService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7170/") // 🔹 Ajuste conforme seu backend
            };
        }

        /// <summary>
        /// Busca todos os produtos disponíveis no backend.
        /// </summary>
        public async Task<List<Produtos>> ObterProdutosAsync()
        {
            try
            {
                var produtos = await _httpClient.GetFromJsonAsync<List<Produtos>>("api/produtos");
                return produtos ?? new List<Produtos>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao buscar produtos: {ex.Message}");
                return new List<Produtos>();
            }
        }
    }
}
