using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using PosDesktop.Services;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using ApiClient;
using PosDesktop . Helpers;

namespace PosDesktop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Load appsettings.json
            using var appsettingsStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("PosDesktop.wwwroot.appsettings.json");

            if (appsettingsStream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(appsettingsStream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // 🔥 Your migrated Blazor WebAssembly setup
            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddAuthorizationCore();

            // Replace this with your actual API base URL or use appsettings.json

            var url = builder.Configuration.GetValue<string>("ApiSettings:ApiUrl");

            // Default HttpClient
            builder.Services.AddScoped(sp => new HttpClient());

            // Named HttpClient with custom handler
            builder.Services.AddHttpClient("DesktopApiClient", client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<HttpInterceptorHandler>();

            builder.Services.AddScoped<DesktopApiClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("DesktopApiClient");
                var interceptorHandler = sp.GetRequiredService<HttpInterceptorHandler>();
                return new DesktopApiClient(url, httpClient);
            });
			builder.Services . AddSingleton<ApiClientWrapper> ( );
			// Your own extension for registering services
			builder . Services.AddApplicationServices(builder.Configuration);

            return builder.Build();
        }
    }
}
