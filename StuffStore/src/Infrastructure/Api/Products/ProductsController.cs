using Application.Products.CreateProduct;
using Application.Products.GetProduct;
using Application.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductsUseCase(), cancellationToken);
            return result.ToActionResult();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProduct([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProduct.UseCase(id), cancellationToken);
            return result.ToActionResult();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var useCase = new CreateProduct.UseCase
            {
                Name = request.Name,
                Price = request.Price
            };
            var result = await _mediator.Send(useCase, cancellationToken);

            // here since we don't have a value I need to check
            // the result status anyway.
            return result.ToActionResult(
                onSuccess: _ => Ok(),
                onError: result => Problem(result.ErrorMessage)
                );
        }
    }
}
