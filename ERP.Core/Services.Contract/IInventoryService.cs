using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERP.Core.Services.Contract
{
    public interface IInventoryService
    {
        #region Category Services
        //Get All Categoris
        Task<IReadOnlyList<ParentCategory>> GetCategoriesAsync();
        //Create parent Category 
        Task<ApiResponseDto<ParentCategoryDto>> CreateParentcategoryasync(ParentCategory parentCategory);
        //create sub category
        Task<ApiResponseDto<SubCategoryDto>> CreateSubcategoryasync(SubCategory subCategory);
        Task<ApiResponseDto<ParentCategoryDto>> UpdateParentCategoryAsync(ParentCategory parentCategor);
        Task<ApiResponseDto<SubCategoryDto>> UpdateSubCategoryAsync(SubCategory subCategory);

        Task<ApiResponseDto<SubCategoryDto>> DeleteSubcategoryAsync(int subCategoryId);
        Task<ParentCategory?> GetParentCategoryAsync(int id);
        Task<ApiResponseDto<ParentCategoryDto>> DeleteParentCategoryAsync(int id);
        #endregion


        #region ProductServices

        Task<IReadOnlyList<Replenishment>> GetAllProductStockOutsAsync(ProductSpecParams productSpecParams);
        #endregion


      

        Task<ApiResponseDto<InventoryOrder>> CreateInventoryOrderAsync(OrderDto order , string Id);
        Task<ApiResponseDto<OrdertToReturnDto>> UpdateInventoryOrderAsync(OrderToUpdateDto order, int orderId);

       Task<int> GetProductsNumberAsync();
        Task<int> GetReplenishmentNumberAsync();
        Task<IEnumerable<Product>> GetProductsAsync(ProductSpecParams productSpecParams);
        Task<int> GetCountOfProductAsync(ProductSpecParams specParams);
        Task<int> GetCountStockoutAsync(ProductSpecParams specParams);
        Task<Product?> GetProductAsync(int productId);
        //Task<Product> CreateProductAsync(Product product);
        Task<ApiResponseDto<ProductReturnDto>> UpdateProductAsync(Product product);

        Task<ApiResponseDto<ProductReturnDto>> CreateProductAsync(Product product , string Id);
        #region OrdereErvices
       
        //Task<ApiResponseDto<OrdertToReturnDto>> UpdateInventoryOrderAsync(OrderToUpdateDto order, int orrderId);

        //Task<IReadOnlyList<InventoryOrder>> GetOrdersStatusAsync();

        Task<int> GetInventoryOrdersNumberAsync();
        Task<int> GetScmOrdersNumberAsync();
        #endregion

        Task<IEnumerable<Transfer>> GetTransfersAsync(ProductSpecParams productSpecParams);
        Task<int> GetCountOfTransfersAsync(ProductSpecParams specParams);
        Task<IEnumerable<InventoryOrder>> GetAllInventoryOrdersAsync(ProductSpecParams productSpecParams);
        Task<int> GetCountOfInventoryOrderAsync();
        Task<ApiResponseDto<InventoryOrder>> GetInventoryOrderAsync(int id);
    }
}
