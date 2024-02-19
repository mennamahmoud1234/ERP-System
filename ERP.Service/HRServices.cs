using ERP.Core;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Services.Contract;
using ERP.Core.Specifications.DepartmentSpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Service
{
     public class HRServices :IHRServices 
    {
        private readonly IUnitOfWork _unitOfWork;

        public HRServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        #region Get All Department

        public async Task<IReadOnlyList<Department>> GetDepartments()
        {
            var deparmtents = await _unitOfWork.Repositry<Department>().GetAllWithSpecAsync(new DepartmentSpec());
            if(deparmtents !=null)
                return deparmtents;
            else return new List<Department>(); 
        }


        #endregion

        #region Get Specific Department

        public async Task<Department> GetSpesificDepartment(int id)
        {
            var deparmtent = await _unitOfWork.Repositry<Department>().GetWithSpecAsync(new DepartmentSpec(id));
            if (deparmtent != null)
                return deparmtent;
            else
                return new Department();

        }
        #endregion

        #region Create Department

        public async Task<ApiResponseDto<Department>> CreateDepartment(Department department)
        { 
            // check if this department name already exist
            var deptName= await _unitOfWork.Repositry<Department>().GetWithSpecAsync(new DepartmentSpec(department.DepartmentName));
            if(deptName?.DepartmentName != null) { return new ApiResponseDto<Department>() { Status = 400, Message = "This DepartmentName Already Exist" }; };
            // check if this Parentdepartment  already exist
            var parentDept = await _unitOfWork.Repositry<Department>().GetAsync(department.ParentDepartmentId.Value);
            if (parentDept == null) { return new ApiResponseDto<Department>() { Status = 400, Message = "This Parent Department Not Exist" }; };


            await _unitOfWork.Repositry<Department>().AddAsync(department);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return new ApiResponseDto<Department> { Status = 500, Message = "Failed To Create Department" };

            //if departemnt created
            return new ApiResponseDto<Department>
            {
                Status = 200,
                Message = " Created Successfully",
                Data = department
            };

        }
        #endregion

    }
}
