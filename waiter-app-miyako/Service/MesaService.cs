using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using waiter_app_miyako.Models;

public class MesaService
{
    private readonly HttpClient _httpClient;

    public MesaService()
    {
        var handler = new HttpClientHandler
        {
            // ⚠️ Ignora certificado SSL (útil só para ambiente de desenvolvimento)
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://10.0.2.2:7170/") // Android emulador -> aponta para localhost do host
        };
    }

    /// <summary>
    /// Obtém todas as mesas do servidor.
    /// </summary>
    public async Task<List<Mesas>> GetMesasAsync()
    {
        try
        {
            var mesas = await _httpClient.GetFromJsonAsync<List<Mesas>>("api/mesas");
            return mesas ?? new List<Mesas>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar mesas: {ex.Message}");
            return new List<Mesas>();
        }
    }

    /// <summary>
    /// Atualiza o status de uma mesa (ex: Livre → Ocupada → Finalizada)
    /// </summary>
    public async Task<bool> AtualizarStatusMesaAsync(int idMesa, string novoStatus)
    {
        try
        {
            var content = JsonContent.Create(new { Status = novoStatus });
            var response = await _httpClient.PutAsync($"api/mesas/{idMesa}/status", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar status da mesa {idMesa}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Cria uma nova mesa (caso queira cadastrar mesas no início do sistema)
    /// </summary>
    public async Task<bool> CriarMesaAsync(Mesas novaMesa)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/mesas", novaMesa);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar mesa: {ex.Message}");
            return false;
        }
    }
}
