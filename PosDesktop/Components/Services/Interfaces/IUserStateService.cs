namespace PosDesktop.Services.Interfaces;

public interface IUserStateService
{
    Task<StaffResponseApiResponse> CurrentUser();
    public  Task<bool> IsLoggedIn();
}
