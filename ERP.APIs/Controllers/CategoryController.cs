using AutoMapper;
using ERP.APIs.Errors;
using ERP.APIs.Extensions;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class CategoryController : BaseController
    {   
       

        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public CategoryController(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }




        #region Add Parent Category
        [HttpPost("ParentCategory")] //Post :/api/Category/ParentCategory
        public async Task<ActionResult<ApiResponseDto<ParentCategoryDto>>> CreateParentcategory([FromBody] ParentCategoryDto parentCategoryModel)
        {
            var mappedParentCategory = _mapper.Map<ParentCategoryDto, ParentCategory>(parentCategoryModel);
            var parentcategory = await _inventoryService.CreateParentcategoryasync(mappedParentCategory);
            if (parentcategory.Status != 200)
                return BadRequest(new { parentcategory.Status, parentcategory.Message });
            parentcategory.Data = _mapper.Map<ParentCategory, ParentCategoryDto>(mappedParentCategory);
            return Ok(parentcategory);
        }

        #endregion

        #region Add Parent Category
        [HttpPost("SubCategory")] //Post :/api/Category/ParentCategory
        public async Task<ActionResult<ApiResponseDto<ParentCategoryDto>>> CreateSubcategory([FromBody] SubCategoryDto subCategoryModel)
        {
            if (subCategoryModel.ParentCategoryId <= 0) return BadRequest(new { Status = 400, Message = "ParentCategoryId must more than zero" });
            var mappedSubCategory = _mapper.Map<SubCategoryDto, SubCategory>(subCategoryModel);
            var subCategory = await _inventoryService.CreateSubcategoryasync(mappedSubCategory);
            if (subCategory.Status != 200)
                return BadRequest(new { subCategory.Status, subCategory.Message });
            subCategory.Data = _mapper.Map<SubCategory, SubCategoryDto>(mappedSubCategory);
            return Ok(subCategory);
        }

        #endregion

        #region  EditParentCategory

        [HttpPut("ParentCategory/{id}")]   //Post :/api/Category/ParentCategory/{id}
        public async Task<ActionResult<ApiResponseDto<ParentCategory>>> EditParentCategory(int Id, [FromBody] ParentCategoryDto Category)
        {

            var MappedCategory = _mapper.Map<ParentCategoryDto, ParentCategory>(Category);
                MappedCategory.Id = Id;
            var category = await _inventoryService.UpdateParentCategoryAsync(MappedCategory);
            if (category.Status != 200) return BadRequest(new { category.Status, category.Message });

            category.Data = _mapper.Map<ParentCategory,ParentCategoryDto>(MappedCategory);

            return Ok(category);
           


        }

        [HttpPut("SubCategory/{id}")]   //Post :/api/Category/SubCategory/{id}
        public async Task<ActionResult<ApiResponseDto<SubCategoryDto>>> EditSubCategory(int Id, [FromBody] SubCategoryDto Category)
        {

            var MappedCategory = _mapper.Map<SubCategoryDto, SubCategory>(Category);
            MappedCategory.Id = Id;
            var category = await _inventoryService.UpdateSubCategoryAsync(MappedCategory);
            if (category.Status != 200) return BadRequest(new { category.Status, category.Message });

            category.Data = _mapper.Map<SubCategory, SubCategoryDto>(MappedCategory);

            return Ok(category);



        }



        #endregion


        #region GetAllCategory
        [HttpGet]    //Post :/api/Category
        public async Task<ActionResult<IReadOnlyList<CategoryToReturnDto>>> GetAllCategories()
        {
            var allCategories = await _inventoryService.GetCategoriesAsync();
            return Ok(_mapper.Map<IReadOnlyList<ParentCategory>, IReadOnlyList<CategoryToReturnDto>>(allCategories));
        }
        #endregion

        #region Delete ParentCategory

        [HttpDelete("ParentCategory/{id}")]
        public async Task<ActionResult<ApiResponseDto<ParentCategoryDto>>> DeleteParentcategory(int id)
        {
            // Check if id is valid
            if (id <= 0)
                return BadRequest(new { Status = 400, Message = "Invalid ParentCategory ID" });

            // Call the service method to delete the SubCategory
            var ParentCategory = await _inventoryService.DeleteParentCategoryAsync(id);

            // Check the status returned by the service
            if (ParentCategory.Status != 200)
                return BadRequest(new { ParentCategory.Status, ParentCategory.Message });

            return Ok(new { ParentCategory.Status, ParentCategory.Message });
        }



        #endregion


        #region Delete SubCategory
        [HttpDelete("SubCategory/{id}")]
        public async Task<ActionResult<ApiResponseDto<SubCategoryDto>>> DeleteSubcategory(int id)
        {
            // Check if id is valid
            if (id <= 0)
                return BadRequest(new { Status = 400, Message = "Invalid SubCategory ID" });

            // Call the service method to delete the SubCategory
            var subCategory = await _inventoryService.DeleteSubcategoryAsync(id);

            // Check the status returned by the service
            if (subCategory.Status != 200)
                return BadRequest(new { subCategory.Status, subCategory.Message });

            return Ok(new { subCategory.Status, subCategory.Message });
        }


        #endregion



    }
}
