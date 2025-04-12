using Microsoft.AspNetCore.Components.Authorization;
using PosDesktop.Components.Services.Interfaces;
using PosDesktop.Helpers;
using System.Security.Claims;

namespace PosDesktop.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ISecureStorageService _sessionStorage;

    public CustomAuthStateProvider(ISecureStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        var token = await _sessionStorage.GetAsync("authToken");

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Token is null or empty");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = JwtParser.ParseClaimsFromJwt(token);
        if (claims == null || !claims.Any())
        {
            Console.WriteLine("No claims found in token");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        Console.WriteLine("User authenticated with claims:");
        foreach (var claim in claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }

        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        var claims = JwtParser.ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        await _sessionStorage.SetAsync("authToken", token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
         _sessionStorage.Remove("authToken");
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}
