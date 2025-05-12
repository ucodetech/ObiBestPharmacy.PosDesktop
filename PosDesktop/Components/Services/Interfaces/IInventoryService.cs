using ApiClient;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace PosDesktop . Components . Services . Interfaces
{
	public interface IInventoryService
	{
		#region category
		Task<CategoryResponseApiResponse> CreateCategoryAsync (CategoryRequest request );
		Task<CategoryResponseApiResponse> GetCategoryByIdAsync ( Guid id );
		Task<CategoryResponseIEnumerableApiResponse> GetAllCategoriesAsync ( );
		Task<CategoryResponseApiResponse> UpdateCategoryAsync ( Guid id, CategoryRequest request );
		Task<BaseResponse> DeleteCategoryAsync ( Guid id );
		#endregion

		#region product
		Task<BaseResponse> UpsertProduct ( ProductRequest request );
		Task<BaseResponse> DeleteProduct ( Guid Id );
		Task<ProductResponseIEnumerableApiResponse> GetAllProduct ();
		Task<ProductResponseApiResponse> GetProductById ( Guid Id );
		Task<ProductResponseApiResponse> GetProductByReferenceNo ( string ReferenceNo );

		#endregion 
		}
	}
