using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    // View Model
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        [MaxLength(50, ErrorMessage = "Max Length is 50")]
        [MinLength(5, ErrorMessage = "Min Length is 5")]
        public string Name { get; set; }
        [Range(20, 50)]

        public int Age { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }

        public int? DepartmentId { get; set; }  // Foriegn Key   --> Allow Null

        // Navigational Property --> one
        public Department Department { get; set; }
    }
}
