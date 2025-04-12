
using Microsoft.AspNetCore.Components.Authorization;
//using PosDesktop.Services.Interfaces;
//using PosDesktop.Services;
using System.Net.Http.Headers;
using Blazored.Toast;
using Microsoft.Extensions.Configuration;
using PosDesktop.Services.Interfaces;
using PosDesktop.Services;
using PosDesktop.Components.Services.Interfaces;
using PosDesktop.Components.Services;

namespace PosDesktop;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) // Add IConfiguration parameter
    {

        // ✅ Register Custom Services
         services.AddScoped<IAuthService, AuthService>();

        services.AddSingleton<ISecureStorageService, SecureStorageService>();
        services.AddScoped<IHttpInterceptorService, HttpInterceptorService>();
        services.AddTransient<HttpInterceptorHandler>();
        services.AddScoped<IUserStateService, UserStateService>();
        services.AddScoped<IStaffManagementService, StaffManagementService>();
        services.AddScoped<IFileService, FileService>();


        return services;
    }
}
