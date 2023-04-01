using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class Seller
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
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
