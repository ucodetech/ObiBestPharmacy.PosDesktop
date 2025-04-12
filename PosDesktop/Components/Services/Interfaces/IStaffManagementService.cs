using static  PosDesktop.Components.Pages.Staff.UpsertStaff;

namespace PosDesktop.Services.Interfaces;

public interface IStaffManagementService
{
    Task<StaffResponseApiResponse> GetStaff(Guid Id);
    Task<StaffResponseApiResponse> GetStaffByRole(Guid roleId);
    Task<StaffResponseIEnumerableApiResponse> GetAllStaff(int pageNumber, int pageSize);
    Task<BaseResponse> AddStaff(ProfileUserDTO request);
    Task<BaseResponse> UpdateStaff(ProfileUserDTO request);
    Task<BaseResponse> DeleteStaff(Guid Id);
    Task<List<CountryCodeModel>> GetCountryCodesAsync();

}
