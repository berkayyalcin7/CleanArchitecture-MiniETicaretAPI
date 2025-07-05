using MediatR;
using MiniETicaret.Application.DTOs;
using MiniETicaret.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>, ICacheableRequest
    {
        public string CacheKey => "GetAllProducts";
        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(5);
    }
}
