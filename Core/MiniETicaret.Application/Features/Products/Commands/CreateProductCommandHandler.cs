using AutoMapper;
using MediatR;
using MiniETicaret.Application.Interfaces.Repositories;
using MiniETicaret.Application.Interfaces;
using MiniETicaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            product.CreatedDate = DateTime.UtcNow; // Tarih atamasını burada yapıyoruz.

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync(cancellationToken); // Değişiklikleri tek noktadan kaydediyoruz.

            return product.Id;
        }
    }
}
