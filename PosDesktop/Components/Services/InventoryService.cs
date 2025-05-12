using ApiClient;
using Microsoft . Extensions . Logging;
using PosDesktop . Components . Services . Interfaces;
using PosDesktop . Helpers;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace PosDesktop . Components . Services;

public class InventoryService : IInventoryService
	{
	private readonly ILogger<InventoryService> _logger;
	private readonly DesktopApiClient _client;
	private readonly ApiClientWrapper _clientWrapper;

	public InventoryService (ILogger<InventoryService> logger, DesktopApiClient client, ApiClientWrapper clientWrapper)
		{
		_logger = logger;
		_client = client;
		_clientWrapper = clientWrapper;
			
		}

	#region category
	public async Task<CategoryResponseApiResponse> CreateCategoryAsync ( CategoryRequest request )
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.CreateCategoryAsync(request));
		return response;
		
		}

	public async Task<CategoryResponseApiResponse> UpdateCategoryAsync ( Guid id, CategoryRequest request )
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.UpdateCategoryAsync(id, request));
		return response;
		
		}

	public async Task<BaseResponse> DeleteCategoryAsync ( Guid id )
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.DeleteCategoryAsync(id));
		return response;
		}

	public async Task<CategoryResponseIEnumerableApiResponse> GetAllCategoriesAsync ()
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.GetAllCategoriesAsync());
		return response;
		}

	public async Task<CategoryResponseApiResponse> GetCategoryByIdAsync ( Guid id )
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.GetCategoryAsync(id));
		return response;
	}
		
	#endregion

	#region products
	public async Task<ProductResponseIEnumerableApiResponse> GetAllProduct ()
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Product_getAllAsync());
		return response;
		
		}

	public async Task<ProductResponseApiResponse> GetProductById ( Guid Id )
	{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Product_getByIdAsync(Id));
		return response;
		
		}

	public async Task<ProductResponseApiResponse> GetProductByReferenceNo ( string ReferenceNo )
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Product_getByReferenceNoAsync(ReferenceNo));
		return response;
		
		}

	public async Task<BaseResponse> UpsertProduct ( ProductRequest request )
		{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Product_upsertAsync(request));
		return response;
		
		}
	public async Task<BaseResponse> DeleteProduct ( Guid Id )
	{
		var response = await _clientWrapper.ExecuteApiCall(() =>  _client.Product_deleteAsync(Id));
		return response;
		
		}
	}

#endregion

