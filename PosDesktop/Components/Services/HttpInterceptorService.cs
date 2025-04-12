using PosDesktop.Components.Services.Interfaces;
using PosDesktop.Services.Interfaces;
using System.Net.Http.Headers;

namespace PosDesktop.Services;

public class HttpInterceptorService : IHttpInterceptorService
{
    private readonly ISecureStorageService _sessionStorage;
    private readonly HttpClient _httpClient;

    public HttpInterceptorService(ISecureStorageService sessionStorage, HttpClient httpClient)
    {
        _sessionStorage = sessionStorage;
        _httpClient = httpClient;
    }

    public async Task AttachToken()
    {
        var token = await _sessionStorage.GetAsync("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
