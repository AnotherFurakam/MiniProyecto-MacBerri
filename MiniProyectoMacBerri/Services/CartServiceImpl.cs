using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class CartServiceImpl : ICartServices
    {
        private readonly MacberriprojectContext _context;
        public CartServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        public async Task AddToCart(string id_user, string id_product)
        {
            var cartItem = await _context.Shopcarts.FirstOrDefaultAsync(sc => sc.IdUser.ToString().Equals(id_user) && sc.IdProduct.ToString().Equals(id_product));

            if (cartItem == null)
            {
                Shopcart cart = new()
                {
                    IdUser = Guid.Parse(id_user),
                    IdProduct = Guid.Parse(id_product)
                };
                await _context.Shopcarts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }
        }

        public void ClearCart(string id_user)
        {
            var cartItems = _context.Shopcarts.Where(sc => sc.IdUser.ToString().Equals(id_user)).ToList();
            _context.Shopcarts.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public async Task<int> CountProducts(string id_user)
        {
            int countItems = await _context.Shopcarts.CountAsync(s => s.IdUser.ToString().Equals(id_user));
            return countItems;
        }

        public async Task<List<Product>> ItemsByUserId(string id_user)
        {
            var items = await _context.Shopcarts.Where(sc => sc.IdUser.ToString().Equals(id_user)).Include(sc => sc.IdProductNavigation).ToListAsync();
            var products = items.Select(i => i.IdProductNavigation).ToList();
            return products;
        }
    }
}
