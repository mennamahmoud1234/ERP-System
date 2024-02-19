using ERP.Core;
using ERP.Core.Entities;
using ERP.Core.Services.Contract;
using Microsoft.EntityFrameworkCore;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Dtos;
using Microsoft.AspNetCore.Identity;
using ERP.Core.Specifications;
using ERP.Core.Identity;
using ERP.Core.Specifications.ReplenishmentSpec;
using ERP.Core.RepositryContract;
using Microsoft.AspNetCore.Http.HttpResults;
using ERP.Core.Specifications.UsersSpec;
using ERP.Core.Specifications.TransfersSpec;
using ERP.Core.Specifications.CategorySpec;
using System.Xml.Linq;
using ERP.Core.Specifications.SCmOrderProduct;
using ERP.Core.Specifications.OrderSpec;

namespace ERP.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepo _categoryRepo;

        public InventoryService(IUnitOfWork unitOfWork, ICategoryRepo categoryRepo)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
        }
        #region Category services
        public async Task<IReadOnlyList<ParentCategory>> GetCategoriesAsync()
        {
            var allCategories = await GetParentCategoryWithCategories();
            return allCategories;

        }
        public async Task<ApiResponseDto<ParentCategoryDto>> CreateParentcategoryasync(ParentCategory parentCategory)
        {
            //Get Specific Parent Category
            var SpecificParentCategory = await GetParentCategoryByName(parentCategory.ParentCategoryName);
            // Ckeck if this Parent Category already exist 
            if (SpecificParentCategory != null) return new ApiResponseDto<ParentCategoryDto>() { Status = 400, Message = "Parent category name already exists" };

            //add to db
            await _unitOfWork.Repositry<ParentCategory>().AddAsync(parentCategory);

            // 6. Save to Database
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<ParentCategoryDto>() { Status = 400, Message = "Invaliv adding parent category" };

            return new ApiResponseDto<ParentCategoryDto>()
            {
                Status = 200,
                Message = "Parent category created Successfully"
            };
        }

        ///Create sub category <summary>
        public async Task<ApiResponseDto<SubCategoryDto>> CreateSubcategoryasync(SubCategory subCategory)
        {
            //Get Specific Parent Category
            var SpecificParentCategory = await GetParentCategoryById(subCategory.ParentCategoryId);
            // Ckeck if this Parent Category already exist 
            if (SpecificParentCategory is null) return new ApiResponseDto<SubCategoryDto>() { Status = 400, Message = "ParentCategoryId not found" };

            //get sub categories by name for specific ParentCategoryId
            var SubCatergoriesForSpacificParentId = await GetSubCategoriesByNameForSpecificParentId(subCategory.SubCategoryName, subCategory.ParentCategoryId);
            if (SubCatergoriesForSpacificParentId.Count() > 0) return new ApiResponseDto<SubCategoryDto>() { Status = 400, Message = "There is subCategory with the same name and the same ParentCategoryId)" };

            //add to db
            await _unitOfWork.Repositry<SubCategory>().AddAsync(subCategory);

            // 6. Save to Database
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<SubCategoryDto>() { Status = 400, Message = "Invaliv adding  SubCategory" };

            return new ApiResponseDto<SubCategoryDto>()
            {
                Status = 200,
                Message = "SubCategory created Successfully"
            };
        }





        

        public async Task<ApiResponseDto<ParentCategoryDto>> UpdateParentCategoryAsync(ParentCategory parentCategory)
        {   
            //check if this category exist
             var ParentCatg = await _unitOfWork.Repositry<ParentCategory>().GetAsync(parentCategory.Id);

             if (ParentCatg is  null) return new ApiResponseDto<ParentCategoryDto> { Status = 404, Message = "this category not exist" };
            // ckeck if name of category already exist
            var CatgName = new ParentCategorySpec(parentCategory.ParentCategoryName);
              var name=await _unitOfWork.Repositry<ParentCategory>().GetWithSpecAsync(CatgName);
     
            if (name is not null && name.Id!=parentCategory.Id) return new ApiResponseDto<ParentCategoryDto> { Status = 404, Message = "This Category Already Exist" };
            //update
            if (ParentCatg is not null)
            {
                ParentCatg.ParentCategoryName = parentCategory.ParentCategoryName;

                _unitOfWork.Repositry<ParentCategory>().Update(ParentCatg);
            }
            // save to db
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<ParentCategoryDto> { Status = 400, Message = "Failed To Update ParentCategory" };

            //if product Created
            return new ApiResponseDto<ParentCategoryDto>
            {
                Status = 200,
                Message = "UpdatedS Successfully"
            };
        }

        public  async Task<ApiResponseDto<SubCategoryDto>> UpdateSubCategoryAsync(SubCategory subCategory)
        {

            //check if this category exist
            var subCateg = await _unitOfWork.Repositry<SubCategory>().GetAsync(subCategory.Id);
            if (subCateg is null) return new ApiResponseDto<SubCategoryDto> { Status = 404, Message = "This Category Not Exist" };

            //check if this Parentcategory id  exist
            var ParentCatg = await _unitOfWork.Repositry<ParentCategory>().GetAsync(subCategory.ParentCategoryId);

            if (ParentCatg is null) return new ApiResponseDto<SubCategoryDto> { Status = 404, Message = "This ParentCategory Not Exist" };


            // Check if this name already exist in the same category 
            var Namespec = new SubCategorySpec(subCategory.SubCategoryName, subCategory.ParentCategoryId);
            var name = await _unitOfWork.Repositry<SubCategory>().GetWithSpecAsync(Namespec);
            if ((name?.Id!= subCateg.Id && name!=null))
            {
                return new ApiResponseDto<SubCategoryDto> { Status = 404, Message = "This SubCategoryName Already Exist " };

            }
            // ckeck if name of SubCategory already exist in ParentCategory
             var spec = new SubCategorySpec(subCategory.SubCategoryName, subCategory.ParentCategoryId, subCategory.Id);
             var subName = await _unitOfWork.Repositry<SubCategory>().GetWithSpecAsync(spec);
             if (subName is not null) return new ApiResponseDto<SubCategoryDto> { Status = 200, Message = "Updated Successfully" };
            // update SubCategory
            subCateg.SubCategoryName=subCategory.SubCategoryName;
            subCateg.ParentCategoryId=subCategory.ParentCategoryId;
            _unitOfWork.Repositry<SubCategory>().Update(subCateg);
            //
            // save to db
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<SubCategoryDto> { Status = 400, Message = "Failed To Update SubCategory" };

            //if product Created
            return new ApiResponseDto<SubCategoryDto>
            {
                Status = 200,
                Message = "Updated Successfully"
            };



        }

        #endregion


    

        #region ProductServices
        public async Task<IReadOnlyList<Replenishment>> GetAllProductStockOutsAsync(ProductSpecParams productSpecParams)
        {
            var specReplenishment = new ReplenishmentProductStockoutSpec(productSpecParams);

            return await _unitOfWork.Repositry<Replenishment>().GetAllWithSpecAsync(specReplenishment);

        }
        #endregion


        #region InventoryOrder

        public async Task<ApiResponseDto<InventoryOrder>> CreateInventoryOrderAsync(OrderDto order, string Id)
        {
            //Check Employee Exist
            var SpecificEmployee = await CheckUser(Id);

            //Check AccEmployee Exist
            var SpecificAccEmployee = await CheckUser(order.AccEmployeeId);
            if (SpecificAccEmployee == null)
            {
                return new ApiResponseDto<InventoryOrder>() { Status = 400, Message = "This AccEmployee Not exist" };
            }
            // Check if this Product Already exist
            var product = await _unitOfWork.Repositry<Product>().GetAsync(order.ProductId);
           
            if (product == null)
            {
                return new ApiResponseDto<InventoryOrder>() { Status = 400, Message = "This ProductId Not exist" };
            }
            // Add to db
            
                var Invorder = new InventoryOrder()
                {
                    ProductId= order.ProductId,
                    AccEmployee= order.AccEmployeeId,
                    Quantity= order.Quantity,
                    InventoryEmployee =Id,
                    Reference= order.Reference

                };

               await _unitOfWork.Repositry<InventoryOrder>().AddAsync(Invorder);

                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0) return new ApiResponseDto<InventoryOrder> { Status = 500, Message = "Failed To Create InventoryOrder" };
                else
                {
                return new ApiResponseDto<InventoryOrder>
                {
                    Status = 200,
                    Message = " Created InventoryOrder Successfuly"
                    };

                }


            
            
            
            

        }

        public async Task<ApiResponseDto<OrdertToReturnDto>> UpdateInventoryOrderAsync(OrderToUpdateDto order , int orderId)
        {
            //Check AccEmployee Exist
            var SpecificAccEmployee = await CheckUser(order.AccEmployeeId);
            if (SpecificAccEmployee == null)
            {
                return new ApiResponseDto<OrdertToReturnDto>() { Status = 400, Message = "This AccEmployee Not exist" };
            }
            //Get Inventory Order
            var SpecificInventoryOrder = await _unitOfWork.Repositry<InventoryOrder>().GetAsync(orderId);
            SpecificInventoryOrder.Quantity = order.Quantity;
            SpecificInventoryOrder.Reference = order.Reference;
            SpecificInventoryOrder.AccEmployee = order.AccEmployeeId;
            // Update to db

            _unitOfWork.Repositry<InventoryOrder>().Update(SpecificInventoryOrder);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return new ApiResponseDto<OrdertToReturnDto> { Status = 500, Message = "Failed To Update InventoryOrder" };
            else
            {
                return new ApiResponseDto<OrdertToReturnDto>
                {
                    Status = 200,
                    Message = " Created InventoryOrder Successfuly",
                    Data = new OrdertToReturnDto()
                    {
                        Quantity = order.Quantity,
                        Reference = order.Reference,
                        AccEmployeeId = order.AccEmployeeId,
                        InventoryEmployeeId =  SpecificInventoryOrder.InventoryEmployee,
                        OrderId = orderId,
                        ProductId = SpecificInventoryOrder.ProductId
                    }
                };

            }







        }

        public async Task<int> GetInventoryOrdersNumberAsync()
        {
            int InventoryOrdersCount = await _unitOfWork.Repositry<InventoryOrder>().GetCountAsync();
            return InventoryOrdersCount;
        }


        public async Task<int> GetScmOrdersNumberAsync()
        {
            int ScmOrdersCount = await _unitOfWork.Repositry<ScmOrder>().GetCountAsync();
            return ScmOrdersCount;
        }


        public async Task<IEnumerable<InventoryOrder>> GetAllInventoryOrdersAsync(ProductSpecParams productSpecParams)
        {
            var Spec = new InventoryOrderSpecification(productSpecParams);
            var inventoryOrders = await _unitOfWork.Repositry<InventoryOrder>().GetAllWithSpecAsync(Spec);
            return inventoryOrders;
        }

        public async Task<int> GetCountOfInventoryOrderAsync()
        {
            var Count = await _unitOfWork.Repositry<InventoryOrder>().GetCountAsync();
            return Count;
        }

        public async Task<ApiResponseDto<InventoryOrder>> GetInventoryOrderAsync(int id)
        {
            var spec = new InventoryOrderWithAllNavigationsSpecifications(id);
            var inventoryOrder = await _unitOfWork.Repositry<InventoryOrder>().GetWithSpecAsync(spec);
            if (inventoryOrder == null)
                return new ApiResponseDto<InventoryOrder>
                {
                    Status = 404,
                    Message = "Order not found"
                };

            return new ApiResponseDto<InventoryOrder>
            {
                Status = 200,
                Message = "Get Order Succeeded",
                Data = inventoryOrder
            }; 
        }

        #endregion

        public async Task<int> GetProductsNumberAsync()
        {
            int productCount = await _unitOfWork.Repositry<Product>().GetCountAsync();
            return productCount;
        }

        public async Task<int> GetReplenishmentNumberAsync()
        {
            int ReplenishmentCount = await _unitOfWork.Repositry<Replenishment>().GetCountAsync();
            return ReplenishmentCount;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithCategorySpecifications(productSpecParams);
            var Products = await _unitOfWork.Repositry<Product>().GetAllWithSpecAsync(spec);
            return Products;
        }

        public async Task<int> GetCountOfProductAsync(ProductSpecParams specParams)
        {
            var countSpec = new ProductWithFilterationForCountSpecifications(specParams);
            var Count = await _unitOfWork.Repositry<Product>().GetCountAsync(countSpec);
            return Count;
        }

        public async Task<int> GetCountStockoutAsync(ProductSpecParams specParams)
        {
            var countSpec = new ReplenishmentWithFilterationForCountSpecifications(specParams);
            var Count = await _unitOfWork.Repositry<Replenishment>().GetCountAsync(countSpec);
            return Count;
        }
        public async Task<Product?> GetProductAsync(int productId)
        {
            var spec = new ProductsWithCategorySpecifications(productId);
            var Product = await _unitOfWork.Repositry<Product>().GetWithSpecAsync(spec);
            return Product;
        }

        //public async Task<InventoryOrderProduct?> GetInventoryOrderProduct(int orderId)
        //{
        //    var spec = new InventoryOrderProductSpecification(orderId);
        //    var inventoryOrderProduct = await _unitOfWork.Repositry<InventoryOrderProduct>().GetWithSpecAsync(spec);
        //    return inventoryOrderProduct;
        //}

        public async Task<Employee?> CheckUser(string userId)
        {
            //Get specific User
            var UseSpec = new UserSpec(userId);
            var SpecificUser = await _unitOfWork.Repositry<Employee>().GetWithSpecAsync(UseSpec);
            return SpecificUser;
        }
        public async Task<ApiResponseDto<ProductReturnDto>> UpdateProductAsync(Product product)
        {
            var CheckProduct = await ProductValidationAsync(product.ProductName, product.Id, product.AddedBy, product.SubCategoryId);
            //Check if Product is not valid
            if (CheckProduct.Status != 200) return CheckProduct;

            //Update Product
            var SpecificProduct = await _unitOfWork.Repositry<Product>().GetAsync(product.Id);
            if (SpecificProduct != null)
            {
                SpecificProduct.ProductName = product.ProductName;
                SpecificProduct.ProductOnHand = product.ProductOnHand;
                SpecificProduct.ProductBarcode = product.ProductBarcode;
                SpecificProduct.ProductInComing = product.ProductInComing;
                SpecificProduct.ProductOutGoing = product.ProductOutGoing;
                SpecificProduct.ProductSellPrice = product.ProductSellPrice;
                SpecificProduct.ProductCostPrice = product.ProductCostPrice;
                SpecificProduct.ActiveOrder = product.ActiveOrder;
                SpecificProduct.AddedBy = product.AddedBy;
                SpecificProduct.SubCategoryId = product.SubCategoryId;
                _unitOfWork.Repositry<Product>().Update(SpecificProduct);
            }
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<ProductReturnDto> { Status = 400, Message = "Invalid product update" };

            //if product updated
            return new ApiResponseDto<ProductReturnDto>
            {
                Status = 200,
                Message = "updated successfully"
            };

        }

        public async Task<ApiResponseDto<ProductReturnDto>> ProductValidationAsync(string productName, int productId, string addedById, int categoryId)
        {
            //Get specific Product
            var ProductSpec = new OneProductWithNameSpecifications(productName);
            var SpecificProduct = await _unitOfWork.Repositry<Product>().GetWithSpecAsync(ProductSpec);
            // Check if there is product with same Name
            if (SpecificProduct is not null && SpecificProduct.Id != productId)
                return new ApiResponseDto<ProductReturnDto> { Status = 400, Message = "Invalid name , There is a product with the same name" };


            //Check specific User
            var SpecificUser = await CheckUser(addedById);
            // Check if there is User with same Id
            if (SpecificUser is null)
                return new ApiResponseDto<ProductReturnDto> { Status = 400, Message = "Invalid AddedById" };

            //Get specific Category
            var SpecificCategory = await _unitOfWork.Repositry<SubCategory>().GetAsync(categoryId);
            // Check if there is Category with same CategoryId
            if (SpecificCategory is null)
                return new ApiResponseDto<ProductReturnDto> { Status = 400, Message = "Invalid CategoryId" };

            return new ApiResponseDto<ProductReturnDto> { Status = 200 };
        }


        public async Task<ApiResponseDto<ProductReturnDto>> CreateProductAsync(Product product , string Id)
        {
            var CheckProduct = await ProductValidationAsync(product.ProductName, product.Id, Id, product.SubCategoryId);
            //Check if Product is not valid
            if (CheckProduct.Status != 200) return CheckProduct;

            product.AddedBy = Id;
            //Create Product
            await _unitOfWork.Repositry<Product>().AddAsync(product);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<ProductReturnDto> { Status = 400, Message = "Failed To Add Product" };

            //if product Created
            return new ApiResponseDto<ProductReturnDto>
            {
                Status = 200,
                Message = "Created Successfully"
            };

        }

      

        public async Task<IEnumerable<Transfer>> GetTransfersAsync(ProductSpecParams productSpecParams)
        {
            var spec = new TransfersWithTransferProductsSpecifications(productSpecParams);
            var Transfers = await _unitOfWork.Repositry<Transfer>().GetAllWithSpecAsync(spec);
            return Transfers;
        }

        public async Task<int> GetCountOfTransfersAsync(ProductSpecParams specParams)
        {
            var Count = await _unitOfWork.Repositry<Transfer>().GetCountAsync();
            return Count;
        }
        public async Task<IReadOnlyList<ParentCategory>> GetParentCategoryWithCategories()
        {
            //Get  Parent Category with sub
            var ParentCategoryWithSubSpec = new ParentCategorySpec();
            var AllCategories = await _unitOfWork.Repositry<ParentCategory>().GetAllWithSpecAsync(ParentCategoryWithSubSpec);
            return AllCategories;
        }
        public async Task<ParentCategory> GetParentCategoryByName(string name)
        {
            //Get Specific Parent Category
            var ParentCategorySpec = new ParentCategorySpec(name);
            var SpecificParentCategory = await _unitOfWork.Repositry<ParentCategory>().GetWithSpecAsync(ParentCategorySpec);
            return SpecificParentCategory;
        }
        public async Task<ParentCategory> GetParentCategoryById(int parentCategoryId)
        {
            //Get Specific Parent Category
            var ParentCategorySpec = new ParentCategorySpec(parentCategoryId);
            var SpecificParentCategory = await _unitOfWork.Repositry<ParentCategory>().GetWithSpecAsync(ParentCategorySpec);
            return SpecificParentCategory;
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByNameForSpecificParentId(string subCategoryName,int parentCategoryId)
        {
            //Get Specific Parent Category
            var SubCategorySpec = new SubCategorySpec(subCategoryName , parentCategoryId);
            var SpecificSubCategory = await _unitOfWork.Repositry<SubCategory>().GetAllWithSpecAsync(SubCategorySpec);
            return SpecificSubCategory;
        }


        public async Task<ApiResponseDto<SubCategoryDto>> DeleteSubcategoryAsync(int subCategoryId)
        {
            var response = new ApiResponseDto<SubCategoryDto>();

            try
            {
                // Check if the SubCategory exists
                var existingSubCategory = await _unitOfWork.Repositry<SubCategory>().GetAsync(subCategoryId);

                if (existingSubCategory == null)
                {
                    response.Status = 404;
                    response.Message = "SubCategory not found";
                    return response;
                }

                // Call your repository or data access layer to delete the SubCategory
                await _unitOfWork.Repositry<SubCategory>().DeleteAsync(existingSubCategory);

                return new ApiResponseDto<SubCategoryDto>
                {
                    Status = 200,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                response.Status = 500;
                response.Message = "Internal Server Error";
                return response;
            }
        }

        public async Task<ParentCategory?> GetParentCategoryAsync(int id)
        {
            var parentCategory = await _unitOfWork.Repositry<ParentCategory>().GetAsync(id);
            return parentCategory;
        }

        public async Task<ApiResponseDto<ParentCategoryDto>> DeleteParentCategoryAsync(int id)
        {
            var response = new ApiResponseDto<ParentCategoryDto>();

            try
            {
                // Check if the SubCategory exists
                var existingParentCategory = await _unitOfWork.Repositry<ParentCategory>().GetAsync(id);

                if (existingParentCategory == null)
                {
                    response.Status = 404;
                    response.Message = "ParentCategory not found";
                    return response;
                }

                // Call your repository or data access layer to delete the SubCategory
                await _unitOfWork.Repositry<ParentCategory>().DeleteAsync(existingParentCategory);

                return new ApiResponseDto<ParentCategoryDto>
                {
                    Status = 200,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                response.Status = 500;
                response.Message = "Internal Server Error";
                return response;
            }
        }


       
    }
}
