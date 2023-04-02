using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class SalesRecordService
    {
        private readonly WebAppContext _context;

        public SalesRecordService(WebAppContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = _context.SalesRecord.Select(record => record);

            if (minDate.HasValue)
            {
                result = result.Where(record => record.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(record => record.Date <= maxDate.Value);
            }

            return await result
                .Include(record => record.Seller)
                .Include(record => record.Seller.Department)
                .OrderByDescending(record => record.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = _context.SalesRecord.Select(record => record);

            if (minDate.HasValue)
            {
                result = result.Where(record => record.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(record => record.Date <= maxDate.Value);
            }

            return await result
                .Include(record => record.Seller)
                .Include(record => record.Seller.Department)
                .OrderByDescending(record => record.Date)
                .GroupBy(record => record.Seller.Department)
                .ToListAsync();
        }
    }
}
