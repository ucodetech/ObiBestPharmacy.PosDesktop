using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PosDesktop.Services.Interfaces;

namespace PosDesktop.Services;

public class FileService : IFileService
{
    private readonly DesktopApiClient _client;
    private readonly ILogger<FileService> _logger;
    public FileService(DesktopApiClient client, ILogger<FileService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public async Task<BaseResponse> DeleteFile(string filePath)
    {
        var response = await _client.File_deleteAsync(filePath);
        if (response.Status)
        {
            return new()
            {
                Status = response.Status,
                Code = response.Code,
                Message = response.Message,
            };
        }
        return new()
        {
            Status = response.Status,
            Code = response.Code,
            Message = response.Message,
        };
    }

    public async Task<FileResponseApiResponse> UploadFile(FileParameter formFile)
    {
        var response = await _client.File_uploadAsync(formFile);
        if (response.Status)
        {
            return new()
            {
                Status = response.Status,
                Code = response.Code,
                Message = response.Message,
                Data = response.Data
            };
        }
        return new()
        {
            Status = response.Status,
            Code = response.Code,
            Message = response.Message,
        };
    }

}
