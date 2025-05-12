using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace PosDesktop . Helpers;

using ApiClient;
using Microsoft . Extensions . Logging;

public class ApiClientWrapper
	{
	private readonly DesktopApiClient _apiClient;
	private readonly ILogger<ApiClientWrapper> _logger;

	public ApiClientWrapper ( DesktopApiClient apiClient, ILogger<ApiClientWrapper> logger )
		{
		_apiClient = apiClient;
		_logger = logger;
		}

    public async Task<TResponse> ExecuteApiCall<TResponse>(Func<Task<TResponse>> apiCall)
        where TResponse : class
    {
        try
        {
            // Execute the API call
            return await apiCall();
        }
        catch (ApiException<TResponse> apiException)
        {
            // Log and return the actual response object
            _logger.LogWarning(apiException, "API error occurred");
            if (apiException.Result != null)
            {
                return apiException.Result;
            }
            throw;
        }
        catch (ApiException apiException)
        {
            // Handle generic ApiException
            _logger.LogError(apiException, "Unexpected API error occurred");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            _logger.LogError(ex, "An unexpected error occurred");
            throw;
        }
    }
	}

