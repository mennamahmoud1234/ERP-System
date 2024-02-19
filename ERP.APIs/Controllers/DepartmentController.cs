using AutoMapper;
using ERP.APIs.Extensions;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIs.Controllers
{
    [UserAuthorize]
    public class DepartmentController : BaseController
    {
        private readonly IHRServices _HRServices;
        private readonly IMapper _mapper;

        public DepartmentController(IHRServices HRServices ,IMapper mapper)
        {
            _HRServices = HRServices;
             _mapper = mapper;
        }


        #region Get All Department

        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult<IReadOnlyList<DepartmentToReturnDto>>> GetAllDepatrment()
        {
            var department = await _HRServices.GetDepartments();
            if(department != null) { 
            return Ok( _mapper.Map<IReadOnlyList<Department>, IReadOnlyList<DepartmentToReturnDto>>(department));
            
            }
            else
            {
                 return new List<DepartmentToReturnDto>();
            }
             
        }
        #endregion


        #region Get Specific Department

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentToReturnDto>> GetDepatrment( int id)
        {
            var department = await _HRServices.GetSpesificDepartment(id);
            if (department != null)
            {
                return Ok(_mapper.Map<DepartmentToReturnDto>(department));

            }
            else
            {
                return new DepartmentToReturnDto();
            }

        }
        #endregion

        #region  Create Department
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<CreatedDepartmentReturnDto>>> CreateDepartment([FromBody] DepartmentDto dto)
        {
            var MappedDepartment =  _mapper.Map<Department>(dto);
            var dept= await _HRServices.CreateDepartment(MappedDepartment);
            if (dept.Status !=200)
            {
                return Ok(new ApiResponseDto<CreatedDepartmentReturnDto>() { Status=dept.Status, Message = dept.Message });
            }
            else
            {
                return Ok(new ApiResponseDto<CreatedDepartmentReturnDto>() { Status = 200, Message = "Department Created Successuflly",Data=_mapper.Map<CreatedDepartmentReturnDto> (dept.Data)});
            }

        }
        #endregion

    }
}
