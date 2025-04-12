using Microsoft.Extensions.Logging;
using PosDesktop.Services.Interfaces;
using System.Net.Http.Json;
using static  PosDesktop.Components.Pages.Staff.UpsertStaff;

namespace PosDesktop.Services;

public class StaffManagementService : IStaffManagementService
{
    private readonly DesktopApiClient _client;
    private readonly ILogger<StaffManagementService> _logger;
    private readonly HttpClient _httpClient;

    public StaffManagementService(DesktopApiClient client, ILogger<StaffManagementService> logger, HttpClient httpClient)
    {
        _client = client;
        _logger = logger;
        _httpClient = httpClient;
    }
    public async Task<BaseResponse> AddStaff(ProfileUserDTO request)
    {
        try
        {
            var staffs = await _client.Auth_createStaffCredentialsAsync(request);
            return new ()
            {
                Status = staffs.Status,
                Code = staffs.Code,
                Message = staffs.Message,
                Errors = staffs.Errors
            };
          
        }
        catch (Exception e)
        {
            _logger.LogError(e, "sever error");
            throw;
        }
    }

    public Task<BaseResponse> DeleteStaff(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<StaffResponseIEnumerableApiResponse> GetAllStaff(int pageNumber, int pageSize)
    {
        try
        {
            var staffs = await _client.Staff_getAllAsync(pageNumber, pageSize);
            if(staffs.Status && staffs.Data != null)
            {
                return new StaffResponseIEnumerableApiResponse
                {
                    Status = staffs.Status,
                    Code = staffs.Code,
                    Message = staffs.Message,
                    Data = staffs.Data
                };
            }
            else
            {
                return new StaffResponseIEnumerableApiResponse
                {
                    Status = staffs.Status,
                    Code = staffs.Code,
                    Message = staffs.Message
                };
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "sever error");
            throw;
        }
    }

    public async Task<StaffResponseApiResponse> GetStaff(Guid Id)
    {
        var staff = await _client.Staff_getAsync(Id);
        if (staff.Status && staff.Data != null)
        {
            return new( )
            {
                Status = staff.Status,
                Code = staff.Code,
                Message = staff.Message,
                Data = staff.Data
            };
        }
        else
        {
            return new ()
            {
                Status = staff.Status,
                Code = staff.Code,
                Message = staff.Message
            };
        }
    }

    public Task<StaffResponseApiResponse> GetStaffByRole(Guid roleId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateStaff(ProfileUserDTO request)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CountryCodeModel>> GetCountryCodesAsync()
    {
       var  CountryCodes = await _httpClient.GetFromJsonAsync<List<CountryCodeModel>>("country-code.json");
        if (CountryCodes == null)
        {
            return new List<CountryCodeModel>();
        }
        return CountryCodes;
    }
}
