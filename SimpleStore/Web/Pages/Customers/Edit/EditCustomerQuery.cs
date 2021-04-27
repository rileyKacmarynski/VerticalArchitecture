using MediatR;

namespace Web.Pages.Customers.Edit
{
    public record EditCustomerQuery : IRequest<EditCustomerCommand>
    {
        public int CustomerId { get; set; }
    }
}