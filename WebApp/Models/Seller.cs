using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApp.Models
{
    public class Seller
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1} characters")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(100.00, 50000.00, ErrorMessage = "{0} should be between {1} and {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Department")]
        public long DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


        public Seller() { }

        public Seller(long id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSalesRecord(SalesRecord record) => Sales.Add(record);

        public void RemoveSalesRecord(SalesRecord record) => Sales.Remove(record);
        
        public double TotalSales(DateTime initial, DateTime final) => 
            Sales.Where(record => record.Date >= initial && record.Date <= final)
                 .Aggregate(0.0, (acc, curr) => acc + curr.Amount);
        
    }
}
