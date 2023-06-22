using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public interface IProductService
    {

        public Task<List<Product>> GetAll();
        public Task<Product?> GetById(string id_product);

        public Task AddProduct(ProductDto productDto);

        public Task UpdateProduct(ProductDto productDto, string id_product);

        public Task DeleteById(string id_product);

    }
}
