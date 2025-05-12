using ApiClient;
using Microsoft.AspNetCore.Http;

namespace PosDesktop.Services.Interfaces;

public interface IFileService
{
    Task<FileResponseApiResponse> UploadFile(FileParameter formFile);
    Task<BaseResponse> DeleteFile(string filePath);
}
