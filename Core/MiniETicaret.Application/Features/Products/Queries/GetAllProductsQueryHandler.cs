using AutoMapper;
using MediatR;
using MiniETicaret.Application.DTOs;
using MiniETicaret.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Features.Products.Queries
{
    // .NET 8 primary constructor özelliğini kullanıyoruz.
    public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // 1. Repository aracılığıyla veritabanından domain entity'lerini al.
            var products = await productRepository.GetAllAsync();

            // 2. AutoMapper ile domain entity'lerini DTO'lara dönüştür.
            var productDtos = mapper.Map<List<ProductDto>>(products);

            // 3. DTO listesini geri döndür.
            return productDtos;
        }
    }
}
