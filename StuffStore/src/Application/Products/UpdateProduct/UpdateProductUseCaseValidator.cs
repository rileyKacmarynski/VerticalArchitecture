using FluentValidation;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductUseCaseValidator : AbstractValidator<UpdateProductUseCase>
    {
        public UpdateProductUseCaseValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Price).NotEmpty()
                .GreaterThanOrEqualTo(0m);
        }
    }

}

