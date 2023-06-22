using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class SaleServiceImpl : ISaleService
    {
        private readonly MacberriprojectContext _context;
        public SaleServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        public async Task MakeASale(string id_user)
        {
            var chopCartItems = await _context.Shopcarts.Where(sc => sc.IdUser.ToString().Equals(id_user)).Include(sc => sc.IdProductNavigation).ToListAsync();

            if (chopCartItems.Count > 0)
            {
                decimal total = chopCartItems.Sum(p => p.IdProductNavigation.Price * p.Quantity);

                var sale = new Sale()
                {
                    IdUser = Guid.Parse(id_user),
                    Total = total,
                };

                await _context.Sales.AddAsync(sale);
                await _context.SaveChangesAsync();

                foreach (var item in chopCartItems)
                {
                    var detail = new Detail()
                    {
                        IdProduct = item.IdProduct,
                        IdSale = sale.IdSale,
                        UnitPrice = item.IdProductNavigation.Price,
                        TotalPrice = (item.IdProductNavigation.Price * item.Quantity),
                        Quantity = item.Quantity
                    };
                    await _context.Details.AddAsync(detail);
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
