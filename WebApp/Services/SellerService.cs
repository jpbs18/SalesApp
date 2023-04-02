using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services.Exceptions;

namespace WebApp.Services
{
    public class SellerService
    {
        private readonly WebAppContext _context;

        public SellerService(WebAppContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll() => _context.Seller.ToList();

        public void Insert(Seller seller)
        {
            _context.Seller.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(long id) => _context.Seller
            .Include(seller => seller.Department)
            .FirstOrDefault(seller => seller.Id == id);

        public void Delete(long id)
        {
            Seller seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller receivedSeller)
        {
            bool sellerExists = _context.Seller.Any(seller => seller.Id == receivedSeller.Id);

            if (!sellerExists)
            {
                throw new NotFoundException("Id not found in Seller table.");
            }

            try
            {
                _context.Seller.Update(receivedSeller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
