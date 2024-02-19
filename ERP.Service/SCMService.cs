using ERP.Core;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.SCmOrderProduct;
using ERP.Core.Specifications.SupplierSpec;
using ERP.Core.Specifications.UsersSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Service
{
    public class SCMService : ISCMService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISCMService _scmService;

        public SCMService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Supplier Service
        public async Task<ApiResponseDto<SupplierDto>> AddSupplierAsync(Supplier supplier)
        {
            var SupplierValidation = await SupplierValidationAsync(supplier.SupplierEmail, supplier.SupplierPhone , supplier.Id);

            //Check if Supplier is not valid
            if (SupplierValidation.Status != 200) return SupplierValidation;

            //Add Supplier
            await _unitOfWork.Repositry<Supplier>().AddAsync(supplier);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<SupplierDto> { Status = 500, Message = "Failed To Add Supplier" };

            //if product Created
            return new ApiResponseDto<SupplierDto>
            {
                Status = 200,
                Message = "Added Successfully"
            };

        }

        //update Supplier
        public async Task<ApiResponseDto<SupplierDto>> UpdateSupplierAsync(SupplierDto supplier)
        {
            var SupplierValidation = await SupplierValidationAsync(supplier.SupplierEmail, supplier.SupplierPhone, supplier.Id);

            //Check if Supplier is not valid
            if (SupplierValidation.Status != 200) return SupplierValidation;
            //get Supplier
            var specificSupplier = await _unitOfWork.Repositry<Supplier>().GetAsync(supplier.Id);
            if (specificSupplier is not null)
            {
                specificSupplier.SupplierEmail = supplier.SupplierEmail;
                specificSupplier.SupplierPhone = supplier.SupplierPhone;
                specificSupplier.SupplierName = supplier.SupplierName;
            }
            //Add Supplier
            _unitOfWork.Repositry<Supplier>().Update(specificSupplier);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<SupplierDto> { Status = 500, Message = "Failed To Update Supplier" };

            //if product Created
            return new ApiResponseDto<SupplierDto>
            {
                Status = 200,
                Message = "Updated Successfully",
                Data = new SupplierDto()
                {
                    Id = supplier.Id,
                    AddedBy = specificSupplier.Employee.UserName,
                    SupplierEmail = supplier.SupplierEmail,
                    SupplierPhone = supplier.SupplierPhone,
                    SupplierName = supplier.SupplierName
                }
            };

        }
        #endregion
        #region Helper
        public async Task<ApiResponseDto<SupplierDto>> SupplierValidationAsync(string supplierEmail, string supplierPhone , int supplierId)
        {
            //Get specific Supplier By Email
            var supplierByEmailSpec = new SupplierSpec(supplierEmail);
            var specificSupplierByEmail = await _unitOfWork.Repositry<Supplier>().GetWithSpecAsync(supplierByEmailSpec);
            // Check if there is Supplier with same Email 
            if( specificSupplierByEmail is not null && specificSupplierByEmail.Id != supplierId)
                return new ApiResponseDto<SupplierDto> { Status = 400, Message = "Supplier email already exists" };

            //Get specific Supplier By Phone
            var supplierByPhonelSpec = new SupplierSpec(supplierPhone);
            var specificSupplierByPhone = await _unitOfWork.Repositry<Supplier>().GetWithSpecAsync(supplierByPhonelSpec);
            // Check if there is Supplier with same Phone 
            if (specificSupplierByPhone is not null && specificSupplierByPhone.Id != supplierId)
                return new ApiResponseDto<SupplierDto> { Status = 400, Message = "Supplier phone already exists" };

            return new ApiResponseDto<SupplierDto> { Status = 200 };
        }
        #endregion

        #region SCMOrder
        public async Task<ApiResponseDto<ScmOrderProduct>> CreateScmOrderAsync(ScmOrderDto scmOrderProduct, string EmpolyeeId)
        {
            //Check ScmEmployee Exist
            var SpecificSCMEmployee = await CheckUser(EmpolyeeId);
            if (SpecificSCMEmployee is null) return new ApiResponseDto<ScmOrderProduct> { Status = 400, Message = "This SCMEmployee Not Exist" };
            //Check AccEmployee
            var SpecificACCEmployee = await CheckUser(scmOrderProduct.AccEmployeeId);
            if (SpecificACCEmployee is null) return new ApiResponseDto<ScmOrderProduct> { Status = 400, Message = "This ACCEmployee Not Exist" };
            // Check if This Product Already Exist
            foreach (var scmOrderProductDto in scmOrderProduct.Products)
            {
                var product = await _unitOfWork.Repositry<Product>().GetAsync(scmOrderProductDto.ProductId);

                if (product?.Id != scmOrderProductDto.ProductId) return new ApiResponseDto<ScmOrderProduct> { Status = 400, Message = "This Product Not Exist" };

            }
            // Check if all ProductId values are distinct => to avoid exception of DB
            var DistinctProductId = scmOrderProduct.Products.Select(item => item.ProductId).Distinct().Count();
            if (DistinctProductId != scmOrderProduct.Products.Count)
            {
                return new ApiResponseDto<ScmOrderProduct> { Status = 400, Message = "ProductId Should Be Destinct" };
            }


            // Create ScmOrder
            var scmOrder = new ScmOrder()
            {
                Reference = scmOrderProduct.Reference,
                ScmEmployeeId = EmpolyeeId,
                AccEmployeeId = scmOrderProduct.AccEmployeeId,

            };
            // Save ScmOrder into db
            await _unitOfWork.Repositry<ScmOrder>().AddAsync(scmOrder);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<ScmOrderProduct> { Status = 400, Message = "Failed To Create ScmOrder" };


            // Create ScmOrderProduct

            foreach (var scmOrderProductDto in scmOrderProduct.Products)
            {
                var scmOrderProductCreated = new ScmOrderProduct()
                {
                    ScmId = scmOrder.Id,
                    ProductId = scmOrderProductDto.ProductId,
                    Quantity = scmOrderProductDto.Quantity
                };
                await _unitOfWork.Repositry<ScmOrderProduct>().AddAsync(scmOrderProductCreated);
            }

            // check it save

            var result2 = await _unitOfWork.CompleteAsync();
            if (result2 <= 0) return new ApiResponseDto<ScmOrderProduct> { Status = 400, Message = "Failed To Create ScmOrderProduct" };
            else
            {
                return new ApiResponseDto<ScmOrderProduct>
                {
                    Status = 200,
                    Message = " Created Successfuly"
                };

            }
        }

        public async Task<IReadOnlyList<ScmOrder>> GetAllScmOrderAsync()
        {
            var orderSpec = new ScmOrderSpec();
            var orders = await _unitOfWork.Repositry<ScmOrder>().GetAllWithSpecAsync(orderSpec);
            return orders;
        }

        public async Task<IReadOnlyList<ScmOrder>> GetAllScmOrderStatusAsync()
        {
            var status = await _unitOfWork.Repositry<ScmOrder>().GetAllAsync();

            return status;
        }

        #endregion
        public async Task<Employee?> CheckUser(string userId)
        {
            //Get specific User
            var UseSpec = new UserSpec(userId);
            var SpecificUser = await _unitOfWork.Repositry<Employee>().GetWithSpecAsync(UseSpec);
            return SpecificUser;
        }

    }
}
