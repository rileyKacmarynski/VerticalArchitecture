using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.GetProducts
{
    public class ProductsRequest : IRequest<ProductsDto>
    {
    }

    public class ProductsRequestHandler : IRequestHandler<ProductsRequest, ProductsDto>
    {
        public async Task<ProductsDto> Handle(ProductsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
