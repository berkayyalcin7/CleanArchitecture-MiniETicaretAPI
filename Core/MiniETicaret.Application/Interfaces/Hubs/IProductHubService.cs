using MiniETicaret.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Interfaces.Hubs
{
    public interface IProductHubService
    {
        Task ProductAddedAsync(ProductDto product);
    }
}
