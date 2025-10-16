using System.Net.Http.Json;
using waiter_app_miyako.Models;

public class GrupoService
{
    private readonly HttpClient _httpClient;

    public GrupoService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://10.0.2.2:7170/") // ajuste se necessário
        };
    }

    public async Task<List<Grupos>> ObterGruposAsync()
    {
        try
        {
            var grupos = await _httpClient.GetFromJsonAsync<List<Grupos>>("api/grupos");
            return grupos?.Where(g => g.ativo == true).ToList() ?? new List<Grupos>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao buscar grupos: {ex.Message}");
            return new List<Grupos>();
        }
    }
}
