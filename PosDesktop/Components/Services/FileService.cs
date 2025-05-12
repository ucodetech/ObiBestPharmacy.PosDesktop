using ApiClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PosDesktop . Helpers;
using PosDesktop.Services.Interfaces;

namespace PosDesktop.Services;

public class FileService : IFileService
{
    private readonly DesktopApiClient _client;
	private readonly ApiClientWrapper _clientWrapper;
    private readonly ILogger<FileService> _logger;
    public FileService(DesktopApiClient client, ApiClientWrapper clientWrapper, ILogger<FileService> logger)
    {
        _client = client;
		_clientWrapper = clientWrapper;
        _logger = logger;
    }
    public async Task<BaseResponse> DeleteFile(string filePath)
    {

		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.File_deleteAsync(filePath));
		return response;
    }

    public async Task<FileResponseApiResponse> UploadFile(FileParameter formFile)
    {
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.File_uploadAsync(formFile));
		return response; 
        
    }

}
