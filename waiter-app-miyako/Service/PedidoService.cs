using System.Net.Http.Json;
using waiter_app_miyako.Models;

public class PedidoService
{
    private readonly HttpClient _httpClient;

    public PedidoService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://10.0.2.2:7170/") // ou o que for seu backend
        };
    }

    public async Task<Pedidos?> ObterPedidoPorNumeroMesaAsync(int numeroMesa)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Pedidos>($"api/pedidos/mesa/{numeroMesa}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar pedido da mesa {numeroMesa}: {ex.Message}");
            return null;
        }
    }

    public async Task<List<Pedidos>?> ObterPedidos()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<Pedidos>>("api/ItensPedidos");
            return response ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar pedidos: {ex.Message}");
            return null;
        }
    }
}
