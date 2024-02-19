﻿using System.ComponentModel.DataAnnotations;

namespace ERP.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string EmployeeJob { get; set; }

        [Required]
        public string AddedById { get; set; }

        [Required]
        public int EmployeeDepartmentId { get; set; }

        [Required]
        public string Role { get; set;}

    }
}
