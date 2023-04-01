using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class DepartmentService
    {
        private readonly WebAppContext _context;

        public DepartmentService(WebAppContext context)
        {
            _context = context;
        }

        public List<Department> FindAll() => _context.Department.OrderBy(dep => dep.Name).ToList();
    }
}
