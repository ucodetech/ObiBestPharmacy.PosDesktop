
using PosDesktop.Components.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PosDesktop.Services;

public class HttpInterceptorHandler : DelegatingHandler
{
    private readonly ISecureStorageService _sessionStorage;

    public HttpInterceptorHandler(ISecureStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // ✅ Only attach token if Authorization header is missing
        if (!request.Headers.Contains("Authorization"))
        {
            var token = await _sessionStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
