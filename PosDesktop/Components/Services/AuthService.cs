using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using PosDesktop.Components.Services.Interfaces;
using PosDesktop.Helpers;
using PosDesktop.Services.Interfaces;

namespace PosDesktop.Services;

public class AuthService : IAuthService
{
    private readonly DesktopApiClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ISecureStorageService _sessionStorage;
    private readonly ILogger<AuthService> _logger;
    private readonly IHttpInterceptorService _httpInterceptorService;
    public AuthService(DesktopApiClient client, AuthenticationStateProvider authStateProvider, ISecureStorageService sessionStorage, ILogger<AuthService> logger, IHttpInterceptorService httpInterceptorService)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _sessionStorage = sessionStorage;
        _logger = logger;
        _httpInterceptorService = httpInterceptorService;
    }

    public async Task<CountryResponseIEnumerableApiResponse> GetCountries()
    {
        try
        {
            var cs = await _client.Countries_listAsync();
            return cs;
        }
        catch
        {
            throw;
        }
    }

    public async Task<StaffResponseApiResponse> GetCurrentUser()
    {
        try
        {
            var Id = await _sessionStorage.GetAsync("Id");
            return await _client.Staff_getAsync(Guid.Parse(Id));
        }
        catch
        {
            return null;
        }
    }

    public async Task<LoginResponseApiResponse> Login(LoginUserRequest request) 
    {
        try
        {
            var result = await _client.Auth_loginByCredentialsAsync(request);
            if (!result.Status)
            {
                return new()
                {
                    Status = false,
                    Code = result.Code, 
                    Message = result.Message,
                    Errors = result.Errors

                };
            }
            if (result.Data is not null)
            {
                await _sessionStorage.SetAsync("authToken", result.Data.Token);
                await _sessionStorage.SetAsync("Id", result.Data.Id.ToString());
                await _sessionStorage.SetAsync("email", result.Data.Email);

                await _httpInterceptorService.AttachToken();
                // Notify authentication state provider
                await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Data.Token);

                return new()
                {
                    Status = true,
                    Code = result.Code,
                    Message = result.Message,
                    Data = result.Data
                };
            }
            return new()
            {
                Status = false,
                Code = result.Code,
                Message = result.Message,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error logging in: {e.Message}");
            throw;
        }
    }

    public async Task Logout()
    {
        var email = await _sessionStorage.GetAsync("email");
        await _client.Auth_logoutAsync(email);
         _sessionStorage.Remove("authToken");
         _sessionStorage.Remove("Id");
         _sessionStorage.Remove("Email");
        await((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
    }

    public async Task<LoginResponseApiResponse> LoginWithCode(int Code)
    {
        try
        {
            var result = await _client.Auth_loginBycodeAsync(Code);
            if (!result.Status)
            {
                return new()
                {
                    Status = false,
                    Code = result.Code,
                    Message = result.Message,
                    Errors = result.Errors

                };
            }
            if (result.Data is not null)
            {
                await _sessionStorage.SetAsync("authToken", result.Data.Token);
                await _sessionStorage.SetAsync("Id", result.Data.Id.ToString());
                await _sessionStorage.SetAsync("email", result.Data.Email);

                await _httpInterceptorService.AttachToken();
                // Notify authentication state provider
                await ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(result.Data.Token);

                return new()
                {
                    Status = true,
                    Code = result.Code,
                    Message = result.Message,
                    Data = result.Data
                };
            }
            return new()
            {
                Status = false,
                Code = result.Code,
                Message = result.Message,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error logging in: {e.Message}");
            throw;
        }
    }

    public async Task<BaseResponse> ForgotPassword(string Email)
    {
        try
        {
            var result = await _client.Password_resetLinkAsync(Email);
            if (!result.Status)
            {
                return new()
                {
                    Status = false,
                    Code = result.Code,
                    Message = result.Message,
                    Errors = result.Errors
                };
            }
            return new()
            {
                Status = true,
                Code = result.Code,
                Message = result.Message,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "sever error");
            throw;
        }

        
    }

    public async Task<BaseResponse> ResetPassword(ResetPasswordRequest request)
    {
        try
        {
            var result = await _client.Auth_resetPasswordAsync(request);
            if (!result.Status)
            {
                return new()
                {
                    Status = false,
                    Code = result.Code,
                    Message = result.Message,
                    Errors = result.Errors
                };
            }
            return new()
            {
                Status = true,
                Code = result.Code,
                Message = result.Message,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "sever error");
            throw;
        }
    }

    public async Task<BaseResponse> Changepassword(ChangePasswordRequest request)
    {
        try
        {
            var result = await _client.Auth_changePasswordAsync(request);
            if (!result.Status)
            {
                return new()
                {
                    Status = false,
                    Code = result.Code,
                    Message = result.Message,
                    Errors = result.Errors
                };
            }
            return new()
            {
                Status = true,
                Code = result.Code,
                Message = result.Message,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "sever error");
            throw;
        }
    }

    public async Task<StateResponseIEnumerableApiResponse> GetStates(Guid CountryId)
    {
        var states = await _client.States_listAsync(CountryId);
        if (states.Status && states.Data != null)
        {
            return new ()
            {
                Status = states.Status,
                Code = states.Code,
                Message = states.Message,
                Data = states.Data
            };
        }
        else
        {
            return new ()
            {
                Status = states.Status,
                Code = states.Code,
                Message = states.Message
            };
        }

    }

    public async Task<CityResponseIEnumerableApiResponse> GetCities(Guid StateId)
    {
        var cities = await _client.Cities_listAsync(StateId);
        if (cities.Status && cities.Data != null)
        {
            return new()
            {
                Status = cities.Status,
                Code = cities.Code,
                Message = cities.Message,
                Data = cities.Data
            };
        }
        else
        {
            return new()
            {
                Status = cities.Status,
                Code = cities.Code,
                Message = cities.Message
            };
        }

    }

    public async Task<RoleResponseIEnumerableApiResponse> GetRoles()
    {
        var roles = await _client.Roles_listAsync();
        if(roles.Data is not null)
        {
            return new()
            {
                Status = roles.Status,
                Code = roles.Code,
                Message = roles.Message,
                Data = roles.Data
            };
        }
        else
        {
            return new()
            {
                Status = roles.Status,
                Code = roles.Code,
                Message = roles.Message
            };
        }
    }
}
