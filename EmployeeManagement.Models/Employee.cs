using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using Microsoft.AspNetCore.Components.DataAnnotations.Validation;
namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        // [EmailAddress]
      //  [EmailDomainValidator(AllowedDomain = "gmail.com")]
        public string Email { get; set; }
       // [Compare("Email",  ErrorMessage = "Email and Confirm Email must match")]
       // public string ConfirmEmail { get; set; }
        public DateTime DateOfBrith { get; set; }
      //  [ValidateComplexType]
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string PhotoPath { get; set; }
    }
}
