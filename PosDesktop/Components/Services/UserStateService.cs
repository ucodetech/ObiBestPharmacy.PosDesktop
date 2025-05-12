using ApiClient;
using Microsoft.AspNetCore.Components.Authorization;
using PosDesktop.Components.Services.Interfaces;
using PosDesktop.Services.Interfaces;

namespace PosDesktop.Services;

public class UserStateService : IUserStateService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly DesktopApiClient _client;
    private readonly ISecureStorageService _sessionStorage;    
    public UserStateService(AuthenticationStateProvider authenticationStateProvider, DesktopApiClient client, ISecureStorageService sessionStorageService)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _client = client;
        _sessionStorage = sessionStorageService;
    }
    public async Task<StaffResponseApiResponse> CurrentUser()
    {
        StaffResponseApiResponse userResponse = new StaffResponseApiResponse();
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if(user.Identity is {IsAuthenticated: true})
        {
            var Id = await _sessionStorage.GetAsync("Id");
            //get user details from API CALL
            var staff = await _client.Staff_currentUserAsync(Guid.Parse(Id));
            if (staff.Status && staff.Data != null)
            {
                userResponse.Status = staff.Status;
                userResponse.Code = staff.Code;
                userResponse.Message = staff.Message;
                userResponse.Data = staff.Data;
            }
            else
            {
                userResponse.Status = staff.Status;
                userResponse.Code = staff.Code;
                userResponse.Message = staff.Message;
            }
        }

        return userResponse;
    }

    public async Task<bool> IsLoggedIn()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity is { IsAuthenticated: true })
        {
            return true;
        }
        return false;
    }
}
