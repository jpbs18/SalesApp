using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<Seller>> FindAllAsync() => await _context.Seller.ToListAsync();

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(long id) => await _context.Seller
            .Include(seller => seller.Department)
            .FirstOrDefaultAsync(seller => seller.Id == id);

        public async Task DeleteAsync(long id)
        {
            try
            {
                Seller seller = await _context.Seller.FindAsync(id);
                _context.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller that has sales associated");
            }
     
        }

        public async Task UpdateAsync(Seller receivedSeller)
        {
            bool sellerExists = await _context.Seller.AnyAsync(seller => seller.Id == receivedSeller.Id);

            if (!sellerExists)
            {
                throw new NotFoundException("Id not found in Seller table.");
            }

            try
            {
                _context.Update(receivedSeller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
