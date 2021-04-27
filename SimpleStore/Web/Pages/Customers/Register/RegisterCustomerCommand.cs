using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Customers.Register
{
    public record RegisterCustomerCommand : IRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
    }
}