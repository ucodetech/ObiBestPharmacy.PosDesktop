using ApiClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using PosDesktop.Components.Services.Interfaces;
using PosDesktop.Helpers;
using PosDesktop.Services.Interfaces;

namespace PosDesktop.Services;

public class AuthService : IAuthService
{
    private readonly DesktopApiClient _client;
	private readonly ApiClientWrapper _apiClientWrapper;
	private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ISecureStorageService _sessionStorage;
    private readonly ILogger<AuthService> _logger;
    private readonly IHttpInterceptorService _httpInterceptorService;
    public AuthService(DesktopApiClient client,ApiClientWrapper apiClientWrapper,  AuthenticationStateProvider authStateProvider, ISecureStorageService sessionStorage, ILogger<AuthService> logger, IHttpInterceptorService httpInterceptorService)
    {
        _client = client;
		_apiClientWrapper = apiClientWrapper;
		_authStateProvider = authStateProvider;
        _sessionStorage = sessionStorage;
        _logger = logger;
        _httpInterceptorService = httpInterceptorService;
    }

    public async Task<CountryResponseIEnumerableApiResponse> GetCountries()
    {
		var response = await _apiClientWrapper . ExecuteApiCall ( ( ) => _client . Countries_listAsync (  ) );
		return response;
		
    }

    public async Task<StaffResponseApiResponse> GetCurrentUser()
    {
		var Id = await _sessionStorage.GetAsync("Id");
		var response = await _apiClientWrapper . ExecuteApiCall ( ( ) => _client . Staff_getAsync(Guid.Parse(Id)) );
		return response;
		
    }

    public async Task<LoginResponseApiResponse> Login(LoginUserRequest request) 
    {

		var response = await _apiClientWrapper . ExecuteApiCall ( ( ) => _client . Auth_loginByCredentialsAsync ( request ) );
		if ( response . Data is not null )
			{
			await _sessionStorage . SetAsync ( "authToken", response . Data . Token );
			await _sessionStorage . SetAsync ( "Id", response . Data . Id . ToString ( ) );
			await _sessionStorage . SetAsync ( "email", response . Data . Email );

			await _httpInterceptorService . AttachToken ( );
			// Notify authentication state provider
			await ( ( CustomAuthStateProvider ) _authStateProvider ) . MarkUserAsAuthenticated ( response . Data . Token );

			return new ( )
				{
				Status = true,
				Code = response . Code,
				Message = response . Message,
				Data = response . Data
				};
			}
		return response;

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
        
		var response = await _apiClientWrapper . ExecuteApiCall ( ( ) => _client . Auth_loginBycodeAsync(Code) );
		if ( response . Data is not null )
				{
				await _sessionStorage . SetAsync ( "authToken", response . Data . Token );
				await _sessionStorage . SetAsync ( "Id", response . Data . Id . ToString ( ) );
				await _sessionStorage . SetAsync ( "email", response . Data . Email );

				await _httpInterceptorService . AttachToken ( );
				// Notify authentication state provider
				await ( ( CustomAuthStateProvider ) _authStateProvider ) . MarkUserAsAuthenticated ( response . Data . Token );

				return new ( )
					{
					Status = true,
					Code = response . Code,
					Message = response . Message,
					Data = response . Data
					};
				}
			return response;
	}

    public async Task<BaseResponse> ForgotPassword(string Email)
    {
		var response = await _apiClientWrapper . ExecuteApiCall ( () => _client.Password_resetLinkAsync ( Email ) );
		return response;
        

        
    }

    public async Task<BaseResponse> ResetPassword(ResetPasswordRequest request)
    {
		var response = await _apiClientWrapper . ExecuteApiCall ( () => _client.Auth_resetPasswordAsync ( request ) );
		return response;
      
    }

    public async Task<BaseResponse> Changepassword(ChangePasswordRequest request)
    {
		var response = await _apiClientWrapper.ExecuteApiCall(() => _client.Auth_changePasswordAsync(request));
		return response;
        
    }

    public async Task<StateResponseIEnumerableApiResponse> GetStates(Guid CountryId)
    {
		var response = await _apiClientWrapper.ExecuteApiCall(() => _client.States_listAsync(CountryId));
		return response;

    }

    public async Task<CityResponseIEnumerableApiResponse> GetCities(Guid StateId)
    {
		var response = await _apiClientWrapper.ExecuteApiCall(() => _client.Cities_listAsync(StateId));
		return response;

    }

    public async Task<RoleResponseIEnumerableApiResponse> GetRoles()
    {
       var response = await _apiClientWrapper.ExecuteApiCall(() => _client.Roles_listAsync());
		return response;

	}
}
