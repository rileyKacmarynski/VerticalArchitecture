using MediatR;

namespace Web.Pages.Customers.Edit
{
    public record EditCustomerCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}