using MiniETicaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id); // Product? -> Product nesnesi veya null dönebilir.
        Task AddAsync(Product product);
        void Update(Product product); // Update ve Delete işlemleri genellikle senkron olur.
        void Delete(Product product);
    }
}
