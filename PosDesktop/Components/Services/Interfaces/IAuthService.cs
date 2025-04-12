using  PosDesktop.Components.Pages.Auth;

namespace PosDesktop.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseApiResponse> Login(LoginUserRequest request);
    Task<LoginResponseApiResponse> LoginWithCode(int Code);
    Task Logout();
    Task<StaffResponseApiResponse> GetCurrentUser();
    Task<RoleResponseIEnumerableApiResponse> GetRoles();
    Task<CountryResponseIEnumerableApiResponse> GetCountries();
    Task<StateResponseIEnumerableApiResponse> GetStates(Guid CountryId);
    Task<CityResponseIEnumerableApiResponse> GetCities(Guid StateId);
    Task<BaseResponse> ForgotPassword(string Email);
    Task<BaseResponse> ResetPassword(ResetPasswordRequest request);
    Task<BaseResponse> Changepassword(ChangePasswordRequest request);
}
