using AutoMapper;
using MiniETicaret.Application.DTOs;
using MiniETicaret.Application.Features.Products.Commands;
using MiniETicaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<CreateProductCommand, Product>();
        }
    }
}
