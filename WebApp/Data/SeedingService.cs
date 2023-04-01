using System;
using System.Linq;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Data
{
    public class SeedingService
    {
        private WebAppContext _context;

        public SeedingService(WebAppContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return;
            }

            Department d1 = new Department(1, "Electronics");
            Department d2 = new Department(2, "Computers");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller(2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0, d2);
            Seller s3 = new Seller(3, "Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0, d1);
            Seller s4 = new Seller(4, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0, d3);
            Seller s5 = new Seller(5, "Martha Red", "martha@gmail.com", new DateTime(1997, 3, 4), 3000.0, d4);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 09, 4), 6000.0, SaleStatus.Billed, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2018, 09, 7), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2018, 09, 12), 3000.0, SaleStatus.Billed, s2);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2018, 09, 1), 11000.0, SaleStatus.Billed, s3);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2018, 09, 15), 13000.0, SaleStatus.Billed, s4);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2018, 09, 10), 2000.0, SaleStatus.Billed, s5);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2018, 09, 10), 1000.0, SaleStatus.Billed, s5);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2018, 09, 18), 2000.0, SaleStatus.Billed, s3);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2018, 09, 22), 1000.0, SaleStatus.Billed, s4);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4, s5);
            _context.SalesRecord.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);

            _context.SaveChanges();
        }
    }
}
