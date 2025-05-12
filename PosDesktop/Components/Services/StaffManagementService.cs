using ApiClient;
using Microsoft.Extensions.Logging;
using PosDesktop . Helpers;
using PosDesktop.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using static  PosDesktop.Components.Pages.Staff.UpsertStaff;

namespace PosDesktop.Services;

public class StaffManagementService : IStaffManagementService
{
    private readonly DesktopApiClient _client;
	private readonly ApiClientWrapper _clientWrapper;
    private readonly ILogger<StaffManagementService> _logger;
    private readonly HttpClient _httpClient;
    private const string ResourcePath = "PosDesktop.wwwroot.country-code.json";

    public StaffManagementService(DesktopApiClient client, ApiClientWrapper clientWrapper, ILogger<StaffManagementService> logger, HttpClient httpClient)
    {
        _client = client;
		_clientWrapper = clientWrapper;
        _logger = logger;
        _httpClient = httpClient;
    }
    public async Task<BaseResponse> AddStaff(ProfileUserDTO request)
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Auth_createStaffCredentialsAsync(request));
		return response;
		
    }

    public async Task<BaseResponse> DeleteStaff(Guid Id)
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Staff_deleteAsync(Id));
		return response;
	}

    public async Task<StaffResponseIEnumerableApiResponse> GetAllStaff()
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Staff_getAllAsync());
		return response;
		
    }

    public async Task<StaffResponseApiResponse> GetStaff(Guid Id)
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Staff_getAsync(Id));
		return response;
	}

    public async Task<StaffResponseApiResponse> GetStaffByRole(Guid roleId)
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Staff_getByRoleAsync(roleId));
		return response;
		}

    public async Task<BaseResponse> UpdateStaff(ProfileUserDTO request)
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Staff_updateAsync(request));
		return response;
		}

    public async Task<List<CountryCodeModel>> GetCountryCodesAsync()
    {
        
        var resourceName = ResourcePath;
        var assembly = typeof(StaffManagementService).Assembly;

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return new List<CountryCodeModel>();

        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var data = JsonSerializer.Deserialize<List<CountryCodeModel>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return data ?? new List<CountryCodeModel>();
    }

}
