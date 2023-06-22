using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniProyectoMacBerri.Models;
using System.ComponentModel.DataAnnotations;

namespace MiniProyectoMacBerri.Services
{
    public class CartServiceImpl : ICartServices
    {
        private readonly MacberriprojectContext _context;
        public CartServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        private async Task<Shopcart?> GetItemByUserAndProductId(string id_user, string id_product)
        {
            var cartItem = await _context.Shopcarts.FirstOrDefaultAsync(sc => sc.IdUser.ToString().Equals(id_user) && sc.IdProduct.ToString().Equals(id_product));
            return cartItem;
        }

        public async Task AddToCart(string id_user, string id_product)
        {
            var cartItem = await GetItemByUserAndProductId(id_user, id_product);

            if (cartItem == null)
            {
                Shopcart cart = new()
                {
                    IdUser = Guid.Parse(id_user),
                    IdProduct = Guid.Parse(id_product),
                    Quantity = 1
                };
                await _context.Shopcarts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }
            else
            {
                cartItem.Quantity += 1;
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

        public async Task DeleteItem(string id_user, string id_product)
        {
            var cartItem = await GetItemByUserAndProductId(id_user, id_product);
            if (cartItem != null)
            {
                _context.Shopcarts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Shopcart>> ItemsByUserId(string id_user)
        {
            var items = await _context.Shopcarts.Where(sc => sc.IdUser.ToString().Equals(id_user)).Include(sc => sc.IdProductNavigation).ToListAsync();
            return items;
        }

        public async Task RemoveToCart(string id_user, string id_product)
        {
            var cartItem = await GetItemByUserAndProductId(id_user, id_product);

            if (cartItem != null && cartItem.Quantity >= 2)
            {
                cartItem.Quantity -= 1;
                await _context.SaveChangesAsync();
            }
        }
    }
}
