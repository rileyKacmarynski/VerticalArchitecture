using Application.Abstractions;
using Application.Shared;
using MediatR;
using System.Collections.Generic;

namespace Application.Products.GetProducts
{
    public class GetProductsUseCase : IUseCase<IEnumerable<ProductDto>>
    {
    }
}
