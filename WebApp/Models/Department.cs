using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class Department
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); 

        public Department() { }
        public Department(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
