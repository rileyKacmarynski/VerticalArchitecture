using Application.Abstractions;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductUseCase : IUseCase
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }

}

