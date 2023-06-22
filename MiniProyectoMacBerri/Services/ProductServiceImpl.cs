using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class ProductServiceImpl : IProductService
    {
        private readonly MacberriprojectContext _context;
        public ProductServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        public async Task AddProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                UrlImage = productDto.UrlImage
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(string id_product)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.IdProduct.ToString().Equals(id_product));
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var productList = await _context.Products.ToListAsync();
            return productList;
        }

        public async Task<Product?> GetById(string id_product)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.IdProduct.ToString().Equals(id_product));
            return product;
        }

        public async Task UpdateProduct(ProductDto productDto, string id_product)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.IdProduct.ToString().Equals(id_product));
            if (product != null)
            {
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.UrlImage = productDto.UrlImage;
            }
            await _context.SaveChangesAsync();
        }
    }
}
